using AutoMapper;
using MilkPurchasingManagement.Repo.Dtos.Request.Review;
using MilkPurchasingManagement.Repo.Dtos.Response;
using MilkPurchasingManagement.Repo.Dtos.Response.Product;
using MilkPurchasingManagement.Repo.Dtos.Response.Review;
using MilkPurchasingManagement.Repo.Models;
using MilkPurchasingManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseManagement.Repository.Specifications;

namespace MilkPurchasingManagement.Repo.Service.ReviewService
{
    public class ReviewService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateReview(CreateReviewRequest request)
        {
            var product = await _uof.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id == request.ProductId);
            if (product == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Product not found",
                };
            }
            var user = await _uof.GetRepository<User>().SingleOrDefaultAsync(predicate: p => p.Id == request.UserId);
            if (product == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User not found",
                };
            }

            var review = new Review
            {
                UserId = user.Id,
                ProductId = product.Id,
                Content = request.Content,
                Rate = request.Rate,
            };
            _uof.GetRepository<Review>().InsertAsync(review);
            await _uof.CommitAsync();
            return new ApiResponse { Success = true, Message = "Create Review Successfully" };
        }


        public async Task<ApiResponse> UpdateReview(int id, UpdateReview request)
        {
            var review = await _uof.GetRepository<Review>().SingleOrDefaultAsync(predicate: p => p.Id == id);
            if (review == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Product not found",
                };
            }
            review = _mapper.Map<Review>(request);
            review.Id = id;
            _uof.GetRepository<Review>().UpdateAsync(review);
            _uof.CommitAsync();
            return new ApiResponse { Success = true, Message = "Update Review Successfully" };
        }

        public async Task<ApiResponse> Delete(int id)
        {
            var review = await _uof.GetRepository<Review>().SingleOrDefaultAsync(predicate: p => p.Id == id);
            if (review == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Product not found",
                };
            }
           
            _uof.GetRepository<Review>().DeleteAsync(review);
            _uof.CommitAsync();
            return new ApiResponse { Success = true, Message = "Delete Review Successfully" };
        }

        public async Task<GetReviewResponse> GetReviewById(int id)
        {
            var review = await _uof.GetRepository<Review>().SingleOrDefaultAsync(predicate: p => p.Id == id);
            if(review == null)
            {
                throw new Exception("Not found review");
            }
            return _mapper.Map<GetReviewResponse>(review);
        }

        public async Task<IPaginate<GetReviewResponse>> GetReviewByUserId(int id,int page, int size)
        {
            var review = await _uof.GetRepository<Review>().GetPagingListAsync(predicate: p => p.UserId == id, page: page, size: size);
            if (review == null)
            {
                throw new Exception("Not found review");
            }
            var reviewResponses = new Paginate<GetReviewResponse>()
            {
                Page = review.Page,
                Size = review.Size,
                Total = review.Total,
                TotalPages = review.TotalPages,
                Items = _mapper.Map<IList<GetReviewResponse>>(review.Items),
            };
            return reviewResponses;
        }

        public async Task<IPaginate<GetReviewResponse>> GetReviewProductId(int id, int page, int size)
        {
            var review = await _uof.GetRepository<Review>().GetPagingListAsync(predicate: p => p.ProductId == id, page: page, size: size);
            if (review == null)
            {
                throw new Exception("Not found review");
            }
            var reviewResponses = new Paginate<GetReviewResponse>()
            {
                Page = review.Page,
                Size = review.Size,
                Total = review.Total,
                TotalPages = review.TotalPages,
                Items = _mapper.Map<IList<GetReviewResponse>>(review.Items),
            };
            return reviewResponses;
        }

    }
}
