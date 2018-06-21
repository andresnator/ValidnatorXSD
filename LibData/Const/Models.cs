namespace LibData.Const
{
    public class Models
    {
        public enum EEstadosArchivos
        {
            Programado,
            EnEjecucion,
            Terminado
        }


        public enum EEstadosValidacion
        {
            ConErrores,
            SinErrores
        }


        public enum EOperadoresAritmeticos
        {
            Suma = '+',
            Resta = '-',
            Divicion = '/',
            Multiplicacion = '*',
            Modulo = '%'
        }


        public enum EOperadoresLogicos
        {
            And = '&',
            Or = '|'
        }


        public enum EOperadoresRelacionales
        {
            Menor = '<',
            Mayor = '>',
            Igual = '=',
            Diferente = '!'
        }

        public enum ETipoDato
        {
            Entero = 1,
            String = 2,
            Decimal = 3,
            Date = 4
        }


        public static readonly string[] DateFormato =
        {
            "dd/mm/aaaa",
            "aaaammdd",
            "mmddaaaa"
        };

        public static decimal MinOccurs { get; set; } = 1;
        public static decimal MaxOccurs { get; set; } = 1000000;
    }
}