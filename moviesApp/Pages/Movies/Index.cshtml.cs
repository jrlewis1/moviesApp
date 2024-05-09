using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace moviesApp.Pages.Movies
{
    public class IndexModel : PageModel
    {
        public List<MovieInfo> listMovies = new List<MovieInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myMovies;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM movies";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                MovieInfo movieInfo = new MovieInfo();
                                movieInfo.id = "" + reader.GetInt32(0);
                                movieInfo.name = reader.GetString(1);
                                movieInfo.genre = reader.GetString(2);
                                movieInfo.budget = reader.GetString(3);
                                movieInfo.created_at = reader.GetString(4);

                                listMovies.Add(movieInfo);
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class MovieInfo
    {
        public string id;
        public string name;
        public string genre;
        public string budget;
        public string created_at;
    }
}
