using FullStackCI.Dtos;
using FullStackCI.Models;

namespace FullStackCI.Services
{
    public class CategoryService(IUnitOfWorkService unitOfWork) : ICategoryService
    {
        private readonly IUnitOfWorkService _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return categories.Select(ConvertToDto);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            return category == null ? null : ConvertToDto(category);
        }

        public async Task<CategoryDtoV2?> GetCategoryByIdV2Async(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            return category == null ? null : ConvertToDtoV2(category);
        }

        public async Task<bool> GetCategoryExistsAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.ExistsAsync(id);
            return category;
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            var createdCategory = _unitOfWork.CategoryCommandRepository.Create(category);

            await _unitOfWork.SaveChangesAsync();

            return ConvertToDto(createdCategory);
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            _unitOfWork.CategoryCommandRepository.Update(category);

            await _unitOfWork.SaveChangesAsync();

            return ConvertToDto(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (!await _unitOfWork.CategoryRepository.ExistsAsync(id))
                return false;

            await _unitOfWork.CategoryCommandRepository.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private CategoryDto ConvertToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        private CategoryDtoV2 ConvertToDtoV2(Category category)
        {
            return new CategoryDtoV2
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Message = "Prueba version 2"
            };
        }
    }
}