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
    public class ProductoController : Controller
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager
                           .ConnectionStrings["cnx"].ConnectionString);

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

        List<Producto> ListProductoxNombre(string nombre)
        {
            List<Producto> aProducto = new List<Producto>();
            SqlCommand cmd = new SqlCommand("SP_LISTAPPRODUCTOXNOMBRE", cn);
            cmd.Parameters.AddWithValue("@NOM", nombre);
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
            return View(ListProducto());
        }

        public ActionResult listadoProductoPag(int p = 0)
        {
            List<Producto> aProducto = ListProducto();
            int filas = 10;
            int n = aProducto.Count;
            int pag = n % filas > 0 ? n / filas + 1 : n / filas;

            ViewBag.pag = pag;
            ViewBag.p = p;
            return View(aProducto.Skip(p * filas).Take(filas));

        }

        public ActionResult detalleProducto(string id)
        {
            Producto objP = ListProducto().Where(p => p.ID_PROD == id).FirstOrDefault();
            return View(objP);
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

        public ActionResult registroProducto()
        {
            ViewBag.codigo = codigoCorrelativo();
            return View(new ProductoOriginal());
        }

        [HttpPost]
        public ActionResult registroProducto(ProductoOriginal objP, HttpPostedFileBase f)
        {
            if (f == null)
            {
                ViewBag.mensaje = "Selecciona una foto";
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                ViewBag.mensaje = "Debe ser un jpg";
                return View(objP);
            }
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                 new SqlParameter(){ParameterName="@IDPRO",SqlDbType=SqlDbType.Char, Value=objP.ID_PROD},
                new SqlParameter(){ParameterName="@NOMPRO",SqlDbType=SqlDbType.VarChar, Value=objP.NOMB_PROD},
                new SqlParameter(){ParameterName="@PRE",SqlDbType=SqlDbType.SmallMoney, Value=objP.PREC_PROD},
                new SqlParameter(){ParameterName="@STOCK",SqlDbType=SqlDbType.Int, Value=objP.STOCK},
                new SqlParameter(){ParameterName="@SERIE",SqlDbType=SqlDbType.VarChar, Value=objP.SERIE},
                new SqlParameter(){ParameterName="@MARCA",SqlDbType=SqlDbType.VarChar, Value=objP.MARCA},
                new SqlParameter(){ParameterName="@PROV",SqlDbType=SqlDbType.VarChar, Value=objP.PROV_PROD},
                new SqlParameter(){ParameterName="@DESPRO",SqlDbType=SqlDbType.VarChar, Value=objP.DESC_PROD},
                new SqlParameter(){ParameterName="@DESHTML",SqlDbType=SqlDbType.VarChar, Value=objP.DESC_HTML},
                new SqlParameter(){ParameterName="@FOTO",SqlDbType=SqlDbType.VarChar,
                            Value="~/Images/fotos_productos/"+Path.GetFileName(f.FileName)}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPRODUCTO", parameters);
            f.SaveAs(Path.Combine(Server.MapPath("~/Images/fotos_productos/"),
                Path.GetFileName(f.FileName)));

            return RedirectToAction("listadoProducto");
        }

        public ActionResult editarProducto(string id)
        {
            ProductoOriginal peliO = ListProductoOriginal().Where(x => x.ID_PROD == id).FirstOrDefault();
            return View(peliO);
        }

        [HttpPost]
        public ActionResult editarProducto(ProductoOriginal objP, HttpPostedFileBase f)
        {
            if (f == null)
            {
                return View(objP);
            }
            if (Path.GetExtension(f.FileName) != ".jpg")
            {
                return View(objP);
            }
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPRO",SqlDbType=SqlDbType.Char, Value=objP.ID_PROD},
                new SqlParameter(){ParameterName="@NOMPRO",SqlDbType=SqlDbType.VarChar, Value=objP.NOMB_PROD},
                new SqlParameter(){ParameterName="@PRE",SqlDbType=SqlDbType.SmallMoney, Value=objP.PREC_PROD},
                new SqlParameter(){ParameterName="@STOCK",SqlDbType=SqlDbType.Int, Value=objP.STOCK},
                new SqlParameter(){ParameterName="@SERIE",SqlDbType=SqlDbType.VarChar, Value=objP.SERIE},
                new SqlParameter(){ParameterName="@MARCA",SqlDbType=SqlDbType.VarChar, Value=objP.MARCA},
                new SqlParameter(){ParameterName="@PROV",SqlDbType=SqlDbType.VarChar, Value=objP.PROV_PROD},
                new SqlParameter(){ParameterName="@DESPRO",SqlDbType=SqlDbType.VarChar, Value=objP.DESC_PROD},
                new SqlParameter(){ParameterName="@DESHTML",SqlDbType=SqlDbType.VarChar, Value=objP.DESC_HTML},
                new SqlParameter(){ParameterName="@FOTO",SqlDbType=SqlDbType.VarChar,
                            Value="~/Images/fotos_productos/"+Path.GetFileName(f.FileName)}
            };
            ViewBag.mensaje = CRUD("SP_MANTENIMIENTOPRODUCTO", parameters);
            f.SaveAs(Path.Combine(Server.MapPath("~/Images/fotos_productos/"),
                Path.GetFileName(f.FileName)));

            return RedirectToAction("listadoProducto");
        }

        public ActionResult eliminarProducto(string id)
        {
            Producto objP = ListProducto().Where(x => x.ID_PROD == id).FirstOrDefault();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@IDPRO",SqlDbType=SqlDbType.Char, Value=objP.ID_PROD},
            };
            CRUD("SP_ELIMINARPRODUCTO", parameters);
            return RedirectToAction("listadoProducto");
        }

        public ActionResult listadoProductoxNombre()
        {
            return View(ListProductoxNombre(""));
        }

        [HttpPost]
        public ActionResult listadoProductoxNombre(string nombre)
        {
            return View(ListProductoxNombre(nombre));
        }

    }
}