using CRUD_API_SQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_API_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CRUDController(IConfiguration configuration) { 
            _configuration = configuration;
        }
        [HttpGet]

        public string GetStudent() {
            SqlConnection con= new SqlConnection(_configuration.GetConnectionString("CRUDSQL").ToString());
            con.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("Select * from Student", con);
            con.Close();
            DataTable dt= new DataTable();
            cmd.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("{");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) { 
                        sb.Append(",");
                    }
                    sb.AppendFormat("\"{0}\":\"{1}\"", dt.Columns[i].ColumnName, row[i].ToString().Replace("\"","\\\""));
                }
                sb.Append("},");
            }
            if (sb.Length > 0) {
                sb.Length--;
            }
            sb.Append(']');
            string jsonResult=sb.ToString();

            return  jsonResult;
        }

        [HttpGet("{id}")]

        public string GetStudentDetailsById(int id) {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CRUDSQL").ToString());
            con.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("Select * from Student where id="+id, con);
            con.Close();
            DataTable dt= new DataTable();
            cmd.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("{");
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    sb.AppendFormat("\"{0}\":\"{1}\"", dt.Columns[i].ColumnName, row[i].ToString().Replace("\"", "\\\""));
                }
                sb.Append("}]");
            }
        
            return sb.ToString();
        }

        [HttpPost]
        public string InsertStudentDetails(Student student)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CRUDSQL").ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Student VALUES('"+student.Name+"','"+student.Class+"','"+student.FatherName+"','"+student.Subject+"','"+student.BirthYear+"')",con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            
            if (i > 0) {
                return "Successfully inserted";
            }
            else
            return "Error while inserting the record";

        }

    
    }
}
