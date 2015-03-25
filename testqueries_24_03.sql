--SELECT JobDocumentations.ID, Headline, Description, DateCreated, DateCompleted, TimeSpent, Supporters.Name AS SupporterName, Initials, Types.Name AS TypeName FROM JobDocumentations JOIN Supporters ON Supporters.ID = JobDocumentations.Supporter JOIN dbo.Types ON dbo.Types.ID = JobDocumentations.Type WHERE Initials = 'MJO';
USE [C:\USERS\HANS\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\TEAMSEVEN\BIN\DEBUG\DATABASE.MDF];
SELECT * FROM dbo.JobDocumentations WHERE Headline LIKE '%U%';

SELECT * FROM JobDocumentations WHERE DateCreated BETWEEN '2015-03-03' AND '2015-03-25';