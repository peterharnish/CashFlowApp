using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Logging;

namespace CashFlow.DataAccess
{
    public class SqlRepository : IRepository
    {
        string connString = System.Configuration.ConfigurationManager.AppSettings["connstring"];

        /// <summary>
        /// Gets expenses between 2 start and end dates. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <returns> List of expense entities. </returns>
        public List<Entity.Expense> GetExpenses(DateTime start, DateTime end)
        {
            LogHelper.LogInfo(string.Format("Entering SqlRepository.GetExpenses with start = {0} and end = {1}.", start.ToString(), end.ToString()));

            List<Entity.Expense> expenses = new List<Entity.Expense>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetExpenses", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartDate", start));
                command.Parameters.Add(new SqlParameter("@EndDate", end));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Entity.Expense expense = new Entity.Expense();

                    expense.ID = reader.GetInt32(0);
                    expense.Amount = reader.GetDecimal(1);
                    expense.ExpenseDate = reader.GetDateTime(2);
                    expense.ExpenseTypeID = reader.GetInt32(3);
                    expense.ExpenseCategoryID = reader.GetInt32(4);
                    expense.Category = reader.GetString(6);
                    expense.ExpenseType = reader.GetString(7);
                    expense.Description = reader.GetString(8);

                    expenses.Add(expense);
                }
            }

            LogHelper.LogInfo("Exiting SqlRepository.GetExpenses.");
            return expenses;
        }

        /// <summary>
        /// Gets income between 2 different dates. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <returns> List of income entities. </returns>
        public List<Entity.Income> GetIncome(DateTime start, DateTime end)
        {
            LogHelper.LogInfo(string.Format("Entering SqlRepository.GetIncome with start = {0} and end = {1}.", start.ToString(), end.ToString()));

            List<Entity.Income> incomeList = new List<Entity.Income>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetIncome", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartDate", start));
                command.Parameters.Add(new SqlParameter("@EndDate", end));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Entity.Income income = new Entity.Income();

                    income.ID = reader.GetInt32(0);
                    income.Amount = reader.GetDecimal(1);
                    income.PaymentDate = reader.GetDateTime(3);
                    income.Source = reader.GetString(4);

                    incomeList.Add(income);
                }
            }

            LogHelper.LogInfo("Exiting SqlRepository.GetIncome.");
            return incomeList;
        }

        /// <summary>
        /// Inserts an expense record to the database. 
        /// </summary>
        /// <param name="expense"> Expense entity. </param>
        /// <returns> ID of inserted record. </returns>
        public int InsertExpense(Entity.Expense expense)
        {
            LogHelper.LogInfo("Entering SqlRepository.InsertExpense.");

            int id;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[6];
                arrParam[0] = new SqlParameter("@Amount", expense.Amount);
                arrParam[1] = new SqlParameter("@ExpenseDate", expense.ExpenseDate);
                arrParam[2] = new SqlParameter("@Type", expense.ExpenseTypeID);
                arrParam[3] = new SqlParameter("@Category", expense.ExpenseCategoryID);
                arrParam[4] = new SqlParameter("@Description", expense.Description);
                arrParam[5] = new SqlParameter("@Ret", SqlDbType.Int, 4);
                arrParam[5].Direction = ParameterDirection.InputOutput;

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "InsertExpense", arrParam);
                id = Convert.ToInt32(arrParam[5].Value);
            }

            LogHelper.LogInfo(string.Format("Exiting SqlRepository.InsertExpense with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Inserts an income record to the database. 
        /// </summary>
        /// <param name="income"> Income entity. </param>
        /// <returns> ID of inserted record. </returns>
        public int InsertIncome(Entity.Income income)
        {
            LogHelper.LogInfo("Entering SqlRepository.InsertIncome.");

            int id;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlParameter[] arrParam = new SqlParameter[4];
                arrParam[0] = new SqlParameter("@Amount", income.Amount);
                arrParam[1] = new SqlParameter("@Source", income.Source);
                arrParam[2] = new SqlParameter("@PaymentDate", income.PaymentDate);
                arrParam[3] = new SqlParameter("@Ret", SqlDbType.Int, 4);
                arrParam[3].Direction = ParameterDirection.InputOutput;

                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "InsertIncome", arrParam);
                id = Convert.ToInt32(arrParam[3].Value);
            }

            LogHelper.LogInfo(string.Format("Exiting SqlRepository.InsertIncome with id = {0}.", id.ToString()));
            return id;
        }

        /// <summary>
        /// Gets a list of expense types. 
        /// </summary>
        /// <returns> List of expense types. </returns>
        public List<Entity.ExpenseType> GetExpenseTypes()
        {
            LogHelper.LogInfo("Entering SqlRepository.GetExpenseTypes.");

            List<Entity.ExpenseType> expenseTypes = new List<Entity.ExpenseType>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetExpenseTypes", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Entity.ExpenseType expenseType = new Entity.ExpenseType();

                    expenseType.ID = reader.GetInt32(0);
                    expenseType.Name = reader.GetString(1);
                    expenseTypes.Add(expenseType);
                }
            }

            LogHelper.LogInfo("Exiting SqlRepository.GetExpenseTypes.");
            return expenseTypes;
        }

        /// <summary>
        /// Gets a list of expense categories. 
        /// </summary>
        /// <returns> A list of expense categories. </returns>
        public List<Entity.ExpenseCategory> GetExpenseCategories()
        {
            LogHelper.LogInfo("Entering SqlRepository.GetExpenseCategories.");

            List<Entity.ExpenseCategory> categories = new List<Entity.ExpenseCategory>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetExpenseCategories", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Entity.ExpenseCategory category = new Entity.ExpenseCategory();

                    category.ID = reader.GetInt32(0);
                    category.Category = reader.GetString(1);
                    categories.Add(category);
                }
            }

            LogHelper.LogInfo("Exiting SqlRepository.GetExpenseCategories.");
            return categories;
        }

        /// <summary>
        /// Gets autocomplete source for expense description. 
        /// </summary>
        /// <returns> Array of autocomplete strings. </returns>
        public string[] GetExpenseAutocomplete()
        {
            LogHelper.LogInfo("Entering SqlRepository.GetExpenseAutocomplete.");

            string[] source;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetExpenseAutocomplete", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> vals = new List<string>();

                while (reader.Read())
                {
                    vals.Add(reader.GetString(0));
                }

                source = vals.ToArray();
            }

            LogHelper.LogInfo("Exiting SqlRepository.GetExpenseAutocomplete.");
            return source;
        }

        /// <summary>
        /// Gets autocomplete sources for income. 
        /// </summary>
        /// <returns> Array of strings for income sources. </returns>
        public string[] GetIncomeAutocomplete()
        {
            LogHelper.LogInfo("Entering SqlRepository.GetIncomeAutocomplete.");

            string[] source;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("GetIncomeAutocomplete", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<string> vals = new List<string>();

                while (reader.Read())
                {
                    vals.Add(reader.GetString(0));
                }

                source = vals.ToArray();
            }

            LogHelper.LogInfo("Exiting SqlRepository.GetIncomeAutocomplete.");
            return source;
        }

        /// <summary>
        /// Queries for all expenses between a start and end date that match a description pattern. 
        /// </summary>
        /// <param name="start"> Start date. </param>
        /// <param name="end"> End date. </param>
        /// <param name="description"> Description pattern. </param>
        /// <returns> List of expense entities. </returns>
        public List<Entity.Expense> QueryExpenses(DateTime start, DateTime end, string description)
        {
            LogHelper.LogInfo(string.Format("Entering SqlRepository.QueryExpenses with start = {0}, end = {1}, and description = {2}.", start.ToString(), end.ToString(), description));

            List<Entity.Expense> expenses = new List<Entity.Expense>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("QueryExpenses", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartDate", start));
                command.Parameters.Add(new SqlParameter("@EndDate", end));
                command.Parameters.Add(new SqlParameter("@Item", description));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Entity.Expense expense = new Entity.Expense();

                    expense.ID = reader.GetInt32(0);
                    expense.Amount = reader.GetDecimal(1);
                    expense.ExpenseDate = reader.GetDateTime(2);
                    expense.ExpenseTypeID = reader.GetInt32(3);
                    expense.ExpenseCategoryID = reader.GetInt32(4);
                    expense.ExpenseType = reader.GetString(5);
                    expense.Category = reader.GetString(6);                   
                    expense.Description = reader.GetString(7);

                    expenses.Add(expense);
                }
            }

            LogHelper.LogInfo("Exiting SqlRepository.QueryExpenses.");
            return expenses;
        }
    }
}
