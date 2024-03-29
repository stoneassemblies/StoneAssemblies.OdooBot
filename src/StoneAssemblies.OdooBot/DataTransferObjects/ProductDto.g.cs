using System;
using System.Collections.Generic;
using StoneAssemblies.OdooBot.DataTransferObjects;

namespace StoneAssemblies.OdooBot.DataTransferObjects
{
    public partial class ProductDto
    {
        public Guid Id { get; set; }
        public long ExternalId { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ImageDto> Images { get; set; }
        public double InStockQuantity { get; set; }
        public double IncomingQuantity { get; set; }
        public double AggregateQuantity { get; set; }
        public string QuantityUnit { get; set; }
        public double StandardPrice { get; set; }
        public double Price { get; set; }
        public bool IsPublished { get; set; }
    }
}