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
    public class CitaController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                               .ConnectionStrings["cnx"].ConnectionString);

        List<Cita> ListCita()
        {
            List<Cita> aCita = new List<Cita>();
            SqlCommand cmd = new SqlCommand("SP_LISTACITA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCita.Add(new Cita()
                {
                    ID_CITA = dr[0].ToString(),
                    FECHA_REG = DateTime.Parse(dr[1].ToString()),
                    ID_USU = dr[2].ToString(),
                    ID_AREA = dr[3].ToString(),
                    ID_MASC = dr[4].ToString(),
                    ID_HORAR = dr[5].ToString(),
                    ID_HORA = dr[6].ToString(),
                    ID_ESTA = dr[7].ToString(),
                    IMPORTE = double.Parse(dr[8].ToString()),

                });
            }
            dr.Close();
            cn.Close();
            return aCita;
        }

        List<CitaOriginal> ListCitaOriginal()
        {
            List<CitaOriginal> aCita = new List<CitaOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTACITAORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aCita.Add(new CitaOriginal()
                {
                    ID_CITA = dr[0].ToString(),
                    FECHA_REG = DateTime.Parse(dr[1].ToString()),
                    ID_USU = dr[2].ToString(),
                    ID_AREA = dr[3].ToString(),
                    ID_MASC = dr[4].ToString(),
                    ID_HORAR = dr[5].ToString(),
                    ID_HORA = dr[6].ToString(),
                    ID_ESTA = dr[7].ToString(),
                    IMPORTE = double.Parse(dr[8].ToString()),

                });
            }
            dr.Close();
            cn.Close();
            return aCita;
        }

        List<Area> ListArea()
        {
            List<Area> aArea = new List<Area>();
            SqlCommand cmd = new SqlCommand("SP_LISTAAREA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aArea.Add(new Area()
                {
                    ID_AREA = dr[0].ToString(),
                    NOMB_AREA = dr[1].ToString(),
                });
            }
            dr.Close();
            cn.Close();
            return aArea;
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

        List<Hora> ListHora()
        {
            List<Hora> aDistrito = new List<Hora>();
            SqlCommand cmd = new SqlCommand("SP_LISTAHORA", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Hora objP = new Hora()
                {
                    ID_HORA = dr[0].ToString(),
                    NOM_HOR = dr[1].ToString(),
                };
                aDistrito.Add(objP);
            }

            dr.Close();
            cn.Close();
            return aDistrito;
        }

        List<Horario> ListHorar()
        {
            List<Horario> aDistrito = new List<Horario>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOHORARIO", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Horario objP = new Horario()
                {
                    ID_HORAR = dr[0].ToString(),
                    NOMB_HORA = dr[1].ToString(),
                };
                aDistrito.Add(objP);
            }

            dr.Close();
            cn.Close();
            return aDistrito;
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

        List<Estado> ListEstado()
        {
            List<Estado> aEstado = new List<Estado>();
            SqlCommand cmd = new SqlCommand("SP_LISTAESTADO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aEstado.Add(new Estado()
                {
                    ID_ESTA = dr[0].ToString(),
                    NOMB_ESTA = dr[1].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aEstado;
        }

        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOCITA", cn);
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
                codigo = "C000000" + s2;
            }
            else if (s2 >= 9)
            {
                s2++;
                codigo = "C00000" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "C0000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "C000" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "C00" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "C0" + s2;
            }
            else if (s2 >= 999999)
            {
                s2++;
                codigo = "C" + s2;
            }
            return codigo;
        }

        public ActionResult listadoCita()
        {
            return View(ListCita());
        }

        public ActionResult detalleCita(string id)
        {
            Cita objU = ListCita().Where(p => p.ID_CITA == id).FirstOrDefault();
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

        public ActionResult registrarCita()
        {
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.usuario = new SelectList(ListUsuario(), "ID_USU", "NOMBRES");
            ViewBag.mascota = new SelectList(ListMascota(), "ID_MASC", "NOMBRE");
            ViewBag.area = new SelectList(ListArea(), "ID_AREA", "NOMB_AREA");
            ViewBag.hora = new SelectList(ListHora(), "ID_HORA", "NOM_HOR");
            ViewBag.horario = new SelectList(ListHorar(), "ID_HORAR", "NOMB_HORA");
            ViewBag.estado = new SelectList(ListEstado(), "ID_ESTA", "NOMB_ESTA");

            return View(new CitaOriginal());
        }

        [HttpPost]
        public ActionResult registrarCita(CitaOriginal obju)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDCIT",SqlDbType=SqlDbType.Char, Value=obju.ID_CITA},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.DateTime, Value=obju.FECHA_REG},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=obju.ID_USU},
                new SqlParameter(){ParameterName="@AREA",SqlDbType=SqlDbType.Char, Value=obju.ID_AREA},
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=obju.ID_MASC},
                new SqlParameter(){ParameterName="@HORAR",SqlDbType=SqlDbType.Char, Value=obju.ID_HORAR},
                new SqlParameter(){ParameterName="@HORA",SqlDbType=SqlDbType.Char, Value=obju.ID_HORA},
                new SqlParameter(){ParameterName="@IDESTA",SqlDbType=SqlDbType.Char, Value=obju.ID_ESTA},
                new SqlParameter(){ParameterName="@IMPOR",SqlDbType=SqlDbType.SmallMoney, Value=obju.IMPORTE}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOCITA", parametros);
            return RedirectToAction("listadoCita");
        }

        public ActionResult editarCita(string id)
        {
            CitaOriginal usuaO = ListCitaOriginal().Where(x => x.ID_CITA == id).FirstOrDefault();

            ViewBag.codigo = codigoCorrelativo();
            ViewBag.usuario = new SelectList(ListUsuario(), "ID_USU", "NOMBRES");
            ViewBag.mascota = new SelectList(ListMascota(), "ID_MASC", "NOMBRE");
            ViewBag.area = new SelectList(ListArea(), "ID_AREA", "NOMB_AREA");
            ViewBag.hora = new SelectList(ListHora(), "ID_HORA", "NOM_HOR");
            ViewBag.horario = new SelectList(ListHorar(), "ID_HORAR", "NOMB_HORA");
            ViewBag.estado = new SelectList(ListEstado(), "ID_ESTA", "NOMB_ESTA");

            return View(usuaO);
        }

        [HttpPost]
        public ActionResult editarCita(CitaOriginal objU)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDCIT",SqlDbType=SqlDbType.Char, Value=objU.ID_CITA},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.DateTime, Value=objU.FECHA_REG},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objU.ID_USU},
                new SqlParameter(){ParameterName="@AREA",SqlDbType=SqlDbType.Char, Value=objU.ID_AREA},
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=objU.ID_MASC},
                new SqlParameter(){ParameterName="@HORAR",SqlDbType=SqlDbType.Char, Value=objU.ID_HORAR},
                new SqlParameter(){ParameterName="@HORA",SqlDbType=SqlDbType.Char, Value=objU.ID_HORA},
                new SqlParameter(){ParameterName="@IDESTA",SqlDbType=SqlDbType.Char, Value=objU.ID_ESTA},
                new SqlParameter(){ParameterName="@IMPOR",SqlDbType=SqlDbType.SmallMoney, Value=objU.IMPORTE}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOCITA", parametros);
            return RedirectToAction("listadoCita");
        }

        public ActionResult eliminarCita(string id)
        {
            Cita objU = ListCita().Where(x => x.ID_CITA == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDCITA",SqlDbType=SqlDbType.Char, Value=objU.ID_CITA}
            };
            CRUD("SP_ELIMINARCITA", parameters);
            return RedirectToAction("listadoCita");
        }

    }
}
