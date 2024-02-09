using Blorc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using StoneAssemblies.OdooBot.Client;

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
        private IFileService FileService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        public bool AreAllCategoriesSelected
        {
            get => SelectedCategories.Count == Categories.Count;
            set
            {
                if (value == true)
                {
                    SelectedCategories.UnionWith(Categories.Select(dto => dto.Id));
                }
                else
                {
                    SelectedCategories.Clear();
                }
            }
        }

        public bool _isDownloading = false;

        private GridItemsProvider<ProductPreviewViewModel> _productsProvider = default!;

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
                    var pagedResult = await CatalogServiceClient.GetProductsByCategoryIdAsync(CategoryId, request.StartIndex, request.Count);
                    var productPreviewViewModels = pagedResult.Items.Select(dto => new ProductPreviewViewModel(dto)).ToList();
                    return GridItemsProviderResult.From(items: productPreviewViewModels,
                        totalItemCount: pagedResult.Count);
                };
            }
            else
            {
                this.Categories = await this.CatalogServiceClient.GetCategoriesAsync();
            }
        }

        public class ProductPreviewViewModel(ProductDetails product)
        {
            public long ExternalId => product.ExternalId;

            public string Name => product.Name;

            public string Description => product.Description;

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
                await this.FileService.SaveAsync(fileResult.FileName, fileResult.Content);

                ToastService.ShowToast(ToastIntent.Download, "Catalog file have been successfully generated. Please check your browser’s download manager.");
            }
            finally
            {
                _isDownloading = false;
            }
        }

        private Task OnCheckedChanged(Guid categoryId, bool isChecked)
        {
            if (isChecked)
            {
                this.SelectedCategories.Add(categoryId);
            }
            else
            {
                this.SelectedCategories.Remove(categoryId);
            }

            return Task.CompletedTask;
        }
    }
}
