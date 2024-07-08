USE [master]
GO
/****** Object:  Database [MilkSalesManagement]    Script Date: 7/5/2024 10:11:25 PM ******/
CREATE DATABASE [MilkSalesManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MilkSalesManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.QHUY\MSSQL\DATA\MilkSalesManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MilkSalesManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.QHUY\MSSQL\DATA\MilkSalesManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MilkSalesManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MilkSalesManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MilkSalesManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MilkSalesManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MilkSalesManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MilkSalesManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MilkSalesManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MilkSalesManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MilkSalesManagement] SET  MULTI_USER 
GO
ALTER DATABASE [MilkSalesManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MilkSalesManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MilkSalesManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MilkSalesManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MilkSalesManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MilkSalesManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MilkSalesManagement', N'ON'
GO
ALTER DATABASE [MilkSalesManagement] SET QUERY_STORE = OFF
GO
USE [MilkSalesManagement]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[productId] [int] NULL,
	[quantity] [int] NULL,
	[price] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[createdDate] [date] NULL,
	[totalAmount] [decimal](18, 0) NULL,
	[paymentId] [int] NULL,
	[deliveryAdress] [nvarchar](255) NULL,
	[status] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[productId] [int] NULL,
	[quantity] [int] NULL,
	[price] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentMethodName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[price] [decimal](18, 0) NULL,
	[quantity] [int] NULL,
	[expirationDate] [date] NULL,
	[brand] [nvarchar](255) NULL,
	[imgUrl] [nvarchar](255) NULL,
	[volume] [decimal](18, 0) NULL,
	[ageAllowed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[content] [nchar](255) NULL,
	[rate] [int] NULL,
	[userId] [int] NULL,
	[productId] [int] NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rolename] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/5/2024 10:11:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleid] [int] NULL,
	[username] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[fullName] [nvarchar](255) NULL,
	[phone] [numeric](18, 0) NULL,
	[address] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 
