using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClient = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=VAISHNAV\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM client";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo
                                {
                                    id = reader.GetInt32(0).ToString(),
                                    name = reader.GetString(1),
                                    email = reader.GetString(2),
                                    phone = reader.GetString(3),
                                    address = reader.GetString(4),
                                    createdAt = reader.GetDateTime(5).ToString()
                                };
                                listClient.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
            }
        }
    }

    public class ClientInfo
    {
        public string? id;
        public string? name;
        public string? email;
        public string? phone;
        public string? address;
        public string? createdAt;
    }
}
