using Microsoft.AspNetCore.Components;
using StoneAssemblies.OdooBot.Client;

namespace StoneAssemblies.OdooBot.Wasm.Layout
{
    public partial class NavMenu
    {
        private ICollection<CategoryDto>? categories;

        [Inject]
        private ICatalogServiceClient CatalogServiceClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.categories = await CatalogServiceClient.GetCategoriesAsync();
        }
    }
}
