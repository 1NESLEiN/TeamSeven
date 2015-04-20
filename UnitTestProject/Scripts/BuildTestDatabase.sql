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
DROP TABLE dbo.Statuses
DROP TABLE dbo.UserAccesses
DROP TABLE dbo.Positions
GO
CREATE TABLE [dbo].[JobDocumentations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Headline] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[Supporter] [int] NOT NULL,
	[DateCompleted] [datetime] NULL,
	[TimeSpent] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
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
	[Pass] [varchar](max) NOT NULL,
	[UserAccess] [int] NOT NULL,
	[Position] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 22-03-2015 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Statuses](
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


/****** Object:  Table [dbo].[Positions]    Script Date: 22-03-2015 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Positions](
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


/****** Object:  Table [dbo].[UserAccesses]    Script Date: 22-03-2015 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAccesses](
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
ALTER TABLE [dbo].[JobDocumentations]  WITH CHECK ADD  CONSTRAINT [FK_JobDocumentations_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[JobDocumentations] CHECK CONSTRAINT [FK_JobDocumentations_Statuses]
GO
ALTER TABLE [dbo].[Supporters]  WITH CHECK ADD  CONSTRAINT [FK_Supporters_UserAccesses] FOREIGN KEY([UserAccess])
REFERENCES [dbo].[UserAccesses] ([ID])
GO
ALTER TABLE [dbo].[Supporters] CHECK CONSTRAINT [FK_Supporters_UserAccesses]
GO
ALTER TABLE [dbo].[Supporters]  WITH CHECK ADD  CONSTRAINT [FK_Supporters_Positions] FOREIGN KEY([Position])
REFERENCES [dbo].[Positions] ([ID])
GO
ALTER TABLE [dbo].[Supporters] CHECK CONSTRAINT [FK_Supporters_Positions]
GO
INSERT INTO dbo.Types (Name) VALUES ('Hardware')
INSERT INTO dbo.Types (Name) VALUES ('Software')
INSERT INTO dbo.Types (Name) VALUES ('Other')
GO
INSERT INTO dbo.UserAccesses (Name) VALUES ('Admin')
INSERT INTO dbo.UserAccesses (Name) VALUES ('Supporter')
GO
INSERT INTO dbo.Positions (Name) VALUES ('Working')
INSERT INTO dbo.Positions (Name) VALUES ('Resigned')
GO
INSERT INTO dbo.Statuses (Name) VALUES ('Ny Opgave')
INSERT INTO dbo.Statuses (Name) VALUES ('Delvis færdig')
INSERT INTO dbo.Statuses (Name) VALUES ('Færdig')
GO
INSERT INTO dbo.Supporters (Name, Initials, Pass, UserAccess, Position) VALUES ('Martin Kiersgaard', 'MJO', 'Martin', 1, 1)
INSERT INTO dbo.Supporters (Name, Initials, Pass, UserAccess, Position) VALUES ('Thor Pedersen', 'TP', 'Thor', 2, 1)
INSERT INTO dbo.Supporters (Name, Initials, Pass, UserAccess, Position) VALUES ('Hans Christian', 'HC', 'Hans', 2, 1)
INSERT INTO dbo.Supporters (Name, Initials, Pass, UserAccess, Position) VALUES ('Peter Nielsen', 'PN', 'Peter', 2, 1)
INSERT INTO dbo.Supporters (Name, Initials, Pass, UserAccess, Position) VALUES ('Former Worker', 'FW', 'Former', 2, 2)