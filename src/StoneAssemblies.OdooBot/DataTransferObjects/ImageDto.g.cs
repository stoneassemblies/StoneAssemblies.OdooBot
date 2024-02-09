using System;
using StoneAssemblies.OdooBot.DataTransferObjects;
using StoneAssemblies.OdooBot.Entities;

namespace StoneAssemblies.OdooBot.DataTransferObjects
{
    public partial class ImageDto
    {
        public Guid Id { get; set; }
        public long? ExternalId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public ImageSize Size { get; set; }
        public byte[] Content { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}