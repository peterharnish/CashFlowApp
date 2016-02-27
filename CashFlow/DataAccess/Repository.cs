using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using CashFlow.Entity;
using Logging;

namespace CashFlow.DataAccess
{
    /// <summary>
    /// Queries the Access database. 
    /// </summary>
    public class Repository : IRepository
    {
        /// <summary>
        /// Gets a list of expenses. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <returns> A list of expenses. </returns>
        public List<Entity.Expense> GetExpenses(DateTime start, DateTime end)
        {
            LogHelper.LogInfo(string.Format("Entering Repository.GetExpenses with start = {0} and end = {1}.", start.ToString(), end.ToString()));
            List<Expense> expenses = new List<Expense>();
            OleDbConnection conn = null;
            OleDbDataReader reader = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand(string.Format("SELECT Expenses.*, ExpenseCategories.Category, ExpenseTypes.Type From ExpenseTypes INNER JOIN (ExpenseCategories INNER JOIN Expenses ON ExpenseCategories.ID = Expenses.Category) ON ExpenseTypes.ID = Expenses.Type WHERE (((Expenses.ExpenseDate) >= #{0}#)) AND (((Expenses.ExpenseDate) <= #{1}#));",
                    start.ToString("d"),
                    end.ToString("d")),
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    expenses.Add(new Expense()
                    {
                        ID = reader.GetInt32(0),
                        Amount = reader.GetDecimal(1),
                        ExpenseDate = reader.GetDateTime(2),
                        ExpenseTypeID = reader.GetInt32(3),
                        ExpenseCategoryID = reader.GetInt32(4),
                        Description = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        Category = reader.GetString(6),
                        ExpenseType = reader.GetString(7)
                    });
                }               
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetExpenses.");
            return expenses;
        }

