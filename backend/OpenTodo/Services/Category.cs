using OpenTodo.DTOs;
using OpenTodo.Models;
using OpenTodo.Repositories;
using OpenTodo.Utils;

namespace OpenTodo.Services
{
    public class CategoryService(CategoryRepository categoryRepo)
    {
        private readonly CategoryRepository _categoryRepo = categoryRepo;
        private readonly HashID hashID = new();
       
        public async Task<List<CategoryDTO>> GetAllCategories()
        {

            return await _categoryRepo.GetAllCategories();

        }

        public async Task<List<CategoryDTO>> GetAllCategoriesByBoard(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return [];
            return await _categoryRepo.GetAllCategoriesByBoard(id);
        }

        public async Task<CategoryDTO> GetCategoriesById(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return new CategoryDTO();
            return await _categoryRepo.GetCategoriesById(id);
        }

         public async Task<string> Create(CategoryDTO category)
        {
            CategorySchema categorySchema = new() {Name = category.Name, BoardId = hashID.ReverseHash(category.Board.Code), CreatedAt = DateTime.Now.ToUniversalTime()};
            return await _categoryRepo.Create(categorySchema);
        }

        public async Task<bool> Update(CategoryDTO category)
        {
            CategorySchema categorySchema = new() {Name = category.Name, BoardId = hashID.ReverseHash(category.Board.Code), CreatedAt = DateTime.Now.ToUniversalTime()};
            return await _categoryRepo.Update(categorySchema);
        }
        
        public async Task<bool> Delete(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return new();
            return await _categoryRepo.Delete(id);
        }
        
    }
}
