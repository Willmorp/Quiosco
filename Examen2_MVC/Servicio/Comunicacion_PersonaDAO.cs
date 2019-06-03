using Examen2_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Examen2_MVC.Servicio
{
    public class Comunicacion_PersonaDAO
    {



        public static string Insert(int idpersona,int idcomunicacion)
        {
            string rpta = "ok";
            string cadena =""+ ConfigurationManager.ConnectionStrings["cn"];
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SP_I_PERSONA_COMUNICACION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idpersona", idpersona);
                cmd.Parameters.AddWithValue("@idcomunicacion",idcomunicacion);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                rpta = "no";
                
            }
            finally
            {

                cn.Close();
                cn.Dispose();
            }

            return rpta;

        }
        public static List<comunicacion> Listar(int idpersona)
        {
            List<comunicacion> lis = new List<comunicacion>();
            string cadena = "" + ConfigurationManager.ConnectionStrings["cn"];
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();

            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SP_L_COMUNICACION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idpersona", idpersona);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    comunicacion c = new comunicacion();
                    c.nombrecomunicacion = leer.GetString(0);
                    c.nombre = leer.GetString(1);
                    lis.Add(c);
                }

                //cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                lis = null;

            }
            finally
            {

                cn.Close();
                cn.Dispose();
            }

            return lis;

        }
    }
}