using Blorc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using StoneAssemblies.OdooBot.Client;
using StoneAssemblies.OdooBot.Wasm.ViewModels;

namespace StoneAssemblies.OdooBot.Wasm.Pages
{
    public partial class Catalog
    {
        [Parameter]
        public Guid? CategoryId { get; set; }

        public CategoryDto? Category { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }

        public HashSet<Guid> SelectedCategories { get; set; } = new HashSet<Guid>();

        [Inject]
        private ICatalogServiceClient CatalogServiceClient { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IServiceProvider ServiceProvider { get; set; }

        private IFileService FileService => this.ServiceProvider.GetRequiredService<IFileService>();

        public bool AreAllCategoriesSelected
        {
            get => SelectedCategories.Count == Categories.Count;
            set
            {
                if (value)
                {
                    SelectedCategories.UnionWith(Categories.Select(dto => dto.Id));
                }
                else
                {
                    SelectedCategories.Clear();
                }
            }
        }

        private bool _isDownloading;

        private bool _isLoading = true;

        private GridItemsProvider<ProductDetailsViewModel> _productsProvider = default!;

        private PaginationState _paginationState = new PaginationState
        {
            ItemsPerPage = 10
        };

        protected override async Task OnInitializedAsync()
        {
            if (CategoryId is not null)
            {
                this.Category = await this.CatalogServiceClient.GetCategoryByIdAsync(CategoryId);
                _productsProvider = async request =>
                {
                    try
                    {
                        var pagedResult = await CatalogServiceClient.GetProductsByCategoryIdAsync(CategoryId, request.StartIndex, request.Count);
                        var productPreviewViewModels = pagedResult.Items.Select(dto => new ProductDetailsViewModel(dto)).ToList();
                        return GridItemsProviderResult.From(items: productPreviewViewModels,
                            totalItemCount: pagedResult.Count);
                    }
                    finally
                    {
                        _isLoading = false;
                    }
                };
            }
            else
            {
                this.Categories = await this.CatalogServiceClient.GetCategoriesAsync();
            }
        }

        private async Task DownloadDocumentAsync()
        {
            _isDownloading = true;
            try
            {
                var request = new DownloadDocumentByCategoryIdRequest();
                if (this.CategoryId is not null)
                {
                    request.Ids = new List<Guid>
                    {
                        CategoryId.Value
                    };
                }
                else if (this.SelectedCategories.Count > 0)
                {
                    request.Ids = SelectedCategories.ToList();
                }

                var fileResult = await this.CatalogServiceClient.DownloadDocumentByCategoryIdsAsync(request);

                await FileService.SaveAsync(fileResult.FileName, fileResult.Content);

                ToastService.ShowToast(ToastIntent.Download, "Catalog file have been successfully generated. Please check your browser’s download manager.");
            }
            finally
            {
                _isDownloading = false;
            }
        }
        private void OnCheckedChanged(Guid categoryId, bool isChecked)
        {
            if (isChecked)
            {
                this.SelectedCategories.Add(categoryId);
            }
            else
            {
                this.SelectedCategories.Remove(categoryId);
            }
        }
    }
}