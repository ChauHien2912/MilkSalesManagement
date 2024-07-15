using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MilkPurchasingManagement.Repo.Dtos.Request.Product;
using MilkPurchasingManagement.Repo.Dtos.Response.Product;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WareHouseManagement.Repository.Specifications;

namespace MilkPurchasingManagement.Repo.Service.ProductService
{
    public class ProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private const string FirebaseStorageBaseUrl = "https://firebasestorage.googleapis.com/v0/b/milkmanagement-image.appspot.com/o";
        public ProductService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<GetProductResponse> GetProductById(int id)
        {
            var product = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id == id, include: i => i.Include(m => m.Images));
            if(product == null)
            {
                throw new Exception("Product not found!");
            }
            return _mapper.Map<GetProductResponse>(product);
        }

        public async Task<IPaginate<GetProductResponse>> GetProducts(int page, int size)
        {
            var products = await _uow.GetRepository<Product>()
            .GetPagingListAsync(               
                page: page,
                size: size,
                include: i => i.Include(po => po.Images)
            );

            var voucherResponses = new Paginate<GetProductResponse>()
            {
                Page = products.Page,
                Size = products.Size,
                Total = products.Total,
                TotalPages = products.TotalPages,
                Items = _mapper.Map<IList<GetProductResponse>>(products.Items),
            };
            return voucherResponses;
        }


        public async Task<bool> CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                ExpirationDate = request.ExpirationDate,
                Brand = request.Brand,
                Volume = request.Volume,
                AgeAllowed = request.AgeAllowed
            };

            if (request.ImgUrl != null && request.ImgUrl.Any())
            {
                var imageUrls = await UploadFilesToFirebase(request.ImgUrl);
                foreach (var imageUrl in imageUrls)
                {
                    product.Images.Add(new Image { ImageUrl = imageUrl });
                }
            }

            await _uow.GetRepository<Product>().InsertAsync(product);
            bool isCreated = await _uow.CommitAsync() > 0;
            return isCreated;
        }

        private async Task<List<string>> UploadFilesToFirebase(List<IFormFile> formFiles)
        {
            var uploadedUrls = new List<string>();

            try
            {
                using (var client = new HttpClient())
                {
                    foreach (var formFile in formFiles)
                    {
                        if (formFile.Length > 0)
                        {
                            string fileName = Path.GetFileName(formFile.FileName);
                            string firebaseStorageUrl = $"{FirebaseStorageBaseUrl}?uploadType=media&name=images/{Guid.NewGuid()}_{fileName}";

                            using (var stream = new MemoryStream())
                            {
                                await formFile.CopyToAsync(stream);
                                stream.Position = 0;
                                var content = new ByteArrayContent(stream.ToArray());
                                content.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);

                                var response = await client.PostAsync(firebaseStorageUrl, content);
                                if (response.IsSuccessStatusCode)
                                {
                                    var responseBody = await response.Content.ReadAsStringAsync();
                                    var downloadUrl = ParseDownloadUrl(responseBody, fileName);
                                    uploadedUrls.Add(downloadUrl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
            }

            return uploadedUrls;
        }

        private string ParseDownloadUrl(string responseBody, string fileName)
        {
            var json = JsonDocument.Parse(responseBody);
            var nameElement = json.RootElement.GetProperty("name");
            var downloadUrl = $"{FirebaseStorageBaseUrl}/{Uri.EscapeDataString(nameElement.GetString())}?alt=media";
            return downloadUrl;
        }

        public async Task<bool> UpdateProduct(int id, UpdateProductRequest request)
        {
            var productexisting = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: e => e.Id == id);
            if(productexisting == null)
            {
                throw new Exception("Product not found!");
            }

            productexisting = _mapper.Map<Product>(request);
            productexisting.Id = id;
            _uow.GetRepository<Product>().UpdateAsync(productexisting);
            bool isUpdate = await _uow.CommitAsync() > 0;
            return isUpdate;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var productexisting = await _uow.GetRepository<Product>().SingleOrDefaultAsync(predicate: e => e.Id == id);
            if(productexisting == null)
            {
                throw new Exception("Product not found");
            }
            _uow.GetRepository<Product>().DeleteAsync(productexisting);
            bool isDeleted = await _uow.CommitAsync() > 0;
            return isDeleted;


        }
    }
}
