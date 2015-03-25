USE "C:\Users\Peter\Documents\Repositories\TeamSevenDevelopment\bin\Debug\Database.mdf"
/****** Object:  Table [dbo].[JobDocumentations]    Script Date: 22-03-2015 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
DROP TABLE dbo.JobDocumentations
DROP TABLE dbo.Types
DROP TABLE dbo.Supporters
GO
CREATE TABLE [dbo].[JobDocumentations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Headline] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[Supporter] [int] NOT NULL,
	[DateCompleted] [datetime] NOT NULL,
	[TimeSpent] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supporters]    Script Date: 22-03-2015 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supporters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Initials] [varchar](5) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Types]    Script Date: 22-03-2015 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[JobDocumentations] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[JobDocumentations]  WITH CHECK ADD  CONSTRAINT [FK_JobDocumentations_Supporters] FOREIGN KEY([Supporter])
REFERENCES [dbo].[Supporters] ([ID])
GO
ALTER TABLE [dbo].[JobDocumentations] CHECK CONSTRAINT [FK_JobDocumentations_Supporters]
GO
ALTER TABLE [dbo].[JobDocumentations]  WITH CHECK ADD  CONSTRAINT [FK_JobDocumentations_Types] FOREIGN KEY([Type])
REFERENCES [dbo].[Types] ([ID])
GO
ALTER TABLE [dbo].[JobDocumentations] CHECK CONSTRAINT [FK_JobDocumentations_Types]
GO
INSERT INTO dbo.Types (Name) VALUES ('Hardware')
INSERT INTO dbo.Types (Name) VALUES ('Software')
INSERT INTO dbo.Types (Name) VALUES ('Other')
GO
INSERT INTO dbo.Supporters (Name, Initials) VALUES ('Martin Kiersgaard', 'MJO')