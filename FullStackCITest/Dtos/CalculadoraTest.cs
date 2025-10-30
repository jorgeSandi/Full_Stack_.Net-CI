using FullStackCI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCITest.Dtos
{
    public class CalculadoraTest
    {
        // [Unidad]_[Escenario]_[ResultadoEsperado]
        [Fact]
        public void Sumar_DosNumerosPositivos_RetornaLaSuma()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "sumar";
            double resultadoEsperado = 15;

            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            Assert.Equal(operacion, "sumar");
        }

        [Fact]
        public void dividir_DosNumerosPositivos_RetornaLaDivision()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "dividir";
            double resultadoEsperado = 0.5;

            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert

            Assert.Equal(resultadoEsperado, resultado);
            Assert.NotEmpty(operacion);
        }

        [Fact]
        public void dividir_DosNumerosPositivos_RetornaLaExepcion()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "dividir";

            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 0;

            // Act & Assert
            var error = Assert.Throws<DivideByZeroException>(() => calculadora.Calcular());
            Assert.IsType<DivideByZeroException>(error);
            Assert.NotEmpty(operacion);
        }

        [Fact]
        public void multiplicar_DosNumerosPositivos_RetornaLaMultiplicacion()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "multiplicar";
            double resultadoEsperado = 50;
            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            Assert.NotEmpty(operacion);
        }

        [Fact]
        public void restar_DosNumerosPositivos_RetornaLaResta()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "restar";
            double resultadoEsperado = -5;
            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            Assert.NotEmpty(operacion);
        }

        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(-5, -10, -15)]
        public void Sumar_VariosNumeros_RetornaLaSuma(double num1, double num2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            double resultado = calculadora.Sumar(num1, num2);
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(5, 10, -5)]
        [InlineData(-5, -10, 5)]
        public void Restar_VariosNumeros_RetornaLaResta(double num1, double num2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            double resultado = calculadora.Restar(num1, num2);
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(-5, -10, 50)]
        public void Multiplicar_VariosNumeros_RetornaLaMultiplicacion(double num1, double num2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            double resultado = calculadora.Multiplicar(num1, num2);
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(-10, -5, 2)]
        public void Dividir_VariosNumeros_RetornaLaDivision(double num1, double num2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            double resultado = calculadora.Dividir(num1, num2);
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(5, 0)]
        public void Dividir_VariosNumeros_RetornaLaException(double num1, double num2)
        {
            // Arrange
            var calculadora = new Calculadora();
            // Act
            var resultado = Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(num1, num2));
            // Assert
            Assert.IsType<DivideByZeroException>(resultado);
        }
    }
}
