using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetCoreSwap.Helpers
{

    internal sealed class PayloadPropertyExamplesDocumentFilter : IDocumentFilter
    {
        private readonly IDictionary<(Type Schema, string PropertyName), object> _values;

        public PayloadPropertyExamplesDocumentFilter(IDictionary<(Type Schema, string PropertyName), object> values)
        {
            _values = values;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var value in _values)
            {
                if (context.SchemaRegistry.Definitions.TryGetValue(value.Key.Schema.Name, out Schema schema))
                {
                    Schema property = schema.Properties
                        .Where(p => p.Key.Equals(value.Key.PropertyName, StringComparison.OrdinalIgnoreCase))
                        .Select(p => p.Value)
                        .FirstOrDefault();

                    if (property != null)
                    {
                        property.Example = value.Value;
                    }
                }
            }
        }
    }
}
