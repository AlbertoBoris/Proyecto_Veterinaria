using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services.Description;
using System.Web.Security;

namespace Veterinaria.Controllers
{
    public class HomeController : Controller
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager
                            .ConnectionStrings["cnx"].ConnectionString);

        string CRUD(string proceso, List<SqlParameter> p)
        {
            string mensaje = "No se registro";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(proceso, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(p.ToArray());
                int n = cmd.ExecuteNonQuery();
                mensaje = n + " Registro actualizado";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }

        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOUSUARIO", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                codigo = dr[0].ToString();
            }
            dr.Close();
            cn.Close();

            string s = codigo.Substring(1, 7);
            int s2 = int.Parse(s);
            if (s2 < 9)
            {
                s2++;
                codigo = "U000000" + s2;
            }
            else if (s2 >= 9)
            {
                s2++;
                codigo = "U00000" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "U0000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "U000" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "U00" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "U0" + s2;
            }
            else if (s2 >= 999999)
            {
                s2++;
                codigo = "U" + s2;
            }

            return codigo;
        }

        List<Distrito> ListDistrito()
        {
            List<Distrito> aDistrito = new List<Distrito>();
            SqlCommand cmd = new SqlCommand("SP_LISTADISTRITOS", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distrito objP = new Distrito()
                {
                    ID_DIST = dr[0].ToString(),
                    NOM_DIS = dr[1].ToString(),
                };
                aDistrito.Add(objP);
            }

            dr.Close();
            cn.Close();
            return aDistrito;
        }

        /*VISTAS DE HOMECONTROLLER*/

        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string idUsu, string password)
        {
            if (!string.IsNullOrEmpty(idUsu) && !string.IsNullOrEmpty(password))
            {
                SqlCommand cmd = new SqlCommand("SP_INGRESOUSUARIO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDUSU", idUsu);
                cmd.Parameters.AddWithValue("@PASSUSU", password);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Usuario usu = null;
                while (dr.Read())
                {
                    usu = new Usuario
                    {
                        ID_USU = dr[0].ToString(),
                        NOMBRES = dr[1].ToString(),
                        APELLIDOS = dr[2].ToString(),
                        DIRECCION = dr[3].ToString(),
                        DNI = dr[4].ToString(),
                        NOMB_USU = dr[5].ToString(),
                        PASS_USU = dr[6].ToString(),
                        CORREO_USU = dr[7].ToString(),
                        FECHA_NACI = DateTime.Parse(dr[8].ToString()),
                        TELEFONO = dr[9].ToString(),
                        SEXO_USU = dr[10].ToString(),
                        ID_DIST = dr[11].ToString(),
                    };
                }

                if (usu == null)
                {
                    return RedirectToAction("Index", new { message = "Usuario o Contraseña Incorrecto" });
                }

                if (usu.ID_USU != null)
                {
                    if (usu.ID_USU == "U0000001")
                    {
                        FormsAuthentication.SetAuthCookie(usu.ID_USU, true);
                        return RedirectToAction("Index", "Profile", new { n = usu.NOMB_USU, i = usu.ID_USU });
                    }
                    if (usu.ID_USU == "")
                    {
                        return RedirectToAction("Index", new { message = "Llene los campos" });
                    }                  
                    else
                    {
                        FormsAuthentication.SetAuthCookie(usu.ID_USU, true);
                        return RedirectToAction("IndexUsuario", "Profile", new { n = usu.NOMB_USU, i = usu.ID_USU });
                    }                  
                }
                else
                {
                    return RedirectToAction("Index", new { message = "No se encontro usuario" });
                }
            }
            else
            {
                return RedirectToAction("Index", new { message = "Llene los campos" });
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult registrarUsuario()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.distrito = new SelectList(ListDistrito(), "ID_DIST", "NOM_DIS");
            return View(new UsuarioOriginal());
        }

        [HttpPost]
        public ActionResult registrarUsuario(UsuarioOriginal obju)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=obju.ID_USU},
                new SqlParameter(){ParameterName="@NOMB",SqlDbType=SqlDbType.VarChar, Value=obju.NOMBRES},
                new SqlParameter(){ParameterName="@APE",SqlDbType=SqlDbType.VarChar, Value=obju.APELLIDOS},
                new SqlParameter(){ParameterName="@DIREC",SqlDbType=SqlDbType.VarChar, Value=obju.DIRECCION},
                new SqlParameter(){ParameterName="@DNI",SqlDbType=SqlDbType.VarChar, Value=obju.DNI},
                new SqlParameter(){ParameterName="@USU",SqlDbType=SqlDbType.VarChar, Value=obju.NOMB_USU},
                new SqlParameter(){ParameterName="@PASS",SqlDbType=SqlDbType.VarChar, Value=obju.PASS_USU},
                new SqlParameter(){ParameterName="@CORRE",SqlDbType=SqlDbType.VarChar, Value=obju.CORREO_USU},
                new SqlParameter(){ParameterName="@FECHANA",SqlDbType=SqlDbType.Date, Value=obju.FECHA_NACI},
                new SqlParameter(){ParameterName="@TELE",SqlDbType=SqlDbType.VarChar, Value=obju.TELEFONO},
                new SqlParameter(){ParameterName="@SEXO",SqlDbType=SqlDbType.VarChar, Value=obju.SEXO_USU},
                new SqlParameter(){ParameterName="@IDDIS",SqlDbType=SqlDbType.Char, Value=obju.ID_DIST}

            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOUSUARIO", parametros);
            return RedirectToAction("Index");
        }

    }
}