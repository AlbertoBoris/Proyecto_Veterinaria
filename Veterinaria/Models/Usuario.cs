using System;
using System.ComponentModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Models
{
    public class Usuario
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("CODIGO")]
        public string ID_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre del Usuario")]
        [RegularExpression("^[a-zA-Z]{1,40}$", ErrorMessage = "Solo letras en el Campo Nombre")]

        [DisplayName("NOMBRES")]
        public string NOMBRES { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Apellido del Usuario")]
        [RegularExpression("^[a-zA-Z]{1,40}$", ErrorMessage = "Solo letras en el Campo Apellido")]

        [DisplayName("APELLIDOS")]
        public string APELLIDOS { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Direccion del Usuario")]
        [RegularExpression("^[a-zA-Z0-9]{1,40}$", ErrorMessage = "Ingrese una dirreccion con menos de 40 letras")]

        [DisplayName("DIRECCION")]
        public string DIRECCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese DNI del Usuario")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El DNI  es de 8 digitos")]
        [StringLength(8, ErrorMessage = "El DNI  es de 8 digitos")]
        [DisplayName("DNI")]
        public string DNI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nickname del Usuario")]
        [RegularExpression("^[a-zA-Z0-9]{1,10}$", ErrorMessage = "El Nickname debe ser menos de 10 letras")]

        [DisplayName("NICKNAME")]
        public string NOMB_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Contraseña del Usuario")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "La contraseña debe ser de al menos 4 caracteres maximo 10")]
        [DisplayName("CONTRASEÑA")]
        public string PASS_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Correo del Usuario")]
        [EmailAddress]
        [DisplayName("CORREO")]
        public string CORREO_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Nacimiento del Usuario")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("FECHA NACIMIENTO")]
        public DateTime FECHA_NACI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Telefono del Usuario")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "El telefono  es de 9 digitos")]
        [StringLength(9, ErrorMessage = "El telefono  es de 9 digitos")]
        [DisplayName("TELEFONO")]
        public string TELEFONO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Sexo del Usuario")]
        [RegularExpression("^[a-zA-Z]{1,40}$", ErrorMessage = "Solo letras en el Campo Sexo")]

        [DisplayName("SEXO")]
        public string SEXO_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Distrito del Usuario")]
        [DisplayName("DISTRITO")]
        public string ID_DIST { get; set; }
    }
}