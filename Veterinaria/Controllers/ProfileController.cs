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

        /************** Listados **************/

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

        /************** Listados Originales **************/

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

        /************** Listados con Filtros **************/

        public ActionResult IndexUsuario(string n, string i)
        {
            Usuario objU = null;

            if (Session["usuario"] == null)
            {
                objU = ListUsuario().Where(p => p.ID_USU == i).FirstOrDefault();
                Session["usuario"] = objU;
                Session["IdUsuario"] = i;
                ViewBag.nombre = n;
            }
            else if (Session["usuario"] != null)
            {
                objU = (Usuario)Session["usuario"];
                ViewBag.nombre = objU.NOMBRES;
            }

            return View(objU);
        }

        List<PedidoSerOriginal> ListPedidoSerxUsuario(string codigo)
        {
            PedidoSerOriginal mascO = ListPedidoSerOriginal().Where(x => x.ID_USU == codigo).FirstOrDefault();
            List<PedidoSerOriginal> aPedido = new List<PedidoSerOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPEDIDOSERXUSUARIO", cn);
            cmd.Parameters.AddWithValue("@USU", codigo);
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

        List<MascotaOriginal> ListMascotaxUsuario(string codigo)
        {
            MascotaOriginal mascO = ListMascotaOriginal().Where(x => x.ID_MASC == codigo).FirstOrDefault();
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

        List<MascotaOriginal> ListMascotaxUsuarioCita()
        {
            List<MascotaOriginal> aMascota = new List<MascotaOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTAMASCOTAXUSUARIO", cn);
            cmd.Parameters.AddWithValue("@USU", Session["IdUsuario"].ToString());
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

        List<CitaOriginal> ListCitaxUsuario(string codigo)
        {
            CitaOriginal mascO = ListCitaOriginal().Where(x => x.ID_USU == codigo).FirstOrDefault();
            List<CitaOriginal> aPedido = new List<CitaOriginal>();
            SqlCommand cmd = new SqlCommand("SP_LISTACITALXUSUARIO", cn);
            cmd.Parameters.AddWithValue("@USU", codigo);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aPedido.Add(new CitaOriginal()
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
            return aPedido;
        }

        /************** Autogenerar Codigos **************/

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

            string s = codigo.Substring(1, 7);
            int s2 = int.Parse(s);
            if (s2 < 9)
            {
                s2++;
                codigo = "M000000" + s2;
            }
            else if (s2 >= 9)
            {
                s2++;
                codigo = "M00000" + s2;
            }
            else if (s2 >= 99)
            {
                s2++;
                codigo = "M0000" + s2;
            }
            else if (s2 >= 999)
            {
                s2++;
                codigo = "M000" + s2;
            }
            else if (s2 >= 9999)
            {
                s2++;
                codigo = "M00" + s2;
            }
            else if (s2 >= 99999)
            {
                s2++;
                codigo = "M0" + s2;
            }
            else if (s2 >= 999999)
            {
                s2++;
                codigo = "M" + s2;
            }

            return codigo;
        }

        /************** CRUD **************/

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

        /******************* Mascotas *******************/

        public ActionResult registrarMascota(string id)
        {
            ViewBag.usu = id;
            ViewBag.codigo = codigoCorrelativoMascota();
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
            return RedirectToAction("IndexUsuario");
        }

        /******************* Citas *******************/

        public ActionResult registrarCita(string id)
        {
            DateTime now = DateTime.Now;
            ViewBag.codigo = codigoCorrelativo();
            ViewBag.usuario = id;
            ViewBag.fecha = now.Year + "/" + now.Month + "/" + now.Day;
            ViewBag.mascota = new SelectList(ListMascotaxUsuarioCita(), "ID_MASC", "NOMBRE");
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
            return RedirectToAction("IndexUsuario");
        }

        /******************* Usuarios *******************/

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
            return RedirectToAction("IndexUsuario");
        }

        public ActionResult editarUsuario(string id)
        {
            UsuarioOriginal usuaO = ListUsuarioOriginal().Where(x => x.ID_USU == id).FirstOrDefault();

            ViewBag.distrito = new SelectList(ListDistrito(), "ID_DIST", "NOM_DIS");
            return View(usuaO);
        }

        /******************* Vistas Listados *******************/

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

        public ActionResult listadoCita()
        {
            return View(ListCita());
        }

        public ActionResult listadoPedidoSerxUsuario(string id)
        {
            PedidoSerOriginal mascO = ListPedidoSerOriginal().Where(x => x.ID_USU == id).FirstOrDefault();
            return View(ListPedidoSerxUsuario(id));
        }

        public ActionResult listadoCitaxUsuario(string id)
        {
            CitaOriginal mascO = ListCitaOriginal().Where(x => x.ID_USU == id).FirstOrDefault();
            return View(ListCitaxUsuario(id));
        }
        public ActionResult listadoHistorialxMascota(string id)
        {
            HistorialOriginal mascO = ListHistorialOriginal().Where(x => x.ID_MASC == id).FirstOrDefault();
            return View(ListHistorialxMascota(id));
        }

        /******************* Vistas Varios *******************/


        public ActionResult detallePedido(string id)
        {
            PedidoProd objP = ListPedidoProd().Where(p => p.ID_PEDI == id).FirstOrDefault();
            return View(objP);
        }

        public ActionResult detalleHistorial(string id)
        {
            Historial objU = ListHistorial().Where(p => p.ID_HIST == id).FirstOrDefault();
            return View(objU);
        }

        public ActionResult eliminarPedidoProd(string id)
        {
            PedidoProd objU = ListPedidoProd().Where(x => x.ID_PEDI == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPED",SqlDbType=SqlDbType.Char, Value=objU.ID_PEDI}
            };
            CRUD("SP_ELIMINARPEDIDOPROD", parameters);
            return RedirectToAction("IndexUsuario");
        }

        public ActionResult detallePedidoServ(string id)
        {
            PedidoSer objP = ListPedidoSer().Where(p => p.ID_PEDI == id).FirstOrDefault();
            return View(objP);
        }

        public ActionResult eliminarCita(string id)
        {
            Cita objU = ListCita().Where(x => x.ID_CITA == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDCITA",SqlDbType=SqlDbType.Char, Value=objU.ID_CITA}
            };
            CRUD("SP_ELIMINARCITA", parameters);
            return RedirectToAction("IndexUsuario");
        }

        public ActionResult eliminarPedidoServ(string id)
        {
            PedidoSer objU = ListPedidoSer().Where(x => x.ID_PEDI == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPED",SqlDbType=SqlDbType.Char, Value=objU.ID_PEDI}
            };
            CRUD("SP_ELIMINARPEDIDOSER", parameters);
            return RedirectToAction("IndexUsuario");
        }

        public ActionResult eliminarMascota(string id)
        {
            Mascota objU = ListMascota().Where(x => x.ID_MASC == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDMASC",SqlDbType=SqlDbType.Char, Value=objU.ID_MASC}
            };
            CRUD("SP_ELIMINARMASCOTA", parameters);
            return RedirectToAction("IndexUsuario");
        }
    }
}