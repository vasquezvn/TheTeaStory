using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebApplicationTheTeaStory
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int sum(int a, int b)
        {
            return a + b;
        }

        [WebMethod]
        public bool insertClientDo(string firstName, string lastName, string email, string programme)
        {
            bool result = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
            string query = $"INSERT INTO [Client] values('{firstName}', '{lastName}', '{email}', '{programme}')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();

            if(i > 0)
                result = true;

            cmd.Connection.Close();

            return result;
        }

        /*[WebMethod]
        public DataTable Get()
        {
            string constr = ConfigurationManager.ConnectionStrings[@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivan.vasquez\source\repos\TheTeaStory\SampleDatabaseWalkthrough\Database1.mdf;Integrated Security=True"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Client"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "Client";
                            sda.Fill(dt);

                            return dt;
                        }
                    }
                }
            }
        }*/


    }
}
