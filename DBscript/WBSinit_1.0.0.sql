USE [WBS]
GO
/****** Object:  Table [dbo].[WBS]    Script Date: 09/25/2014 23:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WBS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Project] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](10) NULL,
	[Priority] [nvarchar](2) NULL,
	[Item] [nvarchar](50) NULL,
	[Describe] [nvarchar](100) NULL,
	[Todo] [nvarchar](100) NULL,
	[Owner] [nvarchar](10) NULL,
	[Status] [nvarchar](10) NULL,
	[StartDate] [nvarchar](30) NULL,
	[EndDate] [nvarchar](30) NULL,
	[PredictTime_hr] [int] NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_WBS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_WBS_UpdateTime]    Script Date: 09/25/2014 23:16:40 ******/
ALTER TABLE [dbo].[WBS] ADD  CONSTRAINT [DF_WBS_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
