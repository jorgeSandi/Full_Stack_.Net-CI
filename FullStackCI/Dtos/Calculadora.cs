namespace FullStackCI.Dtos
{
    public class Calculadora
    {
        //Crea los metodos de una calculadora sumar, restar, dividir, multiplicar
        public int Numero1 { get; set; }
        public int Numero2 { get; set; }
        public string Operacion { get; set; } = string.Empty;
        public double Resultado { get; set; }
        public double Calcular()
        {
            switch (Operacion)
            {
                case "sumar":
                    return Resultado = Numero1 + Numero2;
                case "restar":
                    return Resultado = Numero1 - Numero2;
                case "multiplicar":
                    return Resultado = Numero1 * Numero2;
                case "dividir":
                    if (Numero2 != 0)
                    {
                        return Resultado = (double)Numero1 / Numero2;
                    }
                    else
                    {
                        throw new DivideByZeroException("No se puede dividir por cero");
                    }
                default:
                    throw new InvalidOperationException("Operación no válida");
            }
        }

        //sumar con 2 parametros
        public double Sumar(double num1, double num2)
        {
            return num1 + num2;
        }

        //restar con 2 parametros
        public double Restar(double num1, double num2)
        {
            return num1 - num2;
        }

        //multiplicar con 2 parametros
        public double Multiplicar(double num1, double num2)
        {
            return num1 * num2;
        }

        //dividir con 2 parametros
        public double Dividir(double num1, double num2)
        {
            if (num2 != 0)
            {
                return num1 / num2;
            }
            else
            {
                throw new DivideByZeroException("No se puede dividir por cero");
            }
        }
    }
}