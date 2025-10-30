using FullStackCI.Models;
using FullStackCI.Services;
using Moq;

namespace FullStackCITest.Services
{
    public class CategoryServiceTest
    {
        private readonly Mock<IUnitOfWorkService> _unitOfWorkService;
        private readonly CategoryService _service;

        public CategoryServiceTest()
        {
            _unitOfWorkService = new Mock<IUnitOfWorkService>();
            _service = new CategoryService(this._unitOfWorkService.Object);
        }

        [Fact]
        public async Task GetAllCategoria_Succes_ReturnCategoriaDto()
        {
            // Arrange
            var categorias = new List<Category>
            {
                new Category { Id = 1, Name = "Categoria1", Description = "Descripcion1" },
                new Category { Id = 2, Name = "Categoria2", Description = "Descripcion2" }
            };
            _unitOfWorkService.Setup(u => u.CategoryRepository.GetAllAsync())
                .ReturnsAsync(categorias);
            // Act
            var result = await _service.GetAllCategoriesAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Categoria1", result.First().Name);
        }

        [Fact]
        public async Task GetByIdCategoria_ExistingId_ReturnCategoriaDto()
        {
            // Arrange
            var categoria = new Category { Id = 1, Name = "Categoria1", Description = "Descripcion1" };

            _unitOfWorkService.Setup(u => u.CategoryRepository.GetByIdAsync(1))
                .ReturnsAsync(categoria);
            // Act
            var result = await _service.GetCategoryByIdAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Categoria1", result.Name);
        }

        [Fact]
        public async Task GetExistsAsync_ExistingId_ReturnCategoriaDtoAsync()
        {
            _unitOfWorkService.Setup(u => u.CategoryRepository.ExistsAsync(1))
                .ReturnsAsync(true); // Fix: Return a Task<bool> instead of a Category object.

            // Act
            var result = await _service.GetCategoryExistsAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result);
        }

        [Fact]
        public async Task GetNotExistsAsync_ExistingId_ReturnCategoriaDtoAsync()
        {

            _unitOfWorkService.Setup(u => u.CategoryRepository.ExistsAsync(3))
                .ReturnsAsync(false); // Fix: Return a Task<bool> instead of a Category object.
            // Act
            var result = await _service.GetCategoryExistsAsync(3);
            // Assert
            Assert.NotNull(result);
            Assert.False(result);
        }
    }
}
