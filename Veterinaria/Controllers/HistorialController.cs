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
    public class HistorialController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                              .ConnectionStrings["cnx"].ConnectionString);

        List<Historial> ListHistorial()
        {
            List<Historial> aHistorial = new List<Historial>();
            SqlCommand cmd = new SqlCommand("SP_LISTAHISTORIAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aHistorial.Add(new Historial()
                {
                    ID_HIST = dr[0].ToString(),
                    ID_MASC = dr[1].ToString(),
                    FEC_ATT = DateTime.Parse(dr[2].ToString()),
                    ASUNTO = dr[3].ToString(),
                    DESCRIPCION = dr[4].ToString(),
                    TRATAMIENTO = dr[5].ToString(),

                });
            }
            dr.Close();
            cn.Close();
            return aHistorial;
        }

        List<HistorialOriginal> ListHistorialOriginal()
        {
            List<HistorialOriginal> aHistorial = new List<HistorialOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAHISTORIALORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aHistorial.Add(new HistorialOriginal()
                {
                    ID_HIST = dr[0].ToString(),
                    ID_MASC = dr[1].ToString(),
                    FEC_ATT = DateTime.Parse(dr[2].ToString()),
                    ASUNTO = dr[3].ToString(),
                    DESCRIPCION = dr[4].ToString(),
                    TRATAMIENTO = dr[5].ToString(),

                });
            }
            dr.Close();
            cn.Close();
            return aHistorial;
        }

        List<HistorialOriginal> ListHistorialxMascota(string codigo)
        {
            HistorialOriginal mascO = ListHistorialOriginal().Where(x => x.ID_MASC == codigo).FirstOrDefault();
            List<HistorialOriginal> aPedido = new List<HistorialOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAHISTORIALXMASCOTA", cn);
            cmd.Parameters.AddWithValue("@MASC", codigo);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedido.Add(new HistorialOriginal()
                {
                    ID_HIST = dr[0].ToString(),
                    ID_MASC = dr[1].ToString(),
                    FEC_ATT = DateTime.Parse(dr[2].ToString()),
                    ASUNTO = dr[3].ToString(),
                    DESCRIPCION = dr[4].ToString(),
                    TRATAMIENTO = dr[5].ToString(),
                });
            }
            dr.Close();
            cn.Close();
            return aPedido;
        }

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

        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOHISOTRIAL", cn);
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
                codigo = "H000000" + s2;
            }
            else if (s2 >= 9)
            {
                s2++;
                codigo = "H00000" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "H0000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "H000" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "H00" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "H0" + s2;
            }
            else if (s2 >= 999999)
            {
                s2++;
                codigo = "H" + s2;
            }
            return codigo;
        }

        public ActionResult listadoHistorial()
        {
            return View(ListHistorial());
        }

        public ActionResult listadoHistorialPag(int p = 0)
        {
            List<Historial> aMascota = ListHistorial();
            int filas = 10;
            int n = aMascota.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aMascota.Skip(p * filas).Take(filas));
        }

        public ActionResult listadoHistorialxMascota(string id)
        {
            HistorialOriginal mascO = ListHistorialOriginal().Where(x => x.ID_MASC == id).FirstOrDefault();
            return View(ListHistorialxMascota(id));
        }

        public ActionResult detalleHistorial(string id)
        {
            Historial objU = ListHistorial().Where(p => p.ID_HIST == id).FirstOrDefault();
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

        public ActionResult registrarHistorial()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.mascota = new SelectList(ListMascota(), "ID_MASC", "NOMBRE");
            return View(new HistorialOriginal());
        }

        [HttpPost]
        public ActionResult registrarHistorial(HistorialOriginal obju)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDHIST",SqlDbType=SqlDbType.Char, Value=obju.ID_HIST},
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=obju.ID_MASC},
                new SqlParameter(){ParameterName="@FECAT",SqlDbType=SqlDbType.Date, Value=obju.FEC_ATT},
                new SqlParameter(){ParameterName="@ASUN",SqlDbType=SqlDbType.VarChar, Value=obju.ASUNTO},
                new SqlParameter(){ParameterName="@DESC",SqlDbType=SqlDbType.VarChar, Value=obju.DESCRIPCION},
                new SqlParameter(){ParameterName="@TRAT",SqlDbType=SqlDbType.VarChar, Value=obju.TRATAMIENTO}              
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOHISTORIAL", parametros);
            ViewBag.mascota = new SelectList(ListMascota(), "ID_MASC", "NOMBRE");
            return RedirectToAction("listadoHistorial");
        }

        public ActionResult editarHistorial(string id)
        {
            HistorialOriginal mascO = ListHistorialOriginal().Where(x => x.ID_HIST == id).FirstOrDefault();
            ViewBag.mascota = new SelectList(ListMascota(), "ID_MASC", "NOMBRE");
            return View(mascO);
        }

        [HttpPost]
        public ActionResult editarHistorial(HistorialOriginal obju)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDHIST",SqlDbType=SqlDbType.Char, Value=obju.ID_HIST},
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=obju.ID_MASC},
                new SqlParameter(){ParameterName="@FECAT",SqlDbType=SqlDbType.Date, Value=obju.FEC_ATT},
                new SqlParameter(){ParameterName="@ASUN",SqlDbType=SqlDbType.VarChar, Value=obju.ASUNTO},
                new SqlParameter(){ParameterName="@DESC",SqlDbType=SqlDbType.VarChar, Value=obju.DESCRIPCION},
                new SqlParameter(){ParameterName="@TRAT",SqlDbType=SqlDbType.VarChar, Value=obju.TRATAMIENTO}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOHISTORIAL", parametros);
            ViewBag.mascota = new SelectList(ListMascota(), "ID_MASC", "NOMBRE");
            return RedirectToAction("listadoHistorial");
        }

        public ActionResult eliminarHistorial(string id)
        {
            Historial objU = ListHistorial().Where(x => x.ID_HIST == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDHIST",SqlDbType=SqlDbType.Char, Value=objU.ID_HIST}
            };
            CRUD("SP_ELIMINARHISTORIAL", parameters);
            return RedirectToAction("listadoHistorial");
        }

    }
}