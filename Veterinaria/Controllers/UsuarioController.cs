using System.Web.Mvc;
using Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Veterinaria.Controllers
{
    public class UsuarioController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                             .ConnectionStrings["cnx"].ConnectionString);


        List<Usuario> ListUsuario()
        {
            List<Usuario> aUsuario = new List<Usuario>();
            SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aUsuario.Add(new Usuario()
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
                });
            }
            dr.Close();
            cn.Close();
            return aUsuario;
        }

        List<UsuarioOriginal> ListUsuarioOriginal()
        {
            List<UsuarioOriginal> aaUsuario = new List<UsuarioOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIOORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aaUsuario.Add(new UsuarioOriginal()
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
                });
            }
            dr.Close();
            cn.Close();
            return aaUsuario;
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

        List<Usuario> ListUsuarioxNombre(string nombre)
        {
            List<Usuario> aUsuario = new List<Usuario>();
            SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIOXNOMBRE", cn);
            cmd.Parameters.AddWithValue("@NOM", nombre);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aUsuario.Add(new Usuario()
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
                });
            }
            dr.Close();
            cn.Close();
            return aUsuario;
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

        /*VISTAS DEL CONTROLADOR*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadoUsuario()
        {
            return View(ListUsuario());
        }

        public ActionResult listadoUsuarioPag(int p = 0)
        {
            List<Usuario> aUsuario = ListUsuario();
            int filas = 10;
            int n = aUsuario.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aUsuario.Skip(p * filas).Take(filas));

        }

        public ActionResult detalleUsuario(string id)
        {
            Usuario objU = ListUsuario().Where(p => p.ID_USU == id).FirstOrDefault();
            return View(objU);
        }

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

        public ActionResult listadoUsuarioxNombre()
        {
            return View(ListUsuarioxNombre(""));
        }

        [HttpPost]
        public ActionResult listadoUsuarioxNombre(string nombre)
        {
            return View(ListUsuarioxNombre(nombre));
        }


        public ActionResult registrarUsuario()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.Distrito = new SelectList(ListDistrito(), "ID_DIST", "NOM_DIS");
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
            return RedirectToAction("listadoUsuario");
        }


 
        public ActionResult editarUsuario(string id)
        {
            UsuarioOriginal usuaO = ListUsuarioOriginal().Where(x => x.ID_USU == id).FirstOrDefault();

            ViewBag.distrito = new SelectList(ListDistrito(), "ID_DIST", "NOM_DIS");
            return View(usuaO);
        }

        [HttpPost]
        public ActionResult editarUsuario(UsuarioOriginal objU)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objU.ID_USU},
                new SqlParameter(){ParameterName="@NOMB",SqlDbType=SqlDbType.VarChar, Value=objU.NOMBRES},
                new SqlParameter(){ParameterName="@APE",SqlDbType=SqlDbType.VarChar, Value=objU.APELLIDOS},
                new SqlParameter(){ParameterName="@DIREC",SqlDbType=SqlDbType.VarChar, Value=objU.DIRECCION},
                new SqlParameter(){ParameterName="@DNI",SqlDbType=SqlDbType.VarChar, Value=objU.DNI},
                new SqlParameter(){ParameterName="@USU",SqlDbType=SqlDbType.VarChar, Value=objU.NOMB_USU},
                new SqlParameter(){ParameterName="@PASS",SqlDbType=SqlDbType.VarChar, Value=objU.PASS_USU},
                new SqlParameter(){ParameterName="@CORRE",SqlDbType=SqlDbType.VarChar, Value=objU.CORREO_USU},
                new SqlParameter(){ParameterName="@FECHANA",SqlDbType=SqlDbType.Date, Value=objU.FECHA_NACI},
                new SqlParameter(){ParameterName="@TELE",SqlDbType=SqlDbType.VarChar, Value=objU.TELEFONO},
                new SqlParameter(){ParameterName="@SEXO",SqlDbType=SqlDbType.VarChar, Value=objU.SEXO_USU},
                new SqlParameter(){ParameterName="@IDDIS",SqlDbType=SqlDbType.Char, Value=objU.ID_DIST}

            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOUSUARIO", parametros);
            ViewBag.distrito = new SelectList(ListDistrito(), "ID_DIST", "NOM_DIS");
            return RedirectToAction("listadoUsuario");
        }

        public ActionResult eliminarUsuario(string id)
        {
            Usuario objU = ListUsuario().Where(x => x.ID_USU == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objU.ID_USU}
            };
            CRUD("SP_ELIMINARUSUARIO", parameters);
            return RedirectToAction("listadoUsuario");
        }

    }
}





