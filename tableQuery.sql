USE [EDMS]
GO
/****** Object:  Table [dbo].[GroupPermission]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AddPermission] [bit] NOT NULL,
	[EditPermission] [bit] NOT NULL,
	[DeletePermission] [bit] NOT NULL,
	[Preview] [bit] NOT NULL,
	[GroupID] [int] NULL,
	[ModuleID] [int] NULL,
 CONSTRAINT [PK_GroupPermission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCompany]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompany](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[ContactPerson] [varchar](100) NULL,
	[Phone] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[TotalUsers] [int] NULL,
	[StartDateAD] [datetime] NULL,
	[EndDateAD] [datetime] NULL,
	[IsBranchApplicable] [bit] NULL,
	[NoOfBranch] [int] NULL,
	[GroupID] [int] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblGroup]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) NOT NULL,
	[GroupCreateDate] [datetime] NOT NULL,
	[GroupCode] [varchar](20) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[GroupID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserTypeID] [int] NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser_CompanyBranch]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser_CompanyBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ConpanyId] [int] NOT NULL,
	[BranchCode] [varchar](10) NOT NULL,
 CONSTRAINT [PK_tblUser_CompanyBranch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblCompany] ON 

INSERT [dbo].[tblCompany] ([Id], [CompanyName], [Address], [City], [Country], [Email], [ContactPerson], [Phone], [IsActive], [TotalUsers], [StartDateAD], [EndDateAD], [IsBranchApplicable], [NoOfBranch], [GroupID]) VALUES (2, N'asas', N'dasdas', N'sdas', N'sadas', N'raaa@gmail.com', N'asda', N'222', 1, 2, CAST(N'2021-01-15T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime), 1, 2, 4)
INSERT [dbo].[tblCompany] ([Id], [CompanyName], [Address], [City], [Country], [Email], [ContactPerson], [Phone], [IsActive], [TotalUsers], [StartDateAD], [EndDateAD], [IsBranchApplicable], [NoOfBranch], [GroupID]) VALUES (4, N'asdas', N'adas', N'adsasd', N'adasd', N'asdasd', N'asdasda', N'adasd', 1, 2, CAST(N'2021-01-01T18:15:00.000' AS DateTime), CAST(N'2021-01-21T18:15:00.000' AS DateTime), 1, 2, 4)
SET IDENTITY_INSERT [dbo].[tblCompany] OFF
SET IDENTITY_INSERT [dbo].[tblGroup] ON 

INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (2, N'Munu Software PVT.LTD', CAST(N'2021-12-28T00:00:00.000' AS DateTime), N'MPRY', 0)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (4, N'rahul rateeee', CAST(N'2020-12-29T22:45:57.510' AS DateTime), N'demo', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (5, N'Hitech', CAST(N'2020-12-29T23:00:17.190' AS DateTime), N'1530', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (6, N'Hitech', CAST(N'2020-12-29T23:00:56.827' AS DateTime), N'1530', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (7, N'asas', CAST(N'2020-12-29T23:03:55.183' AS DateTime), N'7894', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (8, N'asadefef', CAST(N'2020-12-29T23:05:36.167' AS DateTime), N'dsfs', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (9, N'HIte', CAST(N'2020-12-30T19:14:17.190' AS DateTime), N'1111', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (10, N'aa', CAST(N'2021-01-02T14:56:45.250' AS DateTime), N'h001', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (11, N'test group', CAST(N'2021-01-02T19:34:34.437' AS DateTime), N'k001', 1)
INSERT [dbo].[tblGroup] ([ID], [GroupName], [GroupCreateDate], [GroupCode], [Status]) VALUES (13, N'Super Group', CAST(N'2021-01-03T00:00:00.000' AS DateTime), N'SRAD', 1)
SET IDENTITY_INSERT [dbo].[tblGroup] OFF
SET IDENTITY_INSERT [dbo].[tblUser] ON 

INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (2, N'Rahul', N'123', 2, N'Rahul', N'Kumar', N'Yadav', NULL)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (3, N'rahul1', N'123', 2, N'rahul', N'kumar ', N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (4, N'roshan1', N'123', 2, N'roshan', N'kumar', N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (5, N'rabi11', N'Wj7VoVOy/+7ytLNAvim/Yw==', 2, N'Rabi', NULL, N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (6, N'sonu1', N'svkLRj9nYEgZo7gWDJD5IQ==', 4, N'adit', N'kumar', N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (7, N'sunil', N'Wj7VoVOy/+7ytLNAvim/Yw==', 4, N'Sunil', N'kumar ', N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (8, N'test', N'svkLRj9nYEgZo7gWDJD5IQ==', 10, N'test', N'test', N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (9, N'test1', N'svkLRj9nYEgZo7gWDJD5IQ==', 11, N'test1', N'test', N'yadav', 1)
INSERT [dbo].[tblUser] ([Id], [UserName], [Password], [GroupID], [FirstName], [MiddleName], [LastName], [UserTypeID]) VALUES (13, N'SUPERADMIN', N'svkLRj9nYEgZo7gWDJD5IQ==', 13, N'SUPERADMIN', N'SUPERADMIN', N'SUPERADMIN', 0)
SET IDENTITY_INSERT [dbo].[tblUser] OFF
ALTER TABLE [dbo].[GroupPermission]  WITH CHECK ADD  CONSTRAINT [FK_GroupPermission_GroupPermission] FOREIGN KEY([GroupID])
REFERENCES [dbo].[tblGroup] ([ID])
GO
ALTER TABLE [dbo].[GroupPermission] CHECK CONSTRAINT [FK_GroupPermission_GroupPermission]
GO
ALTER TABLE [dbo].[tblCompany]  WITH CHECK ADD  CONSTRAINT [FK_tblCompany_tblGroup] FOREIGN KEY([GroupID])
REFERENCES [dbo].[tblGroup] ([ID])
GO
ALTER TABLE [dbo].[tblCompany] CHECK CONSTRAINT [FK_tblCompany_tblGroup]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[tblGroup] ([ID])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_Group]
GO
ALTER TABLE [dbo].[tblUser_CompanyBranch]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_CompanyBranch_tblCompany] FOREIGN KEY([ConpanyId])
REFERENCES [dbo].[tblCompany] ([Id])
GO
ALTER TABLE [dbo].[tblUser_CompanyBranch] CHECK CONSTRAINT [FK_tblUser_CompanyBranch_tblCompany]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCompanyList]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetCompanyList]
AS
 select Id,CompanyName,[Address],ISNULL(City,'')City,ISNULL(Country,'')Country,ISNULL(ContactPerson,'')ContactPerson,
 ISNULL(Phone,'')Phone,ISNULL(Email,'')Email,IsActive,TotalUsers,StartDateAD,EndDateAD,IsBranchApplicable,NoOfBranch,GroupID from tblCompany
GO;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetGroupList]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetGroupList]
AS
SELECT ID,GroupName,GroupCode,GroupCreateDate,ISNULL( [status],0)status FROM tblGroup
GO;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserList]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserList]
AS
 select Id,UserName,[Password],[GroupID],firstName,MiddleName,LastName,ISNULL(userTypeID,'')userTypeID from tblUser
GO;
GO
/****** Object:  StoredProcedure [dbo].[sp_loginIn]    Script Date: 03/01/2021 9:53:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_loginIn] 
(
 @userId varchar(50),
 @CompanyCode  varchar(4),
 @password  varchar(max)
)
as 
select tu.UserName,tu.GroupID,tu.FirstName,ISNULL(tu.MiddleName,'')MiddleName,tu.LastName,ISNULL(tu.userTypeId,'')userTypeId,g.GroupName,g.GroupCreateDate 
,g.GroupCode from  tblUser tu inner join [Group] g on tu.GroupID=g.ID where tu.UserName=@userId and g.GroupCode=@CompanyCode
and tu.Password=@password

GO
