namespace LibValidadorXSD
{
    public class Modelos
    {
        public enum ETipoDato
        {
            Entero,
            Decimal,
            String,
            Date,

        }




        public static readonly string[] DateFormato =
        {
            "dd/mm/aaaa",
            "aaaammdd",
            "mmddaaaa"
        };


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


    }
}
