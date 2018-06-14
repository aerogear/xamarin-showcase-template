using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Utils
{

    [ContentProperty("Title")]
    class TitleStringResource:StringResource {
        
    }

    [ContentProperty("Text")]
    class StringResource:IMarkupExtension
    {
        const string ResourceId = "Example.Resources.StringResources";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(StringResource)).Assembly));

        public string Text { get; set; }

        public StringResource()
        {
            
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            var text = ResMgr.Value.GetString(Text);
            if (text == null)
            {
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}'.", Text, ResourceId),
                    "Text");
            }
            return text;
        }
    }
}
