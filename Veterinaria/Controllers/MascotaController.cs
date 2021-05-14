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

namespace Veterinaria.Controllers
{
    public class MascotaController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                             .ConnectionStrings["cnx"].ConnectionString);


        List<Mascota> ListMascota()
        {
            List<Mascota> aMascota = new List<Mascota>();
            SqlCommand cmd = new SqlCommand("SP_LISTAMASCOTA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMascota.Add(new Mascota()
                {         
                    ID_MASC = dr[0].ToString(),
                    NOMBRE = dr[1].ToString(),
                    ANIMAL = dr[2].ToString(),
                    RAZA = dr[3].ToString(),
                    EDAD = dr[4].ToString(),
                    FECHA_NACI = DateTime.Parse(dr[5].ToString()),
                    ID_USU = dr[6].ToString(),
                });
            }
            dr.Close();
            cn.Close();
            return aMascota;
        }

        List<MascotaOriginal> ListMascotaOriginal()
        {
            List<MascotaOriginal> aaMascota = new List<MascotaOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAMASCOTAORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aaMascota.Add(new MascotaOriginal()
                {
                    ID_MASC = dr[0].ToString(),
                    NOMBRE = dr[1].ToString(),
                    ANIMAL = dr[2].ToString(),
                    RAZA = dr[3].ToString(),
                    EDAD = dr[4].ToString(),
                    FECHA_NACI = DateTime.Parse(dr[5].ToString()),
                    ID_USU = dr[6].ToString(),
                });
            }
            dr.Close();
            cn.Close();
            return aaMascota;
        }

        
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

        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOMASCOTA", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                codigo = dr[0].ToString();
            }
            dr.Close();
            cn.Close();

            String s = codigo.Substring(2, 2);
            int s2 = int.Parse(s);
            if (s2 <= 9)
            {
                s2++;
                codigo = "M00" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "M0" + s2;
            }

            return codigo;
        }

        /*VISTAS DEL CONTROLADOR*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadoMascota()
        {
            return View(ListMascota());
        }

        public ActionResult listadoMascotaPag(int p = 0)
        {
            List<Mascota> aMascota = ListMascota();
            int filas = 10;
            int n = aMascota.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aMascota.Skip(p * filas).Take(filas));

        }

        public ActionResult detalleMascota(string id)
        {
            Mascota objU = ListMascota().Where(p => p.ID_MASC == id).FirstOrDefault();
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

        public ActionResult registrarMascota()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.usuario = new SelectList(ListUsuario(), "ID_USU", "NOMBRES");
            return View(new MascotaOriginal());
        }

        [HttpPost]
        public ActionResult registrarMascota(MascotaOriginal obju)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=obju.ID_MASC},
                new SqlParameter(){ParameterName="@NOMBRE",SqlDbType=SqlDbType.VarChar, Value=obju.NOMBRE},
                new SqlParameter(){ParameterName="@ANIMAL",SqlDbType=SqlDbType.VarChar, Value=obju.ANIMAL},
                new SqlParameter(){ParameterName="@RAZA",SqlDbType=SqlDbType.VarChar, Value=obju.RAZA},
                new SqlParameter(){ParameterName="@EDAD",SqlDbType=SqlDbType.VarChar, Value=obju.EDAD},
                new SqlParameter(){ParameterName="@FECHANA",SqlDbType=SqlDbType.Date, Value=obju.FECHA_NACI},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=obju.ID_USU}

            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOMASCOTA", parametros);
            ViewBag.usuario = new SelectList(ListUsuario(), "ID_USU", "NOMBRES");
            return RedirectToAction("listadoMascota");
        }



        public ActionResult editarMascota(string id)
        {
            MascotaOriginal mascO = ListMascotaOriginal().Where(x => x.ID_MASC == id).FirstOrDefault();

            ViewBag.usuario = new SelectList(ListUsuario(), "ID_USU", "NOMBRES");
            return View(mascO);
        }

        [HttpPost]
        public ActionResult editarMascota(MascotaOriginal objU)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=objU.ID_MASC},
                new SqlParameter(){ParameterName="@NOMBRE",SqlDbType=SqlDbType.VarChar, Value=objU.NOMBRE},
                new SqlParameter(){ParameterName="@ANIMAL",SqlDbType=SqlDbType.VarChar, Value=objU.ANIMAL},
                new SqlParameter(){ParameterName="@RAZA",SqlDbType=SqlDbType.VarChar, Value=objU.RAZA},
                new SqlParameter(){ParameterName="@EDAD",SqlDbType=SqlDbType.VarChar, Value=objU.EDAD},
                new SqlParameter(){ParameterName="@FECHANA",SqlDbType=SqlDbType.Date, Value=objU.FECHA_NACI},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objU.ID_USU}

            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOMASCOTA", parametros);
            ViewBag.usuario = new SelectList(ListUsuario(), "ID_USU", "NOMBRES");
            return View("listadoMascota");
        }

        public ActionResult eliminarMascota(string id)
        {
            Mascota objU = ListMascota().Where(x => x.ID_MASC == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=objU.ID_MASC}
            };
            CRUD("SP_ELIMINARMASCOTA", parameters);
            return RedirectToAction("listadoMascota");
        }

       

    }
}