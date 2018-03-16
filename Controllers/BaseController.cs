using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AspNetPagingApi.Controllers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetPagingApi.Controllers
{
    public class BaseController : Controller
    {
        private string connectionString = @"Data source=localhost\SQLEXPRESS;Initial Catalog=egshop;Integrated Security=true;Trusted_Connection=True;";

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        protected Task<PageResultViewModel<T>> CreatePagedResults<T>(IQueryable<T> queryable, string paramFilter, int page, int pageSize)
        {
            return Task.Run(() =>
            {
                var skipAmount = pageSize * (page - 1);

                if (paramFilter != null)
                {
                    var properties = typeof(T).GetProperties();

                    queryable = queryable.Where(m =>
                        properties.Any(prop =>
                            prop.GetValue(m, null).ToString().IndexOf(paramFilter) >= 0));
                }

                var projection = queryable
                    .Skip(skipAmount)
                    .Take(pageSize);

                var totalNumberOfRecords = queryable.Count();
                var results = projection.ToList();

                var mod = totalNumberOfRecords % pageSize;
                var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

                return new PageResultViewModel<T>
                {
                    Results = results,
                    PageNumber = page,
                    PageSize = results.Count,
                    TotalNumberOfPages = totalPageCount,
                    TotalNumberOfRecords = totalNumberOfRecords
                };
            });
        }
    }
}