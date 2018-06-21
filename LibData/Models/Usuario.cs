using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibData.Models
{
    [Table("Usuario", Schema = "Basicas")]
    public class Usuario
    {
        public Usuario()
        {
            Estado = true;
            //Contrasena = Guid.NewGuid().ToString();
        }

        public int UsuarioId { get; set; }


        [Required(ErrorMessage = "Campo requerido"), StringLength(30, ErrorMessage = "Cantidad Máxima de 30 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Primer Nombre", Description = "Primer nombre del usuario")]
        public string PrimerNombre { get; set; }

        [StringLength(30, ErrorMessage = "Cantidad Máxima de 30 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Segundo Nombre", Description = "Segundo nombre del usuario")]
        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(30, ErrorMessage = "Cantidad Máxima de 30 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Primer Apellido", Description = "Primer apellido del usuario")]
        public string PrimerApellido { get; set; }

        [StringLength(30, ErrorMessage = "Cantidad Máxima de 30 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Segundo Apellido", Description = "Segundo apellido del usuario")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "Campo requerido"), StringLength(30, ErrorMessage = "Cantidad Máxima de 30 caracteres")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Celular", Description = "Celular del usuario")]
        public string Celular { get; set; }

    
        [Required(ErrorMessage = "Campo requerido"), StringLength(200, ErrorMessage = "Máximo 200 caracteres"), Column(TypeName = "varchar")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no valido"), EmailAddress(ErrorMessage = "Correo no valido")]
        [Display(Name = "Correo de usuario", Description = "Correo del usuario (codigo)")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Campo requerida"), StringLength(500), Column(TypeName = "varchar")]
        [Display(Name = "Clave usuario", Description = "Clave del usuario")]
        public string Contrasena { get; set; }


        [Required(ErrorMessage = "Campo requerido"), StringLength(500), Column(TypeName = "varchar")]
        [NotMapped]
        [Display(Name = "Repetir clave usuario", Description = "Repetir clave del usuario")]
        [CustomContrasena]
        public string ContrasenaValidar { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public bool Estado { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        public int TipoUsuarioId { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
