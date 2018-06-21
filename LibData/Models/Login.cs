using System.ComponentModel.DataAnnotations;

namespace LibData.Models
{
    public class Login

    {
        [Required(ErrorMessage = "Campo requerido erwin"), StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no valido"), EmailAddress(ErrorMessage = "Correo no valido")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [DataType(DataType.Password, ErrorMessage = "Contraseña no valida")]
        public string password { get; set; }

    }
}
