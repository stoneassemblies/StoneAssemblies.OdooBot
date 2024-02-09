using System;
using System.Collections.Generic;
using StoneAssemblies.OdooBot.DataTransferObjects;

namespace StoneAssemblies.OdooBot.DataTransferObjects
{
    public partial class CategoryDto
    {
        public Guid Id { get; set; }
        public long ExternalId { get; set; }
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}