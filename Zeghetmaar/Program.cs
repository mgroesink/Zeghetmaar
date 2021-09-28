using System;
using System.Data.SqlClient;

namespace Zeghetmaar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Inlognaam: ");
            string userName = Console.ReadLine();
            Console.Write("Wachtwoord: ");
            string password = Console.ReadLine();
            SqlConnection conn = new SqlConnection();
            string cs = "Server=sql6004.site4now.net;Database=DB_A2A0BC_vp;User Id=K0501;Password=ROCvT_K0501;";
            conn.ConnectionString = cs;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLogin");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username = @USERNAME AND password = @PASSWORD collate Latin1_General_CS_AS");
                cmd.Parameters.AddWithValue("@USERNAME", userName);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    Console.WriteLine("Ingelogd");
                }
                else
                {
                    Console.WriteLine("Inloggen mislukt");

                }
            }
            catch(SqlException sql)
            {
                Console.WriteLine("Verbinding met server mislukt");
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine("Niet delen door nul");

            }
            catch(Exception ex)
            {
                Console.WriteLine("Onbekende fout. Zoek een betere programmeur");

            }
            finally
            {
                if(conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            Console.ReadKey();

        }
    }
}
