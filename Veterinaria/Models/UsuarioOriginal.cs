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
        [DisplayName("CODIGO")]
        public string ID_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre del Usuario")]
        [DisplayName("NOMBRES")]
        public string NOMBRES { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Apellido del Usuario")]
        [DisplayName("APELLIDOS")]
        public string APELLIDOS { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Direccion del Usuario")]
        [DisplayName("DIRECCION")]
        public string DIRECCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese DNI del Usuario")]
        [DisplayName("DNI")]
        public string DNI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nickname del Usuario")]
        [DisplayName("NICKNAME")]
        public string NOMB_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Contraseña del Usuario")]
        [DisplayName("CONTRASEÑA")]
        public string PASS_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Correo del Usuario")]
        [EmailAddress]
        [DisplayName("CORREO")]
        public string CORREO_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Nacimiento del Usuario")]
        [DisplayName("FECHA NACIMIENTO")]
        public DateTime FECHA_NACI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Telefono del Usuario")]
        [DisplayName("TELEFONO")]
        public string TELEFONO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Sexo del Usuario")]
        [DisplayName("SEXO")]
        public string SEXO_USU { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Distrito del Usuario")]
        [DisplayName("DISTRITO")]
        public string ID_DIST { get; set; }
    }
}