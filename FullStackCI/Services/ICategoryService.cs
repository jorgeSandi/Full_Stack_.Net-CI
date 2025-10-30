using FullStackCI.Dtos;

namespace FullStackCI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<CategoryDtoV2?> GetCategoryByIdV2Async(int id);
        Task<bool> GetCategoryExistsAsync (int id);
        Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<CategoryDto?> UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}