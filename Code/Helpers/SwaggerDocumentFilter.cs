using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreSwap.Helpers
{
    /// <summary>
    /// Splits the examples for the different models
    /// </summary>
    internal sealed class SwaggerExampleFilter : IDocumentFilter
    {
        private readonly IDictionary<string, object> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DotNetCoreSwap.Helpers.SwaggerExampleFilter"/> class.
        /// </summary>
        /// <param name="values">Values.</param>
        public SwaggerExampleFilter(IDictionary<string, object> values)
        {
            _values = values;
        }

        /// <summary>
        /// Apply the specified examples into the swaggerDoc using the document context.
        /// </summary>
        /// <param name="swaggerDoc">Swagger document.</param>
        /// <param name="context">Context.</param>
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
