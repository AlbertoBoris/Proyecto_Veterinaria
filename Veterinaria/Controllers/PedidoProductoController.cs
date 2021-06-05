using Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Services.Description;

namespace Veterinaria.Controllers
{
    public class PedidoProductoController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                          .ConnectionStrings["cnx"].ConnectionString);

        /***Listas***/
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


        /****Vistas de Controlador****/
        public ActionResult listadoPedidoProd()
        {
            return View(ListPedidoProd());
        }


        /****CRUD PEDIDO****/
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

        public ActionResult editarPedido(string id)
        {
            PedidoProdOriginal usuaO = ListPedidoProdOriginal().Where(x => x.ID_PEDI == id).FirstOrDefault();

            ViewBag.estado = new SelectList(ListEstado(), "ID_ESTA", "NOMB_ESTA");
            return View(usuaO);
        }

        [HttpPost]
        public ActionResult editarPedido(PedidoProdOriginal objP)
        {
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@IDPED",SqlDbType=SqlDbType.Char, Value=objP.ID_PEDI},
                new SqlParameter(){ParameterName="@FECHA",SqlDbType=SqlDbType.DateTime, Value=objP.FECHA_PEDI},
                new SqlParameter(){ParameterName="@IDUSU",SqlDbType=SqlDbType.Char, Value=objP.ID_USU},
                new SqlParameter(){ParameterName="@IDPROD",SqlDbType=SqlDbType.Char, Value=objP.ID_PROD},
                new SqlParameter(){ParameterName="@IDESTA",SqlDbType=SqlDbType.Char, Value=objP.ID_ESTA},
                new SqlParameter(){ParameterName="@CONT",SqlDbType=SqlDbType.Int, Value=objP.CONTADOR},
                new SqlParameter(){ParameterName="@IMPOR",SqlDbType=SqlDbType.SmallMoney, Value=objP.IMPORTE}

            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPEDIDOPROD", parametros);
            return RedirectToAction("listadoPedidoProd");
        }
        public ActionResult detallePedido(string id)
        {
            PedidoProd objP = ListPedidoProd().Where(p => p.ID_PEDI == id).FirstOrDefault();
            return View(objP);
        }
    }
}