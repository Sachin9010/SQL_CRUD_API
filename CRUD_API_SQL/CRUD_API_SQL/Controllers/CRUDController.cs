using CRUD_API_SQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

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

        public async Task<ActionResult<Students>> GetStudent() {
            SqlConnection con= new SqlConnection(_configuration.GetConnectionString("CRUDSQL").ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Students", con);
            return Content("ok");
        }

    
    }
}
