﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Category GetCategory(int id)
        {
            var category = _categoryRepository.Get(c => c.Id == id).FirstOrDefault();
            return category;
        }

        public async Task<Category> CreateCategory(CategoryCreateRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                Weight = request.Weight
            };
            _categoryRepository.Add(category);
            await _categoryRepository.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> CreateCategories(List<CategoryCreateRequest> categoryRequests)
        {
            var categories = new List<Category>();
            foreach (var categoryRequest in categoryRequests)
            {
                var category = new Category
                {
                    Name = categoryRequest.Name,
                    Weight = categoryRequest.Weight
                };
                _categoryRepository.Add(category);
                categories.Add(category);
            }
            await _categoryRepository.SaveChangesAsync();
            return categories;
        }

        public async Task<List<Category>> UpdateCategories(List<CategoryUpdateRequest> categoryRequests)
        {
            var categoryIds = categoryRequests.Select(cr => cr.Id);
            var categories = _categoryRepository.GetAll().Where(c => categoryIds.Contains(c.Id)).ToList();
            foreach (var category in categories)
            {
                var categoryRequest = categoryRequests.Find(cr => cr.Id == category.Id);
                category.Name = categoryRequest.Name;
                category.Weight = categoryRequest.Weight;
                _categoryRepository.Update(category);
            }

            await _categoryRepository.SaveChangesAsync();
            return categories;
        }

        public async Task DeleteCategory(int id)
        {
            var category = _categoryRepository.Get(c => c.Id == id).FirstOrDefault();
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}