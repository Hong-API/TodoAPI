USE [HDB]
GO

/****** Object:  Table [dbo].[Todos]    Script Date: 1/15/2024 12:15:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

