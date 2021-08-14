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
    public class CustomerRepository : ICustomerRepository
    {

        IDbConnection dbConnection = DatabaseConnection.DbConnection;

        public int Delete(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public List<Customer> Get()
        {
            var sql = "select * from Customer";
            var customers = dbConnection.Query<Customer>(sql);
            return (List<Customer>)customers;
        }

        public Customer GetById(Guid customerId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Thêm mưới khách hàng vào db
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// Author hieunv 14/08/2021
        public int Post(Customer customer)
        {
            // thêm id của employee là new guid
            customer.CustomerId = Guid.NewGuid();

            /// complete sql String
            var colNames = string.Empty;
            var colParams = string.Empty;
            // đọc từng property
            var properties = customer.GetType().GetProperties();

            DynamicParameters parameters = new DynamicParameters();
            // duyệt từng property

            foreach (var prop in properties)
            {
                // lấy tên prop
                var propName = prop.Name;
                // lấy value prop
                var propValue = prop.GetValue(customer);
                // lấy kiểu prop
                var propType = prop.PropertyType;

                parameters.Add($"@{propName}", propValue);
                colNames += $"{propName},";
                colParams += $"@{propName},";
            }
            colNames = colNames.Remove(colNames.Length - 1, 1);
            colParams = colParams.Remove(colParams.Length - 1, 1);
            var sql = $"insert into Customer({colNames}) values( {colParams} ) ";
            var rowAffects = dbConnection.Execute(sql, param: parameters);
            return rowAffects;
        }

        public int Put(Customer customer, Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