GO
INSERT [dbo].[Cart] ([id], [userId], [productId], [quantity], [price]) VALUES (1, 2, 1, 2, CAST(3 AS Decimal(18, 0)))
GO
INSERT [dbo].[Cart] ([id], [userId], [productId], [quantity], [price]) VALUES (2, 2, 2, 1, CAST(2 AS Decimal(18, 0)))
GO
INSERT [dbo].[Cart] ([id], [userId], [productId], [quantity], [price]) VALUES (3, 3, 3, 3, CAST(5 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([id], [userId], [createdDate], [totalAmount], [paymentId], [deliveryAdress], [status]) VALUES (1, 2, CAST(N'2024-06-20' AS Date), CAST(5 AS Decimal(18, 0)), 1, N'456 Customer Ave', N'Processing')
GO
INSERT [dbo].[Order] ([id], [userId], [createdDate], [totalAmount], [paymentId], [deliveryAdress], [status]) VALUES (2, 3, CAST(N'2024-06-21' AS Date), CAST(5 AS Decimal(18, 0)), 2, N'789 Customer Blvd', N'Shipped')
GO
INSERT [dbo].[Order] ([id], [userId], [createdDate], [totalAmount], [paymentId], [deliveryAdress], [status]) VALUES (21, 1, CAST(N'2024-07-04' AS Date), CAST(30000 AS Decimal(18, 0)), 1, N'string', N'Pending')
GO
INSERT [dbo].[Order] ([id], [userId], [createdDate], [totalAmount], [paymentId], [deliveryAdress], [status]) VALUES (22, 1, CAST(N'2024-07-04' AS Date), CAST(30000 AS Decimal(18, 0)), 2, N'string', N'Pending')
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 
GO
INSERT [dbo].[OrderDetail] ([id], [orderId], [productId], [quantity], [price]) VALUES (1, 1, 1, 2, CAST(3 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([id], [orderId], [productId], [quantity], [price]) VALUES (2, 1, 2, 1, CAST(2 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([id], [orderId], [productId], [quantity], [price]) VALUES (3, 2, 3, 3, CAST(5 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([id], [orderId], [productId], [quantity], [price]) VALUES (19, 21, 1, 2, CAST(15000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([id], [orderId], [productId], [quantity], [price]) VALUES (20, 22, 1, 2, CAST(15000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 
GO
INSERT [dbo].[Payment] ([id], [PaymentMethodName]) VALUES (1, N'Credit Card')
GO
INSERT [dbo].[Payment] ([id], [PaymentMethodName]) VALUES (2, N'PayPal')
GO
INSERT [dbo].[Payment] ([id], [PaymentMethodName]) VALUES (3, N'Bank Transfer')
GO
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([id], [name], [description], [price], [quantity], [expirationDate], [brand], [imgUrl], [volume], [ageAllowed]) VALUES (1, N'Milk A', N'Fresh Milk A', CAST(15000 AS Decimal(18, 0)), 96, CAST(N'2024-12-31' AS Date), N'Brand A', N'http://example.com/img/milkA.jpg', NULL, NULL)
GO
INSERT [dbo].[Product] ([id], [name], [description], [price], [quantity], [expirationDate], [brand], [imgUrl], [volume], [ageAllowed]) VALUES (2, N'Milk B', N'Organic Milk B', CAST(19000 AS Decimal(18, 0)), 200, CAST(N'2025-01-15' AS Date), N'Brand B', N'http://example.com/img/milkB.jpg', NULL, NULL)
GO
INSERT [dbo].[Product] ([id], [name], [description], [price], [quantity], [expirationDate], [brand], [imgUrl], [volume], [ageAllowed]) VALUES (3, N'Milk C', N'Low Fat Milk C', CAST(2000 AS Decimal(18, 0)), 150, CAST(N'2024-11-30' AS Date), N'Brand C', N'http://example.com/img/milkC.jpg', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Review] ON 
GO
INSERT [dbo].[Review] ([id], [content], [rate], [userId], [productId]) VALUES (1, N'Great milk, very fresh!                                                                                                                                                                                                                                        ', 5, 1, 1)
GO
INSERT [dbo].[Review] ([id], [content], [rate], [userId], [productId]) VALUES (2, N'Average taste.                                                                                                                                                                                                                                                 ', 3, 2, 1)
GO
INSERT [dbo].[Review] ([id], [content], [rate], [userId], [productId]) VALUES (3, N'Good value for money.                                                                                                                                                                                                                                          ', 4, 3, 2)
GO
INSERT [dbo].[Review] ([id], [content], [rate], [userId], [productId]) VALUES (4, N'Not my favorite.                                                                                                                                                                                                                                               ', 2, 1, 3)
GO
INSERT [dbo].[Review] ([id], [content], [rate], [userId], [productId]) VALUES (5, N'Excellent quality!                                                                                                                                                                                                                                             ', 5, 2, 3)
GO
SET IDENTITY_INSERT [dbo].[Review] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([id], [rolename]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Role] ([id], [rolename]) VALUES (2, N'Customer')
GO
INSERT [dbo].[Role] ([id], [rolename]) VALUES (3, N'Supplier')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([id], [roleid], [username], [password], [email], [fullName], [phone], [address]) VALUES (1, 1, N'admin', N'admin123', N'admin@example.com', N'Admin User', CAST(1234567890 AS Numeric(18, 0)), N'123 Admin St')
GO
INSERT [dbo].[User] ([id], [roleid], [username], [password], [email], [fullName], [phone], [address]) VALUES (2, 2, N'john_doe', N'password123', N'john@example.com', N'John Doe', CAST(2345678901 AS Numeric(18, 0)), N'456 Customer Ave')
GO
INSERT [dbo].[User] ([id], [roleid], [username], [password], [email], [fullName], [phone], [address]) VALUES (3, 2, N'jane_doe', N'password456', N'jane@example.com', N'Jane Doe', CAST(3456789012 AS Numeric(18, 0)), N'789 Customer Blvd')
GO
INSERT [dbo].[User] ([id], [roleid], [username], [password], [email], [fullName], [phone], [address]) VALUES (4, 3, N'supplier', N'supplier123', N'supplier@example.com', N'Supplier User', CAST(4567890123 AS Numeric(18, 0)), N'123 Supplier Ln')
GO
INSERT [dbo].[User] ([id], [roleid], [username], [password], [email], [fullName], [phone], [address]) VALUES (5, 1, N'vhuy16', N'$2a$11$ajvAp9yfOqulgNVbeFG22eZfL4WQrdgTz33LVn3XQv1qmidevlmSa', N'voquanghuy506@gmail.com', N'VO QUANG HUY', CAST(8712131241 AS Numeric(18, 0)), N'HCM')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([paymentId])
REFERENCES [dbo].[Payment] ([id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Product] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Product]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([roleid])
REFERENCES [dbo].[Role] ([id])
GO
USE [master]
GO
ALTER DATABASE [MilkSalesManagement] SET  READ_WRITE 
GO
