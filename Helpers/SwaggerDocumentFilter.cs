using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreSwap.Helpers
{
    internal sealed class SwaggerExampleFilter : IDocumentFilter
    {
        private readonly IDictionary<string, object> _values;

        public SwaggerExampleFilter(IDictionary<string, object> values)
        {
            _values = values;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var value in _values)
            {
                string schemaName = value.Key.Split('.').First();
                string propertyName = value.Key.Split('.').Last();

                Schema schema;
                if (context.SchemaRegistry.Definitions.TryGetValue(schemaName, out schema))
                {
                    Schema property;
                    if (schema.Properties.TryGetValue(propertyName, out property))
                    {
                        property.Example = value.Value;
                    }
                }
            }
        }
    }
}
