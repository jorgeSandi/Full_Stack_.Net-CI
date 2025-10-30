using FullStackCI.Models;

namespace FullStackCITest
{
    public class UnitTest1
    {
        [Fact] //No envia parametros
        public void Test1()
        {
            Category _category = new()
            {
                Id = 1,
                Description = "Test",
                Name = "Test"
            };

            //Assert es para ejecutar comprobaciones
            Assert.NotNull(_category);
            Assert.NotNull(_category.Description);
            Assert.Equal("Test", _category.Name);

            //Forzar que la prueba se caiga
            //Assert.Equal(3, _category.Id);
        }
    }
}