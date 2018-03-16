using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AspNetPagingApi.Controllers.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetPagingApi.Controllers
{
    [Route("api/componentes")]
    public class ComponenteController : BaseController
    {
        [HttpGet]
        public async Task<dynamic> Get(int page = 1, string query = "")
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var pageResult = await CreatePagedResults<Componente>(dbConnection.QueryAsync<Componente>("select * from componente").Result.AsQueryable(), 
                    query, page, 5);

                return pageResult;
            }
        }
    }
}