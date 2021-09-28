using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoginMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult LoginUnsafe(string username, string password)
        {
            SqlConnection conn = new SqlConnection();
            string cs = "Server=sql6004.site4now.net;Database=DB_A2A0BC_vp;User Id=K0501;Password=ROCvT_K0501;";
            conn.ConnectionString = cs;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username = '" 
                    + username + "' AND password = '" + password + "' collate Latin1_General_CS_AS");
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    // Send to privay page if login was succesfull
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    // Add username and error to viewbag so it can be used in the view
                    ViewBag.UserName = username;
                    ViewBag.Error = "Login mislukt";
                }
            }
            catch (SqlException sql)
            {
                ViewBag.UserName = username;
                ViewBag.Error = "Verbinding met server mislukt";
            }
            catch (DivideByZeroException ex)
            {
                ViewBag.UserName = username;
                ViewBag.Error = "Delen door nul is flauwekul";

            }
            catch (Exception ex)
            {
                ViewBag.UserName = username;
                ViewBag.Error = "Onbekende fout. Raak in paniek of bel de servicedesk.";
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return View("Login");
        }

        [HttpPost]
        public IActionResult LoginSafe(string username , string password)
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
                    // Send to privay page if login was succesfull
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    ViewBag.UserName = username;
                    ViewBag.Error = "Login mislukt";
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
