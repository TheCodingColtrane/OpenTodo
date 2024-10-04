using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using OpenTodo.Data;
using OpenTodo.DTOs;
using OpenTodo.Models;
using OpenTodo.Utils;

namespace OpenTodo.Repositories {
    public class CategoryRepository(OpenTodoContext db)
    {
        private readonly OpenTodoContext _db = db;
        private readonly HashID hashID = new();
         private readonly CategoryDTO dto = new();
          public async Task<List<CategoryDTO>> GetAllCategories()
        {

            var categories = await _db.Categories.Take(30).OrderByDescending(c => c.ID).ToListAsync();
            var categoriesDTO = dto.ConvertSchemaToDTO(categories);
            return categoriesDTO;

            }
        
        public async Task<List<CategoryDTO>> GetAllCategoriesByBoard(int id)
        {
            var categories = await _db.Categories.Where(c => c.Board.ID == id).Take(30).OrderByDescending(c => c.ID).ToListAsync();
             var categoriesDTO = dto.ConvertSchemaToDTO(categories);
            return categoriesDTO;

        }


        public async Task<CategoryDTO> GetCategoriesById(int id)
        {
            var category = await _db.Categories.Where(c => c.ID == id).FirstAsync();
            return dto.ConvertSchemaToDTO([category])[0];
        }

        

        public async Task<string> Create(CategorySchema category){
            var newCategory = _db.Categories.AddAsync(category);
            var savedCategory = _db.SaveChangesAsync();
            await Task.WhenAll([newCategory.AsTask(), savedCategory]);
            var code = hashID.GenerateHash(category.ID);
            return code;

        }

        public async Task<bool> Delete(int id){
            var category = await _db.FindAsync<CategorySchema>(id);
            if(category is not null){
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
        
           public async Task<bool> Update(CategorySchema correctedCategory){
            var category = await _db.FindAsync<CategorySchema>(correctedCategory.ID);
            if(category is not null){
                _db.Categories.Update(correctedCategory);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    
    }

}

   

