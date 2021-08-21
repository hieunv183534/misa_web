using Dapper;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositiory
{
    public class CustomerRepository : BaseRepository<Customer> ,ICustomerRepository
    {
        #region GetFilter

        public PagingResult<Customer> GetFilter(int pageSize, int pageNumber, Guid? customerGroupId, string searchTerms)
        {
            DynamicParameters parameters = new DynamicParameters();
            var sql = $"select * from Customer c left join CustomerGroup cg on c.CustomerGroupId = cg.CustomerGroupId ";
            var sqlCondition = $"where 1=1 ";

            // nếu có search thì thêm điều kiệu sql là hoặc fullname, hoặc mã khách hàng, hoặc số điện thoại
            if (searchTerms != null && searchTerms != "")
            {
                parameters.Add($"@FullName", searchTerms);
                parameters.Add($"@CustomerCode", searchTerms);
                parameters.Add($"@PhoneNumber", searchTerms);
                sqlCondition += $" and ( FullName like '%{searchTerms}%' or CustomerCode=@CustomerCode or PhoneNumber=@PhoneNumber ) ";
            }

            // nếu CustomerGroupId được nhận và là 1 guid thì thêm điều kiện and CustomerGroupId = @CustomerGroupId
            if (customerGroupId != null)
            {
                parameters.Add($"@CustomerGroupId", customerGroupId);
                sqlCondition += $" and CustomerGroupId = @CustomerGroupId ";
            }

            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                // lấy tổng số bản ghi 
                var sqlCount = $"select count(CustomerId) as TotalRecord from Customer " + sqlCondition;
                int TotalRecord = (int)dbConnection.QueryFirstOrDefault(sqlCount, param: parameters).TotalRecord;

                // Xử lsi điều kiện limit, offset
                var limit = pageSize;
                var offset = (pageNumber - 1) * pageSize;
                parameters.Add($"@limit", limit);
                parameters.Add($"@offset", offset);
                sql += sqlCondition;
                sql += $" limit @offset, @limit ";

                // thực hiện truy vấn
                var customers = dbConnection.Query<Customer>(sql, param: parameters);

                return new PagingResult<Customer>(TotalRecord, (List<Customer>)customers);
            }
        }

        #endregion

        #region GetAllCustomer

        public override List<Customer> GetAll()
        {
            var sql = $"select * from Customer c left join CustomerGroup cg on c.CustomerGroupId = cg.CustomerGroupId";
            using (var dbConnection = DatabaseConnection.DbConnection)
            {
                var customers = dbConnection.Query<Customer>(sql);
                return (List<Customer>)customers;
            }
        }

        #endregion
    }
}
