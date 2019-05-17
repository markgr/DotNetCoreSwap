using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetCoreSwap.Helpers
{
    internal sealed class DocumentationFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            //if (swaggerDoc.Info.Title.Contains("Consumer"))
            //{
            swaggerDoc.Info.Description = GetMarkdown("DotNetCoreSwap.Helpers.Resources.explanation.md");
            //}

            swaggerDoc.Paths = swaggerDoc.Paths
                .OrderBy(e => e.Key, new PathComparer())
                .ToDictionary(e => e.Key, e => e.Value);
        }

        public string GetMarkdown(string resourcePath)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private class PathComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return string.Compare(WeightenPath(x), WeightenPath(y), StringComparison.OrdinalIgnoreCase);
            }

            private string WeightenPath(string path)
            {
                if (path.Contains("/settings"))
                    return "b/" + path;
                if (path.Contains("/secrets"))
                    return "c/" + path;
                if (path.Contains("/serviceProviders"))
                    return "a/" + path;

                return "e/" + path;
            }
        }
    }
}
