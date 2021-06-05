using Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;



namespace Veterinaria.Controllers
{
    public class TiendaProductoController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                           .ConnectionStrings["cnx"].ConnectionString);

        string sessionId = System.Web.HttpContext.Current.Session.SessionID.ToString();

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

        List<Producto> ListProducto()
        {
            List<Producto> aProducto = new List<Producto>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProducto.Add(new Producto()
                {
                    ID_PROD = dr[0].ToString(),
                    NOMB_PROD = dr[1].ToString(),
                    PREC_PROD = double.Parse(dr[2].ToString()),
                    STOCK = int.Parse(dr[3].ToString()),
                    SERIE = dr[4].ToString(),
                    MARCA = dr[5].ToString(),
                    PROV_PROD = dr[6].ToString(),
                    DESC_PROD = dr[7].ToString(),
                    DESC_HTML = dr[8].ToString(),
                    FOTO = dr[9].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aProducto;
        }

        List<ProductoOriginal> ListProductoOriginal()
        {
            List<ProductoOriginal> aProducto = new List<ProductoOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPRODUCTOORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProducto.Add(new ProductoOriginal()
                {
                    ID_PROD = dr[0].ToString(),
                    NOMB_PROD = dr[1].ToString(),
                    PREC_PROD = double.Parse(dr[2].ToString()),
                    STOCK = int.Parse(dr[3].ToString()),
                    SERIE = dr[4].ToString(),
                    MARCA = dr[5].ToString(),
                    PROV_PROD = dr[6].ToString(),
                    DESC_PROD = dr[7].ToString(),
                    DESC_HTML = dr[8].ToString(),
                    FOTO = dr[9].ToString()
                });
            }
            dr.Close();
            cn.Close();
            return aProducto;
        }

        List<PedidoProd> ListPedidoProd()
        {
            List<PedidoProd> aPedido = new List<PedidoProd>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOPROD", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedido.Add(new PedidoProd()
                {
                    ID_PEDI = dr[0].ToString(),
                    FECHA_PEDI = DateTime.Parse(dr[1].ToString()),
                    ID_USU = dr[2].ToString(),
                    ID_PROD = dr[3].ToString(),
                    ID_ESTA = dr[4].ToString(),
                    CONTADOR = int.Parse(dr[5].ToString()),
                    IMPORTE = double.Parse(dr[6].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aPedido;
        }

        List<PedidoProdOriginal> ListPedidoProdOriginal()
        {
            List<PedidoProdOriginal> aPedido = new List<PedidoProdOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOPRODORIGINAL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedido.Add(new PedidoProdOriginal()
                {
                    ID_PEDI = dr[0].ToString(),
                    FECHA_PEDI = DateTime.Parse(dr[1].ToString()),
                    ID_USU = dr[2].ToString(),
                    ID_PROD = dr[3].ToString(),
                    ID_ESTA = dr[4].ToString(),
                    CONTADOR = int.Parse(dr[5].ToString()),
                    IMPORTE = double.Parse(dr[6].ToString())
                });
            }
            dr.Close();
            cn.Close();
            return aPedido;
        }


        string codigoCorrelativo()
        {
            string codigo = null;
            SqlCommand cmd = new SqlCommand("SP_ULTIMOCODIGOPEDIDOPROD", cn);
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
        public ActionResult listadoProducto()
        {
            return View(ListProducto());
        }

        public ActionResult listadoProductoPag(int p = 0)
        {
            List<Producto> aProducto = ListProducto();
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

        public ActionResult registroPedido(string id, double p, string F)
        {
            ViewBag.prod = id;
            ViewBag.prec = p;
            ViewBag.foto = F;
            ViewBag.fecha = DateTime.UtcNow.Date;
            ViewBag.codigo = codigoCorrelativo();            
            ViewBag.Id = Session["IdUsuario"].ToString();
            return View(new PedidoProdOriginal());
        }

        [HttpPost]
        public ActionResult registroPedido(PedidoProdOriginal objP)
        {

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPED",SqlDbType=SqlDbType.Char, Value=objP.ID_PEDI},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.DateTime, Value=objP.FECHA_PEDI},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objP.ID_USU},
                new SqlParameter(){ParameterName="@IDPROD",SqlDbType=SqlDbType.Char, Value=objP.ID_PROD},
                new SqlParameter(){ParameterName="@IDESTA",SqlDbType=SqlDbType.Char, Value=objP.ID_ESTA},
                new SqlParameter(){ParameterName="@CONT",SqlDbType=SqlDbType.Int, Value=objP.CONTADOR},
                new SqlParameter(){ParameterName="@IMPOR",SqlDbType=SqlDbType.SmallMoney, Value=objP.IMPORTE}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPEDIDOPROD", parameters);
            return RedirectToAction("successPedido");
        }

        public ActionResult detallePedido(string id)
        {
            PedidoProd objP = ListPedidoProd().Where(p => p.ID_PEDI == id).FirstOrDefault();
            return View(objP);
        }

        public ActionResult successPedido()
        {
            return View("successPedido");
        }
    }
}