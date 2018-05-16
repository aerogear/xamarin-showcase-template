#!/bin/sh

rm -rd "Example"*

git remote add sdk "https://github.com/wojta/aerogear-xamarin-sdk.git"
git fetch sdk master
git checkout sdk/master -- "./Example"
git rm -rf --cached "./Example"
git remote remove sdk
git fetch origin master

mv "Example" "Example.old"
mv "Example.old/"* .
rm -rd "Example.old"
./scripts/update_app.js removeDeps --write
./scripts/update_app.js addNuGets --write
