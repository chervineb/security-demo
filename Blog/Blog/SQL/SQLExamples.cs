using System.Data.SqlClient;
using Blog.Models;
using Microsoft.Extensions.Configuration;

namespace Blog.SQL
{
    public class SQLExamples
    {
        private IConfiguration Configuration { get; }

        public SQLExamples(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// A1 - 
        public void Add(Comment comment)
        {
            using (var conn = new SqlConnection(Configuration
                .GetConnectionString("CommentContext")))
            {

                var cmd = new SqlCommand("insert into comments Id,Body, Author Values ('@author', '@body', '@Id') ", conn);
                cmd.Parameters.Add(new SqlParameter(nameof(Comment.Author), comment.Author));
                cmd.Parameters.Add(new SqlParameter(nameof(Comment.Body), comment.Body));
                cmd.Parameters.Add(new SqlParameter(nameof(comment.Id), comment.Id));
                cmd.ExecuteNonQuery();
            }
        }
    }
}