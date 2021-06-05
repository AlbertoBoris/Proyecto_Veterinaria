using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{ 
    public class UsuarioOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("Codigo")]
        public string ID_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre del Usuario")]
        [RegularExpression("^[a-zA-Z]{1,30}$", ErrorMessage = "Solo letras en el Campo Nombre")]

        [DisplayName("Nombre")]
        public string NOMBRES { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Apellido del Usuario")]
        [RegularExpression("^[a-zA-Z]{1,30}$", ErrorMessage = "Solo letras en el Campo Apellido")]

        [DisplayName("Apellido")]
        public string APELLIDOS { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Direccion del Usuario")]
        [RegularExpression("^[a-zA-Z0-9]{1,60}$", ErrorMessage = "Ingrese una dirreccion con menos de 40 letras")]

        [DisplayName("Direccion")]
        public string DIRECCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese DNI del Usuario")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El DNI  es de 8 digitos")]
        [StringLength(8, ErrorMessage = "El DNI  es de 8 digitos")]
        [DisplayName("DNI")]
        public string DNI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nickname del Usuario")]
        [RegularExpression("^[a-zA-Z0-9]{1,15}$", ErrorMessage = "El Nickname debe ser menos de 10 letras")]

        [DisplayName("Apodo")]
        public string NOMB_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Contraseña del Usuario")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "La contraseña debe ser de al menos 4 caracteres maximo 10")]
        [DisplayName("Contraseña")]
        public string PASS_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Correo del Usuario")]
        [EmailAddress]
        [DisplayName("Correo")]
        public string CORREO_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Nacimiento (yyyy-MM-dd)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha Nacimiento")]
        public DateTime FECHA_NACI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Telefono del Usuario")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "El telefono  es de 9 digitos")]
        [StringLength(9, ErrorMessage = "El telefono  es de 9 digitos")]
        [DisplayName("Telefono")]
        public string TELEFONO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Sexo del Usuario")]
        [RegularExpression("^[a-zA-Z]{1,20}$", ErrorMessage = "Solo letras en el Campo Sexo")]
        [DisplayName("Sexo")]
        public string SEXO_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Distrito del Usuario")]
        [DisplayName("Distrito")]
        public string ID_DIST { get; set; }
    }
}