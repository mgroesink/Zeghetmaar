using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoginMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username , string password)
        {

            SqlConnection conn = new SqlConnection();
            string cs = "Server=sql6004.site4now.net;Database=DB_A2A0BC_vp;User Id=K0501;Password=ROCvT_K0501;";
            conn.ConnectionString = cs;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLogin");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username = @USERNAME AND password = @PASSWORD collate Latin1_General_CS_AS");
                cmd.Parameters.AddWithValue("@USERNAME", username);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Inloggen mislukt";
                    return View();
                }
            }
            catch (SqlException sql)
            {
                Console.WriteLine("Verbinding met server mislukt");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Niet delen door nul");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Onbekende fout. Zoek een betere programmeur");

            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }



            return View();
        }
    }
}
