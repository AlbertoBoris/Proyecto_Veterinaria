using Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.IO;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using System.Web.Services.Description;

namespace Veterinaria.Controllers
{
    public class ServicioController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                            .ConnectionStrings["cnx"].ConnectionString);

        List<Servicio> ListServicio()
        {
            List<Servicio> aServicio = new List<Servicio>();
            SqlCommand cmd = new SqlCommand("SP_LISTASERVICIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aServicio.Add(new Servicio()
                {
                    ID_SERV = dr[0].ToString(),
                    NOMB_SERV = dr[1].ToString(),
                    PRECIO_SERV = double.Parse(dr[2].ToString()),
                    DESC_SERV = dr[3].ToString(),
                    ID_HORAR = dr[4].ToString(),
                    FECH_SERV = DateTime.Parse(dr[5].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aServicio;
        }

        List<ServicioOriginal> ListServicioOriginal()
        {
            List<ServicioOriginal> aServicio = new List<ServicioOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTASERVICIOORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aServicio.Add(new ServicioOriginal()
                {
                    ID_SERV = dr[0].ToString(),
                    NOMB_SERV = dr[1].ToString(),
                    PRECIO_SERV = double.Parse(dr[2].ToString()),
                    DESC_SERV = dr[3].ToString(),
                    ID_HORAR = dr[4].ToString(),
                    FECH_SERV = DateTime.Parse(dr[5].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aServicio;
        }

        List<Horario> ListHorario()
        {
            List<Horario> aHorario = new List<Horario>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOHORARIO", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Horario objH = new Horario()
                {
                    ID_HORAR = dr[0].ToString(),
                    NOMB_HORA = dr[1].ToString(),
                };
                aHorario.Add(objH);
            }

            dr.Close();
            cn.Close();
            return aHorario;
        }

        List<Servicio> ListServicioxNombre(string nombre)
        {
            List<Servicio> aServicio = new List<Servicio>();
            SqlCommand cmd = new SqlCommand("SP_LISTASERVICIOXNOMBRE", cn);
            cmd.Parameters.AddWithValue("@NOM", nombre);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aServicio.Add(new Servicio()
                {
                    ID_SERV = dr[0].ToString(),
                    NOMB_SERV = dr[1].ToString(),
                    PRECIO_SERV = double.Parse(dr[2].ToString()),
                    DESC_SERV = dr[3].ToString(),
                    ID_HORAR = dr[4].ToString(),
                    FECH_SERV = DateTime.Parse(dr[5].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aServicio;
        }

        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOSERVICIO", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                codigo = dr[0].ToString();
            }
            dr.Close();
            cn.Close();

            string s = codigo.Substring(2, 2);
            int s2 = int.Parse(s);
            if (s2 >= 9)
            {
                s2++;
                codigo = "S0" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "S" + s2;
            }


            return codigo;
        }

        /*Vistas del controlador Servicio*/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listadoServicio()
        {
            return View(ListServicio());
        }

        public ActionResult listadoServicioPag(int p = 0)
        {
            List<Servicio> aServicio = ListServicio();
            int filas = 10;
            int n = aServicio.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aServicio.Skip(p * filas).Take(filas));

        }

        public ActionResult detalleServicio(string id)
        {
            Servicio objP = ListServicio().Where(p => p.ID_SERV == id).FirstOrDefault();
            return View(objP);
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

        public ActionResult registroServicio()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.horario = new SelectList(ListHorario(), "ID_HORAR", "NOMB_HORA");
            return View(new ServicioOriginal());
        }

        [HttpPost]
        public ActionResult registroServicio(ServicioOriginal objP)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDSER",SqlDbType=SqlDbType.Char, Value=objP.ID_SERV},
                new SqlParameter(){ParameterName="@NOMSER",SqlDbType=SqlDbType.VarChar, Value=objP.NOMB_SERV},
                new SqlParameter(){ParameterName="@PRESER",SqlDbType=SqlDbType.SmallMoney, Value=objP.PRECIO_SERV},
                new SqlParameter(){ParameterName="@DESCSER",SqlDbType=SqlDbType.VarChar, Value=objP.DESC_SERV},
                new SqlParameter(){ParameterName="@IDHORA",SqlDbType=SqlDbType.Char, Value=objP.ID_HORAR},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.Date, Value=objP.FECH_SERV},
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOSERVICIO", parameters);
            ViewBag.horario = new SelectList(ListHorario(), "ID_HORAR", "NOMB_HORA");
            return RedirectToAction("listadoServicio");
        }

        public ActionResult editarServicio(string id)
        {
            ServicioOriginal serO = ListServicioOriginal().Where(x => x.ID_SERV == id).FirstOrDefault();

            ViewBag.horario = new SelectList(ListHorario(), "ID_HORAR", "NOMB_HORA");
            return View(serO);
        }

        [HttpPost]
        public ActionResult editarServicio(ServicioOriginal objP, HttpPostedFileBase f)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDSER",SqlDbType=SqlDbType.Char, Value=objP.ID_SERV},
                new SqlParameter(){ParameterName="@NOMSER",SqlDbType=SqlDbType.VarChar, Value=objP.NOMB_SERV},
                new SqlParameter(){ParameterName="@PRESER",SqlDbType=SqlDbType.SmallMoney, Value=objP.PRECIO_SERV},
                new SqlParameter(){ParameterName="@DESCSER",SqlDbType=SqlDbType.VarChar, Value=objP.DESC_SERV},
                new SqlParameter(){ParameterName="@IDHORA",SqlDbType=SqlDbType.Char, Value=objP.ID_HORAR},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.Date, Value=objP.FECH_SERV},
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOSERVICIO", parameters);
            ViewBag.horario = new SelectList(ListHorario(), "ID_HORAR", "NOMB_HORA");
            return RedirectToAction("listadoServicio");
        }

        public ActionResult eliminarServicio(string id)
        {
            Servicio objP = ListServicio().Where(x => x.ID_SERV == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDSER",SqlDbType=SqlDbType.Char, Value=objP.ID_SERV},
            };
            CRUD("SP_ELIMINARSERVICIO", parameters);
            return RedirectToAction("listadoServicio");
        }

        public ActionResult listadoServicioxNombre()
        {
            return View(ListServicioxNombre(""));
        }

        [HttpPost]
        public ActionResult listadoServicioxNombre(string nombre)
        {
            return View(ListServicioxNombre(nombre));
        }

    }
}