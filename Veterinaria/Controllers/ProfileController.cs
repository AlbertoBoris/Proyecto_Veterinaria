using Veterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Veterinaria.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                            .ConnectionStrings["cnx"].ConnectionString);

        public static string sesion = null;


        // GET: Profile
        public ActionResult Index(string n, string i)
        {

            Usuario objU = null;

            if (Session["usuario"] == null)
            {
                objU = ListUsuario().Where(p => p.ID_USU == i).FirstOrDefault();
                Session["usuario"] = objU;
                ViewBag.nombre = n;
            }
            else if (Session["usuario"] != null)
            {
                objU = (Usuario)Session["usuario"];
                ViewBag.nombre = objU.NOMBRES;
            }

            return View(objU);
        }

        public ActionResult IndexUsuario(string n, string i)
        {
            Usuario objU = null;

            if (Session["usuario"] == null)
            {
                objU = ListUsuario().Where(p => p.ID_USU == i).FirstOrDefault();
                Session["usuario"] = objU;
                ViewBag.nombre = n;
            }
            else if (Session["usuario"] != null)
            {
                objU = (Usuario)Session["usuario"];
                ViewBag.nombre = objU.NOMBRES;
            }

            return View(objU);
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

        List<PedidoProdOriginal> ListPedidoProdxUsuario(string codigo)
        {
            PedidoProdOriginal mascO = ListPedidoProdOriginal().Where(x => x.ID_USU == codigo).FirstOrDefault();
            List<PedidoProdOriginal> aPedido = new List<PedidoProdOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOXUSUARIO", cn);
            cmd.Parameters.AddWithValue("@USU", codigo);
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

        /**** Registrar Mascota ****/

        string codigoCorrelativoMascota()
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

            string s = codigo.Substring(2, 2);
            int s2 = int.Parse(s);
            if (s2 <= 99)
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

        List<MascotaOriginal> ListMascotaxUsuario(string codigo)
        {
             MascotaOriginal mascO = ListMascotaOriginal().Where(x => x.ID_USU == codigo).FirstOrDefault();
            List<MascotaOriginal> aMascota = new List<MascotaOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAMASCOTAXUSUARIO", cn);
            cmd.Parameters.AddWithValue("@USU", codigo);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aMascota.Add(new MascotaOriginal()
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

        public ActionResult registrarMascota(string id)
        {
            ViewBag.usu = id;
            ViewBag.codigo = codigoCorrelativoMascota() ;
            return View(new MascotaOriginal());
        }

        [HttpPost]
        public ActionResult registrarMascota(MascotaOriginal objU)
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
            ViewBag.codigo = codigoCorrelativoMascota();
            return RedirectToAction("IndexUsuario");
        }
        public ActionResult listadoMascotaxUsuario(string id)
        {
            MascotaOriginal mascO = ListMascotaOriginal().Where(x => x.ID_USU == id).FirstOrDefault();
            return View(ListMascotaxUsuario(id));
        }

        public ActionResult listadoPedidoProdxUsuario(string id)
        {
            PedidoProdOriginal mascO = ListPedidoProdOriginal().Where(x => x.ID_USU == id).FirstOrDefault();
            return View(ListPedidoProdxUsuario(id));
        }
    }
}