        /// <summary>
        /// Queries for expenses that match a given description. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <param name="description"> Expense description. </param>
        /// <returns> List of expenses. </returns>
        public List<Expense> QueryExpenses(DateTime start, DateTime end, string description)
        {
            LogHelper.LogInfo(string.Format("Entering Repository.QueryExpenses with start = {0}, end = {1}, and description = {2}.", start.ToString(), end.ToString(), description));
            List<Expense> expenses = new List<Expense>();
            OleDbConnection conn = null;
            OleDbDataReader reader = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand(string.Format("SELECT Expenses.*, ExpenseCategories.Category, ExpenseTypes.Type FROM ExpenseTypes INNER JOIN (ExpenseCategories INNER JOIN Expenses ON ExpenseCategories.ID = Expenses.Category) ON ExpenseTypes.ID = Expenses.Type WHERE (((Expenses.ExpenseDate)>#{0}# And (Expenses.ExpenseDate)<#{1}#) AND ((Expenses.Description) Like \"{2}%\"));",
                    start.ToString("d"),
                    end.ToString("d"),
                    description),
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    expenses.Add(new Expense()
                    {
                        ID = reader.GetInt32(0),
                        Amount = reader.GetDecimal(1),
                        ExpenseDate = reader.GetDateTime(2),
                        ExpenseTypeID = reader.GetInt32(3),
                        ExpenseCategoryID = reader.GetInt32(4),
                        Description = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        Category = reader.GetString(6),
                        ExpenseType = reader.GetString(7)
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetExpenses.");
            return expenses;
        }

        /// <summary>
        /// Gets the income for a given time period. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <returns> A list of income entities. </returns>
        public List<Entity.Income> GetIncome(DateTime start, DateTime end)
        {
            LogHelper.LogInfo(string.Format("Entering Repository.GetIncome with start = {0} and end = {1}.", start.ToString(), end.ToString()));
            List<Income> income = new List<Income>();
            OleDbConnection conn = null;
            OleDbDataReader reader = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand(string.Format("SELECT Income.* FROM Income WHERE (((Income.PaymentDate) >= #{0}#)) AND (((Income.PaymentDate) <= #{1}#));",
                    start.ToString("d"),
                    end.ToString("d")),
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    income.Add(new Income()
                    {
                        ID = reader.GetInt32(0),
                        Amount = reader.GetDecimal(1),
                        Source = reader.GetString(2),
                        PaymentDate = reader.GetDateTime(3)
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetIncome.");
            return income;
        }

        /// <summary>
        /// Inserts an expense. 
        /// </summary>
        /// <param name="expense"> Expense object. </param>
        /// <returns> ID of expense object. </returns>
        public int InsertExpense(Entity.Expense expense)
        {
            LogHelper.LogInfo("Entering Repository.InsertExpense.");

            OleDbConnection conn = null;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("INSERT into Expenses (Amount, ExpenseDate, Type, Category, Description) Values(@Amount, @ExpenseDate, @Type, @Category, @Description)");

                command.Connection = conn;
                command.Parameters.Add("@Amount", OleDbType.Currency).Value = expense.Amount;
                command.Parameters.Add("@ExpenseDate", OleDbType.Date).Value = expense.ExpenseDate;
                command.Parameters.Add("@Type", OleDbType.Integer).Value = expense.ExpenseTypeID;
                command.Parameters.Add("@Category", OleDbType.Integer).Value = expense.ExpenseCategoryID;
                command.Parameters.Add("@Description", OleDbType.VarChar).Value = expense.Description;
                command.ExecuteNonQuery();

                command.CommandText = SelectIdentity;
                expense.ID = (int)command.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo(string.Format("Exiting Repository.InsertExpense with expense.ID = {0}.", expense.ID.ToString()));
            return expense.ID;
        }

        /// <summary>
        /// Inserts an income entry. 
        /// </summary>
        /// <param name="income"> Income entity. </param>
        /// <returns> ID of income entry. </returns>
        public int InsertIncome(Entity.Income income)
        {
            LogHelper.LogInfo("Entering Repository.InsertIncome.");

            OleDbConnection conn = null;

            try
            {
                conn = GetOleDbConnection();
                OleDbCommand command = new OleDbCommand("INSERT into Income (Amount, Source, PaymentDate) Values(@Amount, @Source, @PaymentDate)");

                command.Connection = conn;
                command.Parameters.Add("@Amount", OleDbType.Currency).Value = income.Amount;
                command.Parameters.Add("@Source", OleDbType.VarChar).Value = income.Source;
                command.Parameters.Add("@PaymentDate", OleDbType.Date).Value = income.PaymentDate;
                command.ExecuteNonQuery();

                command.CommandText = SelectIdentity;
                income.ID = (int)command.ExecuteScalar();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo(string.Format("Exiting Repository.InsertIncome with income.ID = {0}.", income.ID.ToString()));
            return income.ID;
        }

        /// <summary>
        /// Gets the expense types. 
        /// </summary>
        /// <returns> A list of expense types. </returns>
        public List<Entity.ExpenseType> GetExpenseTypes()
        {
            LogHelper.LogInfo("Entering Repository.GetExpenseTypes.");
            List<ExpenseType> expenseTypes = new List<ExpenseType>();

            OleDbConnection conn = null;
            OleDbDataReader reader = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand("SELECT ExpenseTypes.* FROM ExpenseTypes;",                    
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    expenseTypes.Add(new ExpenseType()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetExpenseTypes.");
            return expenseTypes;
        }

        /// <summary>
        /// Gets expense categories. 
        /// </summary>
        /// <returns> Expense categories. </returns>
        public List<Entity.ExpenseCategory> GetExpenseCategories()
        {
            LogHelper.LogInfo("Entering Repository.GetExpenseCategories.");
            List<ExpenseCategory> categories = new List<ExpenseCategory>();

            OleDbConnection conn = null;
            OleDbDataReader reader = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand("SELECT ExpenseCategories.* FROM ExpenseCategories;",
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new ExpenseCategory()
                    {
                        ID = reader.GetInt32(0),
                        Category = reader.GetString(1)
                    });
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetExpenseCategories.");
            return categories;
        }

        /// <summary>
        /// Gets autocomplete for expense description. 
        /// </summary>
        /// <param name="type"> Expense type. </param>
        /// <returns> An array of autocomplete suggestions. </returns>
        public string[] GetExpenseAutocomplete(Expense expense)
        {
            LogHelper.LogInfo("Entering Repository.GetExpenseAutocomplete.");

            OleDbConnection conn = null;
            OleDbDataReader reader = null;
            List<string> suggestions = new List<string>();

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand(string.Format("SELECT Distinct Top 10 Expenses.Description FROM Expenses Where Expenses.Description Like '{0}%' And Expenses.Category = {1} Order By Expenses.Description",
                    expense.Description,
                    expense.ExpenseCategoryID.ToString()),
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    suggestions.Add(reader.GetString(0));                   
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetExpenseAutocomplete.");
            return suggestions.ToArray();
        }

        /// <summary>
        /// Gets autocomplete suggestions for income. 
        /// </summary>
        /// <param name="text"> Text for income source. </param>
        /// <returns> Array of autocomplete suggestions. </returns>
        public string[] GetIncomeAutocomplete(string text)
        {
            LogHelper.LogInfo(string.Format("Entering Repository.GetIncomeAutocomplete with text = {0}.", text));
            List<string> suggestions = new List<string>();

            OleDbConnection conn = null;
            OleDbDataReader reader = null;

            try
            {
                conn = GetOleDbConnection();

                OleDbCommand command = new OleDbCommand(string.Format("SELECT Distinct Top 10 Income.Source FROM Income Where Income.Source Like '{0}%' Order By Income.Source",
                    text),
                    conn);

                command.Connection = conn;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    suggestions.Add(reader.GetString(0));
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            LogHelper.LogInfo("Exiting Repository.GetIncomeAutocomplete.");
            return suggestions.ToArray();
        }

        /// <summary>
        /// Gets an OLE DB connection. 
        /// </summary>
        /// <returns> An OLE DB connection. </returns>
        private OleDbConnection GetOleDbConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["connstring"];
            conn.Open();

            return conn;
        }

        private const string SelectIdentity = "Select @@Identity";





        public string[] GetExpenseAutocomplete()
        {
            throw new NotImplementedException();
        }

        public string[] GetIncomeAutocomplete()
        {
            throw new NotImplementedException();
        }
    }
}
