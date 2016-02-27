using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CashFlow.BusinessRules;
using CashFlow.DataAccess;
using CashFlow.Entity;
using Logging;

namespace CashFlow
{
    public partial class Form1 : Form
    {
        public static IRepository Repository;

        public Form1()
        {
            InitializeComponent();
            SetFilterDataSource();
            SetColumnFormats();
            GetExpenseCategories();
            GetExpenseTypes();
            GetExpenses();
            GetIncome();
            DateTime lastMonth = DateTime.Now - new TimeSpan(30, 0, 0, 0);
            this.dtpStartDate.Value = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            this.dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            GetHistory();
            this.cbxFilter.SelectedValueChanged += new System.EventHandler(cbxFilter_SelectedValueChanged);
        }

        /// <summary>
        /// Repository. 
        /// </summary>
        private BR br = new BR(Repository);

        /// <summary>
        /// List of expenses. 
        /// </summary>
        private List<Expense> Expenses;

        /// <summary>
        /// Expense categories. 
        /// </summary>
        private List<ExpenseCategory> ExpenseCategories;

        /// <summary>
        /// Expense types. 
        /// </summary>
        private List<ExpenseType> ExpenseTypes;

        /// <summary>
        /// List of income sources. 
        /// </summary>
        private List<Income> CurrentIncome;

        /// <summary>
        /// Fills the expense data grid view. 
        /// </summary>
        private void GetExpenses()
        {
            LogHelper.LogInfo("Entering Form1.GetExpenses.");

            try
            {
                this.Expenses = this.br.GetCurrentExpenses();
                this.dgvExpenses.DataSource = new ExpenseList(this.Expenses.OrderBy(x => x.ExpenseDate).ToList());
               
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.GetExpenses.");
        }       

        /// <summary>
        /// Gets income for this month. 
        /// </summary>
        private void GetIncome()
        {
            LogHelper.LogInfo("Entering Form1.GetIncome.");

            try
            {
                this.CurrentIncome = this.br.GetCurrentIncome();
                this.dgvIncome.DataSource = new IncomeList(this.CurrentIncome);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.GetIncome.");
        }

        /// <summary>
        /// Sets the column formats. 
        /// </summary>
        private void SetColumnFormats()
        {
            LogHelper.LogInfo("Entering Form1.SetColumnFormats.");

            this.ExpenseAmount.DefaultCellStyle.Format = "c";
            this.ExpenseDate.DefaultCellStyle.Format = "d";
            this.IncomeAmount.DefaultCellStyle.Format = "c";
            this.PaymentDate.DefaultCellStyle.Format = "d";
            this.Amount.DefaultCellStyle.Format = "c";

            LogHelper.LogInfo("Exiting Form1.SetColumnFormats.");
        }

        /// <summary>
        /// Sets the filter combo box data source. 
        /// </summary>
        private void SetFilterDataSource()
        {
            LogHelper.LogInfo("Entering Form1.SetFilterDataSource.");

            this.cbxFilter.DataSource = this.br.GetExpenseCategories();
            this.cbxFilter.DisplayMember = "Category";

            LogHelper.LogInfo("Exiting Form1.SetFilterDataSource.");
        }

        /// <summary>
        /// Gets the expense categories. 
        /// </summary>
        private void GetExpenseCategories()
        {
            LogHelper.LogInfo("Entering Form1.GetExpenseCategories.");

            this.ExpenseCategories = this.br.GetExpenseCategories();

            LogHelper.LogInfo("Exiting Form1.GetExpenseCategories.");
        }

        /// <summary>
        /// Gets expense types. 
        /// </summary>
        private void GetExpenseTypes()
        {
            LogHelper.LogInfo("Entering Form1.GetExpenseTypes.");

            this.ExpenseTypes = this.br.GetExpenseTypes();

            LogHelper.LogInfo("Exiting Form1.GetExpenseTypes.");
        }

        /// <summary>
        /// Gets the history. 
        /// </summary>
        private void GetHistory()
        {
            LogHelper.LogInfo("Entering Form1.GetHistory.");

            try
            {
                this.dgvHistory.Rows.Clear();
                History history = this.br.GetHistory(this.dtpStartDate.Value, this.dtpEndDate.Value);
                this.dgvHistory.Rows.Add("Total Income", history.TotalIncome);
                this.dgvHistory.Rows.Add("Debt Payments", history.DebtPayments);
                this.dgvHistory.Rows.Add("Major Purchases", history.MajorPurchases);
                this.dgvHistory.Rows.Add("Recreation", history.Recreation);
                this.dgvHistory.Rows.Add("Household Expenses", history.HouseholdExpenses);
                this.dgvHistory.Rows.Add("Food, Restaurant, Groceries", history.FoodExpenses);
                this.dgvHistory.Rows.Add("Insurance and Medical", history.InsuranceAndMedical);
                this.dgvHistory.Rows.Add("Auto", history.Auto);
                this.dgvHistory.Rows.Add("Clothing", history.Clothing);
                this.dgvHistory.Rows.Add("Education", history.Education);
                this.dgvHistory.Rows.Add("Taxes", history.Taxes);
                this.dgvHistory.Rows.Add("Investments", history.Investments);
                this.dgvHistory.Rows.Add("Other", history.Other);
                this.dgvHistory.Rows.Add("Total Expenses", history.TotalExpenses);
                this.dgvHistory.Rows.Add("Net Cash Flow", history.NetCashFlow);

                Font font = new Font(this.dgvHistory.DefaultCellStyle.Font.FontFamily, this.dgvHistory.DefaultCellStyle.Font.Size, FontStyle.Bold);
                this.dgvHistory.Rows[0].DefaultCellStyle.Font = font;
                this.dgvHistory.Rows[13].DefaultCellStyle.Font = font;
                this.dgvHistory.Rows[14].DefaultCellStyle.Font = font;

                if (history.NetCashFlow < 0)
                {
                    this.dgvHistory.Rows[14].Cells[1].Style.ForeColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.GetHistory.");
        }

        private void dataGridView1_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                var source = new AutoCompleteStringCollection();

                if (this.dgvExpenses.CurrentCell.ColumnIndex == 0)
                {
                    source.AddRange(this.ExpenseCategories.Select(x => x.Category).ToArray());
                    AssignAutoCompleteSource(source, e);
                }
                else if (this.dgvExpenses.CurrentCell.ColumnIndex == 2)
                {
                    source.AddRange(this.br.GetExpenseAutocomplete());
                    AssignAutoCompleteSource(source, e);
                }
                else if (this.dgvExpenses.CurrentCell.ColumnIndex == 4)
                {
                    source.AddRange(this.ExpenseTypes.Select(x => x.Name).ToArray());
                    AssignAutoCompleteSource(source, e);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void dgvIncome_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            var source = new AutoCompleteStringCollection();

            if (this.dgvIncome.CurrentCell.ColumnIndex == 1)
            {
                source.AddRange(this.br.GetIncomeAutocomplete());
                AssignAutoCompleteSource(source, e);
            }            
        }

        private void AssignAutoCompleteSource(AutoCompleteStringCollection source, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tbx = e.Control as TextBox;

            if (tbx != null)
            {
                tbx.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tbx.AutoCompleteCustomSource = source;
                tbx.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        /// <summary>
        /// Gets history. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_ValueChanged(object sender, System.EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.dtp_ValueChanged.");

            GetHistory();

            LogHelper.LogInfo("Exiting Form1.dtp_ValueChanged.");
        }

        private void cbxFilter_SelectedValueChanged(object sender, System.EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.cbxFilter_SelectedValueChanged.");

            this.dgvExpenses.DataSource = new ExpenseList(this.Expenses.Where(x => x.ExpenseCategoryID == ((ExpenseCategory)this.cbxFilter.SelectedItem).ID).OrderBy(x => x.ExpenseDate).ToList());

            LogHelper.LogInfo("Entering Form1.cbxFilter_SelectedValueChanged.");
        }

        /// <summary>
        /// Saves the input. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.btnSave_Click.");

            try
            {
                foreach (Expense expense in (List<Expense>)this.dgvExpenses.DataSource)
                {
                    if ((expense.ID == 0) && (expense.Amount > 0))
                    {
                        expense.ExpenseCategoryID = this.ExpenseCategories.Where(x => string.Compare(x.Category, expense.Category.Trim(), true) == 0).FirstOrDefault().ID;
                        expense.ExpenseTypeID = this.ExpenseTypes.Where(x => string.Compare(x.Name, expense.ExpenseType.Trim(), true) == 0).FirstOrDefault().ID;
                        expense.Description = expense.Description.Trim();
                        this.br.RecordExpense(expense);
                    }
                }

                foreach (Income income in (List<Income>)this.dgvIncome.DataSource)
                {
                    if ((income.ID == 0) && (income.Amount > 0))
                    {
                        income.Source = income.Source.Trim();
                        this.br.RecordIncome(income);
                    }
                }

                GetExpenses();
                GetIncome();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.btnSave_Click.");
        }

        /// <summary>
        /// Queries for records based on the description. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescription_Leave(object sender, System.EventArgs e)
        {
            LogHelper.LogInfo("Entering Form1.txtDescription_Leave.");

            try
            {
                this.Expenses = this.br.QueryExpenses(this.dtpStart.Value, this.dtpEnd.Value, this.txtDescription.Text);
                this.dgvExpenses.DataSource = this.Expenses;

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }

            LogHelper.LogInfo("Exiting Form1.txtDescription_Leave.");
        }               

        /// <summary>
        /// Tabs out of a control when the Enter key is pressed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
             LogHelper.LogInfo("Entering Form1.Control_KeyDown.");

             if (e.KeyCode == Keys.Enter)
             {
                 SendKeys.Send("{TAB}");
             }

            LogHelper.LogInfo("Exiting Form1.Control_KeyDown.");
        }

        /// <summary>
        /// Sets the date to today when entering the date cell. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExpenses_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            LogHelper.LogInfo("Entering dgvExpenses_CellEnter.");

            if (e.ColumnIndex == 3)
            {
                Expense expense = this.dgvExpenses.Rows[e.RowIndex].DataBoundItem as Expense;

                if (expense != null && expense.ExpenseDate == null)
                {
                    expense.ExpenseDate = DateTime.Now;
                }
            }

            LogHelper.LogInfo("Exiting dgvExpenses_CellEnter.");
        }

        /// <summary>
        /// Sets the date to today when entering the date cell on the Income data grid view. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIncome_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            LogHelper.LogInfo("Entering dgvIncome_CellEnter.");

            if (e.ColumnIndex == 2)
            {
                Income income = this.dgvIncome.Rows[e.RowIndex].DataBoundItem as Income;

                if (income != null && income.PaymentDate == null)
                {
                    income.PaymentDate = DateTime.Now;
                }
            }
            
            LogHelper.LogInfo("Exiting dgvIncome_CellEnter.");
        }

    }
}
