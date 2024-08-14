using AutoMapper;
using Go1Bet.Core.Context;
using Go1Bet.Core.DTO_s.Category;
using Go1Bet.Core.Entities.Category;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Services
{
    public class CategoryService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CategoryService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var categories = _context.Categories

                    .Select(c => new CategoryItemDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        ParentId = c.ParentId,
                        ParentName = c.Parent.Name,
                        countSubategories =
                            c.Subcategories
                            .Where(s => s.ParentId == c.Id)
                            .Select(s => new CategoryItemDTO
                            {
                                Id = s.Id
                            }).ToList().Count,
                    }).ToList();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = categories
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResponse> GetMainCategoriesAsync()
        {
            var categories = _context.Categories
            .Where(c => c.ParentId == null)
            .Select(c => new CategoryItemDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ParentId = c.ParentId,
                ParentName = c.Parent.Name,
                countSubategories =
                            c.Subcategories
                            .Where(s => s.ParentId == c.Id)
                            .Select(s => new CategoryItemDTO
                            {
                                Id = s.Id
                            }).ToList().Count,
            }).ToList();
            return new ServiceResponse
            {
                Success = true,
                Payload = categories
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var category = _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryItemDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ParentId = c.ParentId,
                    ParentName = c.Parent.Name,
                    //countSubcategories
                    countSubategories =
                            c.Subcategories
                            .Where(s => s.ParentId == c.Id)
                            .Select(s => new CategoryItemDTO
                            {
                                Id = s.Id
                            }).ToList().Count,
                    //Subcategories
                    Subcategories =
                      c.Subcategories
                      .Where(c => c.ParentId == id)
                      .Select(s =>
                          new CategoryItemDTO
                          {
                              Id = s.Id,
                              Name = s.Name,
                              ParentId = s.ParentId,
                              ParentName = s.Parent.Name,
                              Description = s.Description,
                          }).ToList(),
                }).ToList();

            if (category != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Payload = category[0]
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Category not found"
            };
        }


        public async Task<ServiceResponse> Create(CategoryCreateDTO model)
        {
            var category = _mapper.Map<CategoryEntity>(model);
            category.Id = Guid.NewGuid().ToString();
            category.ParentId = model.ParentId == "string" ? null : model.ParentId;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Category was created",
                Success = true,
            };
        }

        public async Task<ServiceResponse> EditCategoryAsync(CategoryEditDTO model)
        {
            var category = await _context.Categories.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (category == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload category is not correct, upload is closed",
                    Success = false,
                };
            }

            category.Name = model.Name;
            category.ParentId = model.ParentId == "string" ? null : model.ParentId;
            category.Description = model.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return new ServiceResponse()
            {
                Message = "Category update was successful",
                Success = true,
            };

        }
        public async Task<ServiceResponse> DeleteCategoryAsync(string id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded category is not correct, uploaded is closed",
                    Success = false,
                };
            }



             _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new ServiceResponse()
            {
                Message = "Сategory has been deleted",
                Success = true,
            };
        }
    }
}
