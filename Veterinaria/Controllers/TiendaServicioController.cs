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
    public class PedidoSerController : Controller
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
            SqlCommand cmd = new SqlCommand("SP_LISTAHORA", cn);
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

        List<Servicio> ListServicio()
        {
            List<Servicio> aServicio = new List<Servicio>();
            SqlCommand cmd = new SqlCommand("SP_LISTASERVICIOSEMIORI", cn);
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
                    ID_HORAROR = dr[4].ToString(),
                    ID_HORAR = dr[5].ToString(),
                    FECH_SERV = DateTime.Parse(dr[6].ToString())
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

        List<PedidoSer> ListPedidoSer()
        {
            List<PedidoSer> aPedido = new List<PedidoSer>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOSER", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedido.Add(new PedidoSer()
                {
                    ID_PEDI = dr[0].ToString(),
                    FECHA_PEDI = DateTime.Parse(dr[1].ToString()),
                    ID_USU = dr[2].ToString(),
                    ID_SERV = dr[3].ToString(),
                    ID_ESTA = dr[4].ToString(),
                    ID_HORAR = dr[5].ToString(),
                    ID_HORA = dr[6].ToString(),
                    IMPORTE = double.Parse(dr[7].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aPedido;
        }

        List<PedidoSerOriginal> ListPedidoSerOriginal()
        {
            List<PedidoSerOriginal> aPedido = new List<PedidoSerOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOSERORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedido.Add(new PedidoSerOriginal()
                {
                    ID_PEDI = dr[0].ToString(),
                    FECHA_PEDI = DateTime.Parse(dr[1].ToString()),
                    ID_USU = dr[2].ToString(),
                    ID_SERV = dr[3].ToString(),
                    ID_ESTA = dr[4].ToString(),
                    ID_HORAR = dr[5].ToString(),
                    ID_HORA = dr[6].ToString(),
                    IMPORTE = double.Parse(dr[7].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aPedido;
        }


        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOPEDIDOSER", cn);
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
                codigo = "P000000" + s2;
            }
            else if (s2 >= 9)
            {
                s2++;
                codigo = "P00000" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "P0000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "P000" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "P00" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "P0" + s2;
            }
            else if (s2 >= 999999)
            {
                s2++;
                codigo = "P" + s2;
            }

            return codigo;
        }



        /*Vistas del controlador Producto*/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listadoProducto()
        {
            return View(ListServicio());
        }

        public ActionResult listadoServicioPag(int p = 0)
        {
            List<Servicio> aProducto = ListServicio();
            int filas = 8;
            int n = aProducto.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aProducto.Skip(p * filas).Take(filas));

        }


        string CRUD(string proceso, List<SqlParameter> p)
        {
            string mensaje = "No se registró";
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

        public ActionResult registroServicio(string id, string H, double p)
        {
            DateTime now = DateTime.Now;
            ViewBag.fecha = now.Year+ "/" + now.Month + "/" + now.Day;
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.hora = new SelectList(ListHora(), "ID_HORA", "NOM_HOR");
            ViewBag.Id = Session["IdUsuario"].ToString();
            ViewBag.serv = id;
            ViewBag.horar = H;
            ViewBag.prec = p;

            return View(new PedidoSerOriginal());
        }

        [HttpPost]
        public ActionResult registroServicio(PedidoSerOriginal objP)
        {

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPED",SqlDbType=SqlDbType.Char, Value=objP.ID_PEDI},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.DateTime, Value=objP.FECHA_PEDI},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objP.ID_USU},
                new SqlParameter(){ParameterName="@IDSERV",SqlDbType=SqlDbType.Char, Value=objP.ID_SERV},
                new SqlParameter(){ParameterName="@IDESTA",SqlDbType=SqlDbType.Char, Value=objP.ID_ESTA},
                new SqlParameter(){ParameterName="@HORAR",SqlDbType=SqlDbType.Char, Value=objP.ID_HORAR},
                new SqlParameter(){ParameterName="@HORA",SqlDbType=SqlDbType.Char, Value=objP.ID_HORA},
                new SqlParameter(){ParameterName="@IMPOR",SqlDbType=SqlDbType.SmallMoney, Value=objP.IMPORTE}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPEDIDOSER", parameters);
            return RedirectToAction("successPedido");
        }
        public ActionResult successPedido()
        {
            return View("successPedido");
        }
    }
}
