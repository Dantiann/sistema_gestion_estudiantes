using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webmvccolegio.Models;

namespace Webmvccolegio.Controllers
{
    
    public class HomeController : Controller
    {
        public List<Personas> personal = new List<Personas>();

        public void consultaEstudiantes() 
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-NE3TVQ9\\SQLEXPRESS;Initial Catalog=Colegio; Integrated Security=True;");
            conn.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            List<Personas> persons = new List<Personas>();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "spConsultarEstudiantes";
            com.ExecuteNonQuery();
            SqlDataReader registros  = com.ExecuteReader();

            while (registros.Read()) 
            {
                Personas art = new Personas
                {
                    Idestudiante = registros["idEstudiante"].ToString(),
                    Nombres = registros["nombres"].ToString(),
                    Apellidos = registros["apellidos"].ToString()
                };
                persons.Add(art);

            }
            conn.Close();
            personal = persons;

    }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(String codigo, String nombres, String apellidos, String boton)
        {
            ViewBag.Message = "Your application description page.";
            
            if (boton is null)
            {
                consultaEstudiantes();
                return View(personal);
            }
            else if(boton == "Agregar")
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-NE3TVQ9\\SQLEXPRESS; Initial Catalog=Colegio; Integrated Security=True;");
                conn.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.StoredProcedure;
                
                com.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = nombres;
                com.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = apellidos;
                com.CommandText = "spInsertEstudiantes";
                com.ExecuteNonQuery();
                //return View();
                consultaEstudiantes();
                return View(personal);
            }
            else if (boton == "Actualizar")
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-NE3TVQ9\\SQLEXPRESS; Initial Catalog=Colegio; Integrated Security=True;");
                conn.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@IdEstudiante", SqlDbType.Int).Value = Convert.ToInt32(codigo);
                com.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = nombres;
                com.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = apellidos;
                com.CommandText = "spActualizarEstudiantes";
                com.ExecuteNonQuery();
                //return View();
                consultaEstudiantes();
                return View(personal);
            }

            else if (boton == "Eliminar")
            {
                SqlConnection conn = new SqlConnection("Data Source=DESKTOP-NE3TVQ9\\SQLEXPRESS; Initial Catalog=Colegio; Integrated Security=True;");
                //SqlConnection conn = new SqlConnection("Data Source=BOGCPCDMF03115\\SQLEXPRESS; Initial Catalog=Colegio; User ID=sa;Password=Asena2023$;");
                conn.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@IdEstudiante", SqlDbType.Int).Value = Convert.ToInt32(codigo);
                com.CommandText = "spEliminarEstudiantes";
                com.ExecuteNonQuery();
                //return View();
                consultaEstudiantes();
                return View(personal);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}