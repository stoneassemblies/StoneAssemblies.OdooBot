using StoneAssemblies.OdooBot.Client;

namespace StoneAssemblies.OdooBot.Wasm.ViewModels
{
    public class ProductDetailsViewModel(ProductDetails product)
    {
        public long ExternalId => product.ExternalId;

        public string Name => product.Name;

        public string Description => product.Description;

        public string QuantityUnit => product.QuantityUnit;

        public double InStockQuantity => product.InStockQuantity;

        public double IncomingQuantity => product.IncomingQuantity;

        public double AggregateQuantity => product.AggregateQuantity;

        public double StandardPrice => product.StandardPrice;
        
        public bool IsPublished => product.IsPublished;

        public string ReferenceImage
        {
            get
            {
                var image = product.FeatureImages.FirstOrDefault();
                if (image is null)
                {
                    return string.Empty;
                }

                return $"data:image/png;base64, {Convert.ToBase64String(image.Content)}";
            }
        }
    }
}
