using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
 
namespace ConsoleApplication {
    public class Startup{
        public void Configure(IApplicationBuilder app){

            string row=null;

            // Substitute credentials - we will use No SSL
            MySqlConnection connection = new MySqlConnection {
                ConnectionString = "server=<server-address>;user id=<user>;<password>;SSL Mode=None;persistsecurityinfo=True;port=3306;database=<dbname>"
            };
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM states;", connection);
 
            using (MySqlDataReader reader =  command.ExecuteReader()) {
                System.Console.WriteLine("id\t\tstate\t\tpopulation");
                while (reader.Read()) {
                    row = $"{reader["id"]}\t\t{reader["state"]}\t\t{reader["population"]}";
                    // System.Console.WriteLine(row);
                }
            }
 
            connection.Close();
 

 app.Run(context => {
                return context.Response.WriteAsync(row + "\n" + "is the last row");
            });
        }
    }
}
