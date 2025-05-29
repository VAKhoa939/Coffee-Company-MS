using CoffeeCompanyMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.DAOs
{
    internal class SupplierDAO : BaseDAO
    {
        private readonly IngredientDAO ingredientDAO;

        public SupplierDAO(IngredientDAO ingredientDAO)
        {
            this.ingredientDAO = ingredientDAO;
        }

        public List<Supplier> GetAllSuppliers()
        {
            string query = "SELECT * FROM Supplier ORDER BY Name";
            return ExecuteQuery(query, 
                reader => new Supplier(reader, ingredientDAO.GetIngredientsBySupplierId));
        }

        public Supplier GetSupplierById(Guid id)
        {
            string query = "SELECT * FROM Supplier WHERE ID = @ID";
            var parameters = new Dictionary<string, object> { 
                ["@ID"] = id
            };

            var result = ExecuteQuery(query, 
                reader => new Supplier(reader, ingredientDAO.GetIngredientsBySupplierId), 
                parameters);
            return result.Count > 0 ? result[0] : null;
        }

        public bool InsertSupplier(Supplier supplier)
        {
            string query = @"
        INSERT INTO Supplier (ContractStartDate, Name, PhoneNum, OwnerName, Email, Address)
        VALUES (@ContractStartDate, @Name, @PhoneNum, @OwnerName, @Email, @Address)";

            var parameters = new Dictionary<string, object>
            {
                ["@ContractStartDate"] = supplier.ContractStartDate,
                ["@Name"] = supplier.Name,
                ["@PhoneNum"] = supplier.PhoneNum,
                ["@OwnerName"] = supplier.OwnerName,
                ["@Email"] = supplier.Email,
                ["@Address"] = supplier.Address
            };

            return ExecuteNonQuery(query, parameters);
        }

        public bool UpdateSupplier(Supplier supplier)
        {
            string query = @"
        UPDATE Supplier
        SET ContractStartDate = @ContractStartDate,
            Name = @Name,
            PhoneNum = @PhoneNum,
            OwnerName = @OwnerName,
            Email = @Email,
            Address = @Address
        WHERE ID = @ID";

            var parameters = new Dictionary<string, object>
            {
                ["@ID"] = supplier.Id,
                ["@ContractStartDate"] = supplier.ContractStartDate,
                ["@Name"] = supplier.Name,
                ["@PhoneNum"] = supplier.PhoneNum,
                ["@OwnerName"] = supplier.OwnerName,
                ["@Email"] = supplier.Email,
                ["@Address"] = supplier.Address
            };

            return ExecuteNonQuery(query, parameters);
        }

        public bool DeleteSupplier(Guid id)
        {
            string query = "DELETE FROM Supplier WHERE ID = @ID";

            var parameters = new Dictionary<string, object>
            {
                ["@ID"] = id
            };

            return ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// Get the name of the supplier for a given import order.
        /// </summary>
        public string GetImportSupplierName(Guid importOrderId)
        {
            string query = @"
                SELECT TOP 1 s.Name
                FROM TransferOrder t
                JOIN TransferOrderItem toi ON t.ID = toi.TransferOrderID
                JOIN Ingredient i ON toi.IngredientID = i.ID
                JOIN Supplier s ON i.SupplierID = s.ID
                WHERE t.ID = @ImportOrderID";

            var parameters = new Dictionary<string, object>
            {
                ["@ImportOrderID"] = importOrderId
            };

            var result = ExecuteScalar(query, parameters);
            return result != null ? result.ToString() : null;
        }
    }
}
