using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Client
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();

        public string ErrorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.email = Request.Form["email"];
            clientInfo.address = Request.Form["address"];

            if( clientInfo.name.Length == 0 || clientInfo.phone.Length == 0 ||
                clientInfo.email.Length == 0 || clientInfo.address.Length == 0)
            {
                ErrorMessage = "All the fields are required";
                return;
            }

            string connectionString = "Data Source=VAISHNAV\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO client" + "(name, email, phone, address) VALUES " +
                             "(@name, @email, @phone, @address)";

                using (SqlCommand commmand = new SqlCommand(sql, connection))
                {
                    commmand.Parameters.AddWithValue("@name", clientInfo.name);
                    commmand.Parameters.AddWithValue("@email", clientInfo.phone);
                    commmand.Parameters.AddWithValue("@phone", clientInfo.email);
                    commmand.Parameters.AddWithValue("@address", clientInfo.address);

                    commmand.ExecuteNonQuery();
                }
            }

            clientInfo.name = "";
            clientInfo.phone = "";
            clientInfo.email = "";
            clientInfo.address = "";

            successMessage = "New client is added successfully";

            Response.Redirect("/Client/Index");

        }
    }
}
