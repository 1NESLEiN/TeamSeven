--SELECT JobDocumentations.ID, Headline, Description, DateCreated, DateCompleted, TimeSpent, Supporters.Name AS SupporterName, Initials, Types.Name AS TypeName FROM JobDocumentations JOIN Supporters ON Supporters.ID = JobDocumentations.Supporter JOIN dbo.Types ON dbo.Types.ID = JobDocumentations.Type WHERE Initials = 'MJO';

--SELECT * FROM dbo.JobDocumentations WHERE DateCreated >= '2015-03-03' AND DateCompleted <= '2015-03-24';

SELECT * FROM JobDocumentations WHERE DateCreated BETWEEN '2015-03-24' AND '2015-03-03';