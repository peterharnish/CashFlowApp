namespace CashFlow
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvIncome = new System.Windows.Forms.DataGridView();
            this.IncomeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.incomeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.expenseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbxFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvExpenses = new CashFlow.CustomDataGridView();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistory = new CashFlow.CustomDataGridView();
            this.Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.incomeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expenseBindingSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1029, 523);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtDescription);
            this.tabPage1.Controls.Add(this.lblDescription);
            this.tabPage1.Controls.Add(this.dtpEnd);
            this.tabPage1.Controls.Add(this.lblEndDate);
            this.tabPage1.Controls.Add(this.lblStartDate);
            this.tabPage1.Controls.Add(this.dtpStart);
            this.tabPage1.Controls.Add(this.dgvIncome);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.dgvExpenses);
            this.tabPage1.Controls.Add(this.cbxFilter);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1021, 497);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Current";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvIncome
            // 
            this.dgvIncome.AutoGenerateColumns = false;
            this.dgvIncome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncome.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IncomeAmount,
            this.sourceDataGridViewTextBoxColumn,
            this.PaymentDate});
            this.dgvIncome.DataSource = this.incomeBindingSource;
            this.dgvIncome.Location = new System.Drawing.Point(660, 88);
            this.dgvIncome.Name = "dgvIncome";
            this.dgvIncome.Size = new System.Drawing.Size(344, 352);
            this.dgvIncome.TabIndex = 4;
            this.dgvIncome.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvIncome_EditingControlShowing);
            this.dgvIncome.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvIncome_CellEnter);
            this.dgvIncome.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
            // 
            // IncomeAmount
            // 
            this.IncomeAmount.DataPropertyName = "Amount";
            this.IncomeAmount.HeaderText = "Amount";
            this.IncomeAmount.Name = "IncomeAmount";
            // 
            // sourceDataGridViewTextBoxColumn
            // 
            this.sourceDataGridViewTextBoxColumn.DataPropertyName = "Source";
            this.sourceDataGridViewTextBoxColumn.HeaderText = "Source";
            this.sourceDataGridViewTextBoxColumn.Name = "sourceDataGridViewTextBoxColumn";
            // 
            // PaymentDate
            // 
            this.PaymentDate.DataPropertyName = "PaymentDate";
            this.PaymentDate.HeaderText = "Date";
            this.PaymentDate.Name = "PaymentDate";
            // 
            // incomeBindingSource
            // 
            this.incomeBindingSource.DataSource = typeof(CashFlow.Entity.Income);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // expenseBindingSource
            // 
            this.expenseBindingSource.DataSource = typeof(CashFlow.Entity.Expense);
            // 
            // cbxFilter
            // 
            this.cbxFilter.FormattingEnabled = true;
            this.cbxFilter.Location = new System.Drawing.Point(50, 6);
            this.cbxFilter.Name = "cbxFilter";
            this.cbxFilter.Size = new System.Drawing.Size(191, 21);
            this.cbxFilter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvHistory);
            this.tabPage2.Controls.Add(this.dtpEndDate);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.dtpStartDate);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1021, 497);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(98, 38);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "End Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(98, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start Date:";
            // 
            // dgvExpenses
            // 
            this.dgvExpenses.AutoGenerateColumns = false;
            this.dgvExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpenses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.ExpenseAmount,
            this.descriptionDataGridViewTextBoxColumn,
            this.ExpenseDate,
            this.ExpenseType});
            this.dgvExpenses.DataSource = this.expenseBindingSource;
            this.dgvExpenses.Location = new System.Drawing.Point(6, 88);
            this.dgvExpenses.Name = "dgvExpenses";
            this.dgvExpenses.Size = new System.Drawing.Size(648, 352);
            this.dgvExpenses.TabIndex = 2;
            this.dgvExpenses.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dgvExpenses.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvExpenses_CellEnter);
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.Width = 200;
            // 
            // ExpenseAmount
            // 
            this.ExpenseAmount.DataPropertyName = "Amount";
            this.ExpenseAmount.HeaderText = "Amount";
            this.ExpenseAmount.Name = "ExpenseAmount";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // ExpenseDate
            // 
            this.ExpenseDate.DataPropertyName = "ExpenseDate";
            this.ExpenseDate.HeaderText = "Date";
            this.ExpenseDate.Name = "ExpenseDate";
            // 
            // ExpenseType
            // 
            this.ExpenseType.DataPropertyName = "ExpenseType";
            this.ExpenseType.HeaderText = "Expense Type";
            this.ExpenseType.Name = "ExpenseType";
            this.ExpenseType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgvHistory
            // 
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Header,
            this.Amount});
            this.dgvHistory.Location = new System.Drawing.Point(27, 73);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.Size = new System.Drawing.Size(345, 396);
            this.dgvHistory.TabIndex = 4;
            // 
            // Header
            // 
            this.Header.HeaderText = "";
            this.Header.Name = "Header";
            this.Header.Width = 200;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "";
            this.Amount.Name = "Amount";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(503, 10);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 5;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(439, 12);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 6;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(439, 42);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 7;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(503, 36);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpEnd.TabIndex = 8;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(718, 14);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(787, 9);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 20);
            this.txtDescription.TabIndex = 10;
            this.txtDescription.Leave += new System.EventHandler(txtDescription_Leave);
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 547);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Cash Flow";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.incomeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expenseBindingSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        
        
        

       

       

       
       

       
        
        

       

        

       
       

        

       
       

       

       

       

      

        

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvIncome;
        private System.Windows.Forms.BindingSource incomeBindingSource;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.BindingSource expenseBindingSource;
        private System.Windows.Forms.ComboBox cbxFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private CustomDataGridView dgvExpenses;
        private CustomDataGridView dgvHistory;
    }
}

