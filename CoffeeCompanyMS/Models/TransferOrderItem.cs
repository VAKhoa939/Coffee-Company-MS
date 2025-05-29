using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompanyMS.Models
{
    internal class TransferOrderItem
    {
        private Guid id;
        private int quantity;
        private DateTime expirationDate;
        private Ingredient ingredient;

        // Constructor to create a TransferOrderItem object with specified values
        public TransferOrderItem(Guid id, int quantity, DateTime expirationDate, Ingredient ingredient)
        {
            this.id = id;
            this.quantity = quantity;
            this.expirationDate = expirationDate;
            this.ingredient = ingredient;
        }

        // Constructor to create a TransferOrderItem object from a SqlDataReader
        public TransferOrderItem(SqlDataReader reader, Func<Guid, Ingredient> getIngredientById)
        {
            id = Guid.Parse(reader["ID"].ToString());
            quantity = Convert.ToInt32(reader["Quantity"]);
            expirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

            var ingredientId = Guid.Parse(reader["IngredientID"].ToString());
            ingredient = getIngredientById(ingredientId);
        }

        public Guid Id
        {
            get => id;
            set => id = value;
        }

        public int Quantity
        {
            get => quantity;
            set => quantity = value;
        }

        public DateTime ExpirationDate
        {
            get => expirationDate;
            set => expirationDate = value;
        }

        public Ingredient Ingredient
        {
            get => ingredient;
            set => ingredient = value;
        }

        public decimal CalculateTotalPrice()
        {
            return ingredient.UnitPrice * quantity;
        }
    }

}
