using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrudOperation
    {
        private readonly string _connectionString;
        private SQLDataAccess db = new SQLDataAccess();

        public SqlCrudOperation(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BasicContactModel> GetAllContacts()
        {
            string query = "select Id, FirstName, LastName from dbo.Contacts";

            return db.LoadData<BasicContactModel, dynamic>(query, new { }, _connectionString);

            
        }

        public FullContactModel GetFullContactById(int id)
        {
            string query = "select Id, FirstName, LastName from dbo.Contacts where Id = @Id";
            FullContactModel output = new FullContactModel();

            output.BasicInfo = db.LoadData<BasicContactModel, dynamic>(sqlStatement, new { Id = id }, _connectionString.FirstOrDefault);

            if (output.BasicInfo == null)
            {
                //do something here to tell the user that the record was not found
                return null;
            }

            query = @"SELECT e.*
                    FROM
                    dbo.EmailAddresses e
                    inner join dbo.ContactEmail ce on ce.EmailAddressId = e.Id
                    where ce.ContactId = @Id";

            output.EmailAddresses = db.LoadData<EmailAddressModel, dynamic>(query, new { Id = id }, _connectionString);

            query = @"SELECT p.* 
                    FROM
                    dbo.PhoneNumbers p
                    inner join dbo.ContactPhoneNumbers cp on cp.PhoneNumberId = p.Id
                    where cp.ContactId = @Id";

            output.PhoneNumbers = db.LoadData<PhoneNumberModel, dynamic>(query, new { Id = id }, _connectionString);

            return output; 
        }
    }
}