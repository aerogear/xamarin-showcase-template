#!/usr/bin/env node
'use strict'
const yargs = require("yargs")
const fs = require("fs")
const xmldom = require("xmldom")
const promisify = require("util").promisify
const csprojeditor = require("./modules/csproj_editor")

const readFile = promisify(fs.readFile)
const writeFile = promisify(fs.writeFile)

const argv = yargs.usage("Usage: $0 <command> [options]'")
    .command("removeDeps", "Removes project dependencies").command("addNuGets","Adds NuGet dependencies to project").demandCommand(1)
    .option("write", {
        default: false, desc:
            "Writes changes to .csproj files (it's a dry-run by default)"
    }).argv

readFile(`${__dirname}/../config.json`).then(data => {
    configLoaded(JSON.parse(data));
}).catch(err => console.log(`$err: Failed to open or read config.json file.`))

/**
 * Opens .csproj XML
 * @param {string} path path to solution directory
 * @param {string} projName 
 * @returns {Promise}
 */
async function openCsproj(path, projName) {
    const fname = `${path}${projName}/${projName}.csproj`
    const data = await readFile(fname);
    return new xmldom.DOMParser().parseFromString(data.toString());
}
/**
 * Saves .csproj XML
 * @param {string} path path to solution directory
 * @param {string} projName project name
 * @param {Document} doc XML document
 */
async function saveCsproj(path, projName, doc) {
    const fname = `${path}${projName}/${projName}.csproj`
    const xmlstring = new xmldom.XMLSerializer().serializeToString(doc)
    await writeFile(fname, xmlstring)
}

/**
 * Adds project dependencies in csproj xml.
 * @param {Document} XML document
 * @param {Object} config configuration
 * @param {string} project project name 
 */
async function addNuGetDeps(doc, config, project) {
    await Object.keys(config["deps-add"][project]).forEach(async dependency => {
        const dependencyVersion = config["deps-add"][project][dependency]
        const result = await csprojeditor.addNuGetDependency(doc, dependency, dependencyVersion)
        console.log(`Project "${project}" added dependency "${dependency}" version ${dependencyVersion}.`);
    })
    return doc;
}

/**
 * Removes project dependencies in csproj xml.
 * @param {Document} XML document
 * @param {Object} config configuration
 * @param {string} project project name 
 */
async function removeDeps(doc, config, project) {
    await config["proj-refs-remove"][project].forEach(async dependency => {
        const result = await csprojeditor.removeProjectDep(doc, dependency)
        console.log(`Project "${project}" dependency "${dependency}" ${result ? "removed" : "not found"}.`);
    })
    return doc;
}

async function processProject(config, project, operation,dryrun) {
    const dir = `${__dirname}/../`;
    const doc = await openCsproj(dir, project)
    await operation(doc,config,project)
    if (!dryrun) {
        saveCsproj(dir, project, doc)
        console.log(`Saved project ${project}.csproj`)
    } else {
        console.log(`Dry-run for project ${project}.csproj completed, changes not saved.`)
    }
}

function configLoaded(config) {
    if (argv._.includes("removeDeps")) {
        console.log("Removing project dependencies:")
        Object.keys(config["proj-refs-remove"]).forEach(async project => await processProject(config, project,removeDeps,!argv.write))
    } else if (argv._.includes("addNuGets")) {
        console.log("Adding project NuGet dependencies:")
        Object.keys(config["deps-add"]).forEach(async project => await processProject(config, project,addNuGetDeps,!argv.write))
    } else {
        console.log(`Invalid command "${argv._[0]}" specified\n`);
        yargs.showHelp()
    }
}

