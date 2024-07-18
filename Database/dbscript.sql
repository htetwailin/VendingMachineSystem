USE [codetest_db]
GO
/****** Object:  Table [dbo].[product]    Script Date: 7/18/2024 5:35:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [decimal](18, 6) NOT NULL,
	[qty_abl] [int] NOT NULL,
	[is_deleted] [bit] NULL,
	[created_user_id] [nvarchar](50) NULL,
	[create_date_time] [datetime] NULL,
	[updated_user_id] [nvarchar](50) NULL,
	[updated_date_time] [datetime] NULL,
	[deleted_user_id] [nvarchar](50) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase]    Script Date: 7/18/2024 5:35:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[purchase_no] [varchar](20) NOT NULL,
	[total] [decimal](18, 6) NOT NULL,
	[puchanse_date] [datetime] NOT NULL,
 CONSTRAINT [PK_purchase] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchasedetail]    Script Date: 7/18/2024 5:35:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchasedetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[purchase_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[qty] [int] NOT NULL,
	[price] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_purchase_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sale]    Script Date: 7/18/2024 5:35:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sale](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[total] [decimal](18, 6) NOT NULL,
	[voucherno] [nvarchar](50) NOT NULL,
	[saledate] [datetime] NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[phoneno] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_sale] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[saledetail]    Script Date: 7/18/2024 5:35:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[saledetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[saleid] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[qty] [int] NOT NULL,
	[price] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_saledetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 7/18/2024 5:35:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[roleid] [int] NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[phoneno] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([id], [name], [price], [qty_abl], [is_deleted], [created_user_id], [create_date_time], [updated_user_id], [updated_date_time], [deleted_user_id]) VALUES (1, N'Pepsi', CAST(3.990000 AS Decimal(18, 6)), 90, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[product] ([id], [name], [price], [qty_abl], [is_deleted], [created_user_id], [create_date_time], [updated_user_id], [updated_date_time], [deleted_user_id]) VALUES (2, N'Water', CAST(1.650000 AS Decimal(18, 6)), 0, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[product] ([id], [name], [price], [qty_abl], [is_deleted], [created_user_id], [create_date_time], [updated_user_id], [updated_date_time], [deleted_user_id]) VALUES (3, N'Coke', CAST(5.690000 AS Decimal(18, 6)), 0, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[purchase] ON 

INSERT [dbo].[purchase] ([id], [purchase_no], [total], [puchanse_date]) VALUES (1, N'PO-24071821f7b', CAST(39.900000 AS Decimal(18, 6)), CAST(N'2024-07-18T15:54:46.400' AS DateTime))
INSERT [dbo].[purchase] ([id], [purchase_no], [total], [puchanse_date]) VALUES (2, N'PO-240718af97c', CAST(399.000000 AS Decimal(18, 6)), CAST(N'2024-07-18T16:46:58.697' AS DateTime))
SET IDENTITY_INSERT [dbo].[purchase] OFF
GO
SET IDENTITY_INSERT [dbo].[purchasedetail] ON 

INSERT [dbo].[purchasedetail] ([id], [purchase_id], [product_id], [qty], [price]) VALUES (1, 1, 1, 10, CAST(3.990000 AS Decimal(18, 6)))
INSERT [dbo].[purchasedetail] ([id], [purchase_id], [product_id], [qty], [price]) VALUES (2, 2, 1, 100, CAST(3.990000 AS Decimal(18, 6)))
SET IDENTITY_INSERT [dbo].[purchasedetail] OFF
GO
SET IDENTITY_INSERT [dbo].[sale] ON 

INSERT [dbo].[sale] ([id], [userid], [total], [voucherno], [saledate], [address], [phoneno]) VALUES (1, 5, CAST(0.000000 AS Decimal(18, 6)), N'24071819d33', CAST(N'2024-07-18T16:40:09.387' AS DateTime), N'monywa', N'12345678')
INSERT [dbo].[sale] ([id], [userid], [total], [voucherno], [saledate], [address], [phoneno]) VALUES (2, 5, CAST(3.990000 AS Decimal(18, 6)), N'240718907fd', CAST(N'2024-07-18T16:41:40.370' AS DateTime), N'monywa', N'12345678')
INSERT [dbo].[sale] ([id], [userid], [total], [voucherno], [saledate], [address], [phoneno]) VALUES (3, 5, CAST(3.990000 AS Decimal(18, 6)), N'240718a067b', CAST(N'2024-07-18T16:47:27.287' AS DateTime), N'mmll', N'25894')
SET IDENTITY_INSERT [dbo].[sale] OFF
GO
SET IDENTITY_INSERT [dbo].[saledetail] ON 

INSERT [dbo].[saledetail] ([id], [saleid], [product_id], [qty], [price]) VALUES (1, 3, 1, 10, CAST(3.990000 AS Decimal(18, 6)))
SET IDENTITY_INSERT [dbo].[saledetail] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([id], [username], [roleid], [password], [phoneno], [address]) VALUES (4, N'Admin01', 1, N'YYRHD1rJDsA7ZlSD/AIKJg==', N'123456987', N'Katha')
INSERT [dbo].[user] ([id], [username], [roleid], [password], [phoneno], [address]) VALUES (5, N'Admin02', 2, N'YYRHD1rJDsA7ZlSD/AIKJg==', N'123456789 ', N'monywa')
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_Table_1_qty_abl]  DEFAULT ((0)) FOR [qty_abl]
GO
ALTER TABLE [dbo].[purchasedetail]  WITH CHECK ADD  CONSTRAINT [FK_purchase_detail_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[purchasedetail] CHECK CONSTRAINT [FK_purchase_detail_product]
GO
ALTER TABLE [dbo].[purchasedetail]  WITH CHECK ADD  CONSTRAINT [FK_purchase_detail_purchase] FOREIGN KEY([purchase_id])
REFERENCES [dbo].[purchase] ([id])
GO
ALTER TABLE [dbo].[purchasedetail] CHECK CONSTRAINT [FK_purchase_detail_purchase]
GO
ALTER TABLE [dbo].[sale]  WITH CHECK ADD  CONSTRAINT [FK_sale_user] FOREIGN KEY([userid])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[sale] CHECK CONSTRAINT [FK_sale_user]
GO
ALTER TABLE [dbo].[saledetail]  WITH CHECK ADD  CONSTRAINT [FK_saledetail_sale] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[saledetail] CHECK CONSTRAINT [FK_saledetail_sale]
GO
