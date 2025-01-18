using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Client
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();

        public string ErrorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=VAISHNAV\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM client WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        public void OnPost()
        {
            
            try
            {
                clientInfo.name = Request.Form["name"];
                clientInfo.phone = Request.Form["phone"];
                clientInfo.email = Request.Form["email"];
                clientInfo.address = Request.Form["address"];
                clientInfo.id = Request.Form["id"];

                if (clientInfo.name.Length == 0 || clientInfo.phone.Length == 0 ||
                    clientInfo.email.Length == 0 || clientInfo.address.Length == 0 || clientInfo.id.Length == 0)
                {
                    ErrorMessage = "All the fields are required";
                    return;
                }

                string connectionString = "Data Source=VAISHNAV\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE client " +
                                "SET name=@name, email=@email, phone=@phone, address=@address " +
                                "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }

                clientInfo.name = "";
                clientInfo.phone = "";
                clientInfo.email = "";
                clientInfo.address = "";

                successMessage = "Client edited successfully";

                Response.Redirect("/Client/Index");
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }
        }
    }
}
