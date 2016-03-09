# CashFlowApp
The CashFlowApp is a WinForms application written for the .NET 4.0 platform. It records income and expenses and calculates cash flow for a household. It uses the Repository pattern to abstract out the data source. A script file is provided which creates a SQL Server database to interoperate with the App. The App has 3 layers - repository, Business Rules, and Windows Forms UI, which are loosely coupled.
The form has 2 tabs - Current and History. The Current tab displays 2 data grid views - Expenses on the left, Income on the right. Expenses and Income from the current month are displayed. The Expenses grid view displays the expense Category, Amount, Description, Date, and Type.
There are 12 expense Categories - Auto; Clothing; Debt Payments; Education; Household Expenses; Insurance and Medical; Investments; Major Purchases; Other; Recreation; Restaurant, Food, Groceries; and Taxes. There are 4 expense Types: N(orth) for investments; S(outh) for big-ticket items that will depreciate; E(ast) for living expenses; and W(est) for luxuries. The form uses intellisense for the expense category, description, and type. The app has the capability to add new Descriptions but not new Categories or Types. Like the Expense Description, the Income Source column uses intellisense based on prior saved Sources. Add expenses and income through the grid views, click Save, and the records are saved to the repository.
When you change the value of the Filter drop down, the system shows Expense records for this month that belong to the chosen Category.
Type a value in the Description text box. When focus leaves the text box, the system searches for all Expenses that were recorded between the Start Date and End Date in the date pickers and that have the given description. An implied wildcard is appended to the description. So if you type in Gro, the system will find expenses with the descriptions Groceries and Growth Club Dues.
The History tab summarizes cash flow for a given date range. By default, the month prior to the current month is picked. The grid view displays the sum of income, followed by expense totals according to category. Then the total of expenses is given followed by the net cash flow.
