USE [master]
GO
/****** Object:  Database [finances]    Script Date: 2/9/2016 5:44:05 PM ******/
CREATE DATABASE [finances]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [finances].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [finances] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [finances] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [finances] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [finances] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [finances] SET ARITHABORT OFF 
GO
ALTER DATABASE [finances] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [finances] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [finances] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [finances] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [finances] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [finances] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [finances] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [finances] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [finances] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [finances] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [finances] SET  ENABLE_BROKER 
GO
ALTER DATABASE [finances] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [finances] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [finances] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [finances] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [finances] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [finances] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [finances] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [finances] SET RECOVERY FULL 
GO
ALTER DATABASE [finances] SET  MULTI_USER 
GO
ALTER DATABASE [finances] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [finances] SET DB_CHAINING OFF 
GO
USE [finances]
GO
/****** Object:  Schema [cashflow]    Script Date: 2/9/2016 5:44:05 PM ******/
CREATE SCHEMA [cashflow]
GO
/****** Object:  Schema [stocks]    Script Date: 2/9/2016 5:44:05 PM ******/
CREATE SCHEMA [stocks]
GO
/****** Object:  StoredProcedure [dbo].[ClosePosition]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Closes a position.
-- =============================================
CREATE PROCEDURE [dbo].[ClosePosition]
	@ID int
	,@TotalProfit Numeric(12,2)
AS
BEGIN
	SET NOCOUNT ON;

    Update stocks.Positions
	Set DateClosed = GetDate()
	,TotalProfit = @TotalProfit
	Where ID = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[GetCurrentPositions]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Gets open stock positions.
-- =============================================
CREATE PROCEDURE [dbo].[GetCurrentPositions]
	
AS
BEGIN
	
	SET NOCOUNT ON;

    Select 
		ID
		,Symbol
		,DateOpened
		,CurrentPrice
		,High
		,[Stop]
		,TargetSalePrice
	From stocks.Positions 
	Where DateClosed Is Null
	Order By Symbol

	Select
		A.ID
		,A.PositionId
		,A.PaymentDate
		,A.Amount
	From stocks.Dividends A Inner Join stocks.Positions B on A.PositionId = B.ID 
	Where B.DateClosed Is Null

	Select
		A.ID
		,A.PositionId
		,A.NumberOfShares
		,A.TotalPrice
		,A.R
		,A.PurchaseDate
	From stocks.Purchases A Inner Join stocks.Positions B on A.PositionId = B.ID
	Where B.DateClosed Is Null

	Select
		A.ID
		,A.SaleDate
		,A.NumberOfShares
		,A.TotalPrice
		,A.PositionId
	From stocks.Sales A Inner Join stocks.Positions B on A.PositionId = B.ID
	Where B.DateClosed Is Null

END

GO
/****** Object:  StoredProcedure [dbo].[GetExpenseAutocomplete]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets autocomplete suggestions for expense descriptions. 
-- =============================================
CREATE PROCEDURE [dbo].[GetExpenseAutocomplete]
	
AS
BEGIN
	SET NOCOUNT ON;

    Select
		Item
	From cashflow.ExpenseItems
END

GO
/****** Object:  StoredProcedure [dbo].[GetExpenseCategories]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets expense categories. 
-- =============================================
CREATE PROCEDURE [dbo].[GetExpenseCategories]
	
AS
BEGIN
	SET NOCOUNT ON;

    Select
		ID
		,Category
	From cashflow.ExpenseCategories
END

GO
/****** Object:  StoredProcedure [dbo].[GetExpenses]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets the expenses within a date range.
-- =============================================
CREATE PROCEDURE [dbo].[GetExpenses]
	@StartDate DateTime
	,@EndDate DateTime
AS
BEGIN
	SET NOCOUNT ON;

    Select
		A.ID
		,A.Amount
		,A.ExpenseDate
		,A.[Type] As ExpenseTypeId
		,A.Category As ExpenseCategoryId
		,A.Item As ExpenseItemId
		,B.Category
		,C.[Type]
		,D.Item
	From cashflow.Expenses A Inner Join cashflow.ExpenseCategories B on A.Category = B.ID
	Inner Join cashflow.ExpenseTypes C on A.[Type] = C.ID
	Inner Join cashflow.ExpenseItems D on A.Item = D.ID
	Where A.ExpenseDate >= @StartDate And A.ExpenseDate <= @EndDate

END

GO
/****** Object:  StoredProcedure [dbo].[GetExpenseTypes]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets the expense types. 
-- =============================================
CREATE PROCEDURE [dbo].[GetExpenseTypes]
AS
BEGIN
	SET NOCOUNT ON;

    Select
		ID
		,[Type]
	From cashflow.ExpenseTypes
END

GO
/****** Object:  StoredProcedure [dbo].[GetHistoryPositions]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Gets positions open between 2 given dates. 
-- =============================================
CREATE PROCEDURE [dbo].[GetHistoryPositions]
	@StartDate DateTime
	,@DateClosed DateTime
AS
BEGIN
	SET NOCOUNT ON;

    Select
		ID
		,Symbol
		,DateOpened
		,CurrentPrice
		,High
		,[Stop]
		,DateClosed
		,TotalProfit
	From stocks.Positions
	Where DateClosed >= @StartDate And DateClosed <= @DateClosed
	Order By Symbol

	Select
		A.ID
		,A.PositionId
		,A.PaymentDate
		,A.Amount
	From stocks.Dividends A Inner Join stocks.Positions B On A.PositionId = B.ID
	Where B.DateClosed >= @StartDate And B.DateClosed <= @DateClosed

	Select
		A.ID
		,A.PositionId
		,A.NumberOfShares
		,A.TotalPrice
		,A.R
		,A.PurchaseDate
	From stocks.Purchases A Inner Join stocks.Positions B On A.PositionId = B.ID
	Where B.DateClosed >= @StartDate And B.DateClosed <= @DateClosed

	Select
		A.ID
		,A.SaleDate
		,A.NumberOfShares
		,A.TotalPrice
		,A.PositionId
	From stocks.Sales A Inner Join stocks.Positions B On A.PositionId = B.ID
	Where B.DateClosed >= @StartDate And B.DateClosed <= @DateClosed

END




GO
/****** Object:  StoredProcedure [dbo].[GetIncome]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets income from a given date range.
-- =============================================
CREATE PROCEDURE [dbo].[GetIncome]
	@StartDate DateTime
	,@EndDate DateTime
AS
BEGIN
	SET NOCOUNT ON;

    Select
		A.ID
		,A.Amount
		,A.[Source] as SourceId
		,A.PaymentDate 
		,B.[Source]
	From cashflow.Income A Inner Join cashflow.IncomeSources B on A.Source = B.ID
	Where A.PaymentDate >= @StartDate and A.PaymentDate <= @EndDate
END

GO
/****** Object:  StoredProcedure [dbo].[GetIncomeAutocomplete]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets autocomplete suggestions for income sources.
-- =============================================
CREATE PROCEDURE [dbo].[GetIncomeAutocomplete]
	
AS
BEGIN
	SET NOCOUNT ON;

    Select
		[Source]
	From cashflow.IncomeSources
END

GO
/****** Object:  StoredProcedure [dbo].[GetPositionBySymbol]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets the open position with the given symbol.
-- =============================================
CREATE PROCEDURE [dbo].[GetPositionBySymbol]
	@Symbol varchar(5)
AS
BEGIN
	SET NOCOUNT ON;

    Select
		ID
		,Symbol
		,DateOpened
		,CurrentPrice
		,High
		,[Stop]
		,TotalProfit
		,TargetSalePrice
	From stocks.Positions
	Where DateClosed Is Null and Symbol = @Symbol

	Select
		A.ID
		,A.PositionId
		,A.PaymentDate
		,A.Amount
	From stocks.Dividends A Inner Join stocks.Positions B on A.PositionId = B.ID 
	Where B.Symbol = @Symbol And B.DateClosed Is Null

	Select
		A.ID
		,A.PositionId
		,A.NumberOfShares
		,A.TotalPrice
		,A.R
		,A.PurchaseDate
	From stocks.Purchases A Inner Join stocks.Positions B on A.PositionId = B.ID
	Where B.Symbol = @Symbol And B.DateClosed Is Null

	Select
		A.ID
		,A.SaleDate
		,A.NumberOfShares
		,A.TotalPrice
		,A.PositionId
	From stocks.Sales A Inner Join stocks.Positions B on A.PositionId = B.ID
	Where B.Symbol = @Symbol And B.DateClosed Is Null
END


GO
/****** Object:  StoredProcedure [dbo].[GetPositionID]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Gets the ID for an open position with the given symbol.
-- =============================================
CREATE PROCEDURE [dbo].[GetPositionID] 
	@Symbol varchar(5)
AS
BEGIN
	SET NOCOUNT ON;

    Select 
		ID
	From stocks.Positions
	Where DateClosed Is Null and Symbol = @Symbol
END

GO
/****** Object:  StoredProcedure [dbo].[InsertDividend]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Inserts a stock dividend.
-- =============================================
CREATE PROCEDURE [dbo].[InsertDividend]
	@PositionId int
	,@Amount Numeric(10,2)
	,@Ret Int Out
AS
BEGIN
	SET NOCOUNT ON;

    Insert into stocks.Dividends
	(
		PositionId
		,PaymentDate
		,Amount
	)
	Select
		@PositionId
		,GetDate()
		,@Amount

	Select @Ret = Scope_Identity()
END

GO
/****** Object:  StoredProcedure [dbo].[InsertExpense]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Inserts an expense.
-- =============================================
CREATE PROCEDURE [dbo].[InsertExpense]
	@Amount Numeric(12,2)
	,@ExpenseDate DateTime
	,@Type Int
	,@Category Int
	,@Description varchar(100)
	,@Ret Int Out
AS
BEGIN
	SET NOCOUNT ON;

    Declare @ItemId Int

	Set @ItemId = (Select ID from cashflow.ExpenseItems Where Item = @Description)

	If @ItemId Is Null
	Begin
		Insert Into cashflow.ExpenseItems
		(
			Item
		)
		Select
			@Description

		Select @ItemId = Scope_Identity()
	End

	Insert into cashflow.Expenses
	(
		Amount
		,ExpenseDate
		,[Type]
		,Category
		,Item
	)
	Select
		@Amount
		,@ExpenseDate
		,@Type
		,@Category
		,@ItemId

	Select @Ret = Scope_Identity()
END


GO
/****** Object:  StoredProcedure [dbo].[InsertIncome]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Inserts an income record.
-- =============================================
CREATE PROCEDURE [dbo].[InsertIncome]
	@Amount Numeric(12,2)
	,@Source varchar(100)
	,@PaymentDate DateTime
	,@Ret Int Out
AS
BEGIN	
	SET NOCOUNT ON;

    Declare @SourceId Int

	Set @SourceId = (Select ID From cashflow.IncomeSources Where [Source] = @Source)

	If @SourceId Is Null
	Begin
		Insert Into cashflow.IncomeSources
		(
			[Source]
		)
		Select
			@Source

		Select @SourceId = Scope_Identity()
	End

	Insert Into cashflow.Income
	(
		Amount
		,[Source]
		,PaymentDate
	)
	Select
		@Amount
		,@SourceId
		,@PaymentDate

	Select @Ret = Scope_Identity()
END


GO
/****** Object:  StoredProcedure [dbo].[InsertPosition]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2013
-- Description:	Inserts a stock position.
-- =============================================
CREATE PROCEDURE [dbo].[InsertPosition]
	@Symbol Varchar(5)
	,@Stop Numeric(2,2)
	,@TargetSalePrice Numeric(8,2)
	,@Ret Int Out
AS
BEGIN
	SET NOCOUNT ON;

    Insert into stocks.Positions
	(
		Symbol
		,DateOpened
		,CurrentPrice
		,High
		,[Stop]
		,DateClosed
		,TotalProfit
		,TargetSalePrice
	)
	Select
		@Symbol
		,GetDate()
		,0
		,0
		,@Stop
		,null
		,null
		,@TargetSalePrice

	Select @Ret = Scope_Identity()
END

GO
/****** Object:  StoredProcedure [dbo].[InsertPurchase]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Inserts a purchase. 
-- =============================================
CREATE PROCEDURE [dbo].[InsertPurchase]
	@PositionId int
	,@NumberOfShares int
	,@TotalPrice Numeric(10,2)
	,@R Numeric(10,2)
	,@Ret Int Out
AS
BEGIN
	SET NOCOUNT ON;

	Declare @Placeholder Int
	,@Now DateTime

	Insert into stocks.Purchases
	(
		PositionId
		,NumberOfShares
		,TotalPrice
		,R
		,PurchaseDate
	)
	Select
		@PositionId
		,@NumberOfShares
		,@TotalPrice
		,@R
		,GetDate()

	Select @Ret = Scope_Identity()
	Select @Now = GetDate()

	Exec dbo.InsertExpense @TotalPrice, @Now, 2, 11, 'Stock Purchase', @Placeholder
END




GO
/****** Object:  StoredProcedure [dbo].[InsertSale]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Inserts a stock sale. 
-- =============================================
CREATE PROCEDURE [dbo].[InsertSale]
	@NumberOfShares Int
	,@TotalPrice Numeric(12,2)
	,@PositionId Int
	,@Ret Int Out
AS
BEGIN
	SET NOCOUNT ON;

	Declare @Placeholder Int
	,@Now DateTime

   Insert into stocks.Sales
   (
	SaleDate
	,NumberOfShares
	,TotalPrice
	,PositionId
   )
   Select
	GetDate()
	,@NumberOfShares
	,@TotalPrice
	,@PositionId

   Select @Ret = Scope_Identity()

   Select @Now = GetDate()

   Exec InsertIncome @TotalPrice, 'Stock sale', @Now, @Placeholder
END


GO
/****** Object:  StoredProcedure [dbo].[QueryExpenses]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Peter Harnish
-- Create date: November 7, 2015
-- Description:	Gets expenses between 2 given dates where the item matches a query pattern.
-- =============================================
CREATE PROCEDURE [dbo].[QueryExpenses]
	@StartDate DateTime
	,@EndDate DateTime
	,@Item VarChar(100)
AS
BEGIN
	SET NOCOUNT ON;

    Select
		A.ID
		,A.Amount
		,A.ExpenseDate
		,A.[Type] As TypeId
		,A.Category As CategoryId
		,B.[Type]
		,C.Category
		,D.Item
	From cashflow.Expenses A Inner Join cashflow.ExpenseTypes B On A.[Type] = B.ID
	Inner Join cashflow.ExpenseCategories C On A.Category = C.ID
	Inner Join cashflow.ExpenseItems D On A.Item = D.ID
	Where A.ExpenseDate >= @StartDate And A.ExpenseDate <= @EndDate And D.Item Like @Item + '%'
END


GO
/****** Object:  StoredProcedure [dbo].[UpdateCurrentPrice]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Peter Harnish
-- Create date: November 6, 2015
-- Description:	Updates the current stock price.
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCurrentPrice]
	@ID int
	,@CurrentPrice Numeric(8,2)
	,@High Numeric(8,2)
AS
BEGIN
	SET NOCOUNT ON;

    Update stocks.Positions
	Set CurrentPrice = @CurrentPrice
	,High = @High
	Where ID = @ID
END

GO
/****** Object:  Table [cashflow].[ExpenseCategories]    Script Date: 2/9/2016 5:44:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [cashflow].[ExpenseCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](50) NOT NULL,
 CONSTRAINT [pk_ExpenseCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [cashflow].[ExpenseItems]    Script Date: 2/9/2016 5:44:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [cashflow].[ExpenseItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Item] [varchar](100) NOT NULL,
 CONSTRAINT [pk_ExpenseItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [cashflow].[Expenses]    Script Date: 2/9/2016 5:44:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [cashflow].[Expenses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [numeric](12, 2) NOT NULL,
	[ExpenseDate] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
	[Category] [int] NOT NULL,
	[Item] [int] NOT NULL,
 CONSTRAINT [pk_Expenses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [cashflow].[ExpenseTypes]    Script Date: 2/9/2016 5:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [cashflow].[ExpenseTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [char](1) NOT NULL,
 CONSTRAINT [pk_ExpenseTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [cashflow].[Income]    Script Date: 2/9/2016 5:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [cashflow].[Income](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [numeric](12, 2) NOT NULL,
	[Source] [int] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
 CONSTRAINT [pk_Income] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [cashflow].[IncomeSources]    Script Date: 2/9/2016 5:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [cashflow].[IncomeSources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Source] [varchar](100) NOT NULL,
 CONSTRAINT [pk_IncomeSources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [stocks].[Dividends]    Script Date: 2/9/2016 5:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [stocks].[Dividends](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PositionId] [int] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[Amount] [numeric](10, 2) NOT NULL,
 CONSTRAINT [pk_Dividends] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [stocks].[Positions]    Script Date: 2/9/2016 5:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [stocks].[Positions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Symbol] [varchar](5) NOT NULL,
	[DateOpened] [datetime] NOT NULL,
	[CurrentPrice] [numeric](8, 2) NOT NULL,
	[High] [numeric](8, 2) NOT NULL,
	[Stop] [numeric](2, 2) NOT NULL,
	[DateClosed] [datetime] NULL,
	[TotalProfit] [numeric](12, 2) NULL,
	[TargetSalePrice] [numeric](8, 2) NOT NULL,
 CONSTRAINT [pk_Positions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [stocks].[Purchases]    Script Date: 2/9/2016 5:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [stocks].[Purchases](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PositionId] [int] NOT NULL,
	[NumberOfShares] [int] NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[R] [numeric](10, 2) NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
 CONSTRAINT [pk_Purchases] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [stocks].[Sales]    Script Date: 2/9/2016 5:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [stocks].[Sales](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SaleDate] [datetime] NOT NULL,
	[NumberOfShares] [int] NOT NULL,
	[TotalPrice] [numeric](12, 2) NOT NULL,
	[PositionId] [int] NOT NULL,
 CONSTRAINT [pk_Sales] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [cashflow].[Expenses]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [cashflow].[ExpenseCategories] ([ID])
GO
ALTER TABLE [cashflow].[Expenses]  WITH CHECK ADD FOREIGN KEY([Item])
REFERENCES [cashflow].[ExpenseItems] ([ID])
GO
ALTER TABLE [cashflow].[Expenses]  WITH CHECK ADD FOREIGN KEY([Type])
REFERENCES [cashflow].[ExpenseTypes] ([ID])
GO
ALTER TABLE [cashflow].[Income]  WITH CHECK ADD FOREIGN KEY([Source])
REFERENCES [cashflow].[IncomeSources] ([ID])
GO
ALTER TABLE [stocks].[Dividends]  WITH NOCHECK ADD FOREIGN KEY([PositionId])
REFERENCES [stocks].[Positions] ([ID])
GO
ALTER TABLE [stocks].[Purchases]  WITH NOCHECK ADD FOREIGN KEY([PositionId])
REFERENCES [stocks].[Positions] ([ID])
GO
ALTER TABLE [stocks].[Sales]  WITH NOCHECK ADD FOREIGN KEY([PositionId])
REFERENCES [stocks].[Positions] ([ID])
GO

USE [master]
GO
ALTER DATABASE [finances] SET  READ_WRITE 
GO

Use [finances]
Go

Insert into cashflow.ExpenseCategories
(
	Category
)
select 'Debt Payments' union all
select 'Major Purchases' union all
select 'Recreation' union all
select 'Household expenses' union all
select 'Restaurant, Food, Groceries' union all
select 'Auto' union all
select 'Insurance and Medical' union all
select 'Clothing' union all
select 'Education' union all
select 'Taxes' union all
select 'Investments' union all
select 'Other'
go

insert into cashflow.ExpenseTypes
(
	[Type]
)
select 'E' union all
select 'N' union all
select 'W' union all
select 'S'
go

