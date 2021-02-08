
CREATE TABLE [CorporateQ&A].[dbo].Question(
Id INT PRIMARY KEY IDENTITY(1,1),
CategoryId INT NOT NULL,
AskedBy INT NOT NULL,
Title VARCHAR(80) NOT NULL,
Content NVARCHAR(MAX) NOT NULL,
AskedOn DATETIME DEFAULT SYSDATETIME(),
);

CREATE TABLE [CorporateQ&A].[dbo].Category(
Id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(80) NOT NULL,
Description VARCHAR(100) NOT NULL,
CreatedBy INT NOT NULL,
CreatedOn DATETIME DEFAULT SYSDATETIME(),
);

DROP TABLE Answer;
CREATE TABLE [CorporateQ&A].[dbo].Answer(
Id INT PRIMARY KEY IDENTITY(1,1),
AnsweredBy INT NOT NULL,
QuestionId INT NOT NULL,
Content NVARCHAR(MAX) NOT NULL,
IsBestSolution BIT DEFAULT 0,
AnsweredOn DATETIME DEFAULT SYSDATETIME(),
)

CREATE TABLE [CorporateQ&A].[dbo].QuestionActivity(
Id INT PRIMARY KEY IDENTITY(1,1),
UserId INT NOT NULL,
QuestionId INT NOT NULL,
ActivityType SMALLINT,
CreatedAt DATETIME DEFAULT SYSDATETIME(),
)

CREATE TABLE [CorporateQ&A].[dbo].AnswerActivity(
Id INT PRIMARY KEY IDENTITY(1,1),
UserId INT NOT NULL,
AnswerId INT NOT NULL,
ActivityType SMALLINT,
CreatedAt DATETIME DEFAULT SYSDATETIME(),
);

CREATE TABLE [CorporateQ&A].[dbo].Users(
Id INT PRIMARY KEY IDENTITY(1,1),
Name NVARCHAR(100) NOT NULL,
Email NVARCHAR(100) UNIQUE NOT NULL,
Location NVARCHAR(100) NOT NULL,
Position NVARCHAR(100) NOT NULL,
Department NVARCHAR(100) NOT NULL,
);
go

CREATE VIEW QuestionAnswerView AS
SELECT q.Id as 'QuestionId',q.AskedBy ,q.Title, q.CategoryId, q.Content, q.AskedOn, a.Id as 'AnswerId' , a.AnsweredBy, a.AnsweredOn FROM [CorporateQ&A].dbo.Question q LEFT JOIN [CorporateQ&A].dbo.Answer a ON q.Id = a.QuestionId
Go



CREATE VIEW QuestionCategoryView AS
(Select c.Id as 'CategoryId', c.Name, c.Description, q.Id as 'QuestionId', q.AskedOn  from
[CorporateQ&A].dbo.Category c left join
[CorporateQ&A].dbo.Question q on q.CategoryId = c.Id);
Go;



Create view CategoryDetails AS
Select qc.CategoryId as 'Id',qc.Name, qc.Description,
(SELECT COUNT(*) FROM QuestionCategoryView qcv WHERE qcv.AskedOn BETWEEN DATEADD(DAY,-7,GETDATE()) AND DATEADD(DAY,7,GETDATE()) AND qcv.CategoryId = qc.CategoryId) as 'ThisWeek',
(SELECT COUNT(*) FROM QuestionCategoryView qcv WHERE qcv.AskedOn >= DATEADD(MONTH,-1,GETDATE()) AND qcv.CategoryId = qc.CategoryId) as 'ThisMonth',
(SELECT COUNT(*) FROM QuestionCategoryView qcv WHERE qcv.CategoryId = qc.CategoryId AND qcv.QuestionId IS NOT NULL) as 'Total'
from QuestionCategoryView qc GROUP BY qc.CategoryId,qc.Name, qc.Description ;
go


DROP View QuestionDetails;
GO;
Create View QuestionDetails AS
SELECT qdat.QuestionId, qdat.CategoryId, u.Name as 'UserName', qdat.Title as 'QuestionTitle', qdat.Content, qdat.AskedBy, qdat.AskedOn, qdat.LikeCount, qdat.ViewCount, qdat.Resolved, qdat.AnswerCount  FROM 
(SELECT q.Id as 'QuestionId', q.Title, q.Content, q.CategoryId,q.AskedOn , q.AskedBy, qac.AnswerCount ,
	(Select Count(*) FROM QuestionActivity qa Where q.Id = qa.QuestionId AND qa.ActivityType = 0) as 'LikeCount' ,
	(Select Count(*) FROM QuestionActivity qa Where q.Id = qa.QuestionId AND qa.ActivityType = 2) as 'ViewCount', 
	(Select Count(*) FROM QuestionActivity qa Where q.Id = qa.QuestionId AND qa.ActivityType = 3) as 'Resolved' 
FROM Question q
INNER JOIN
(SELECT q.Id , COUNT(a.Id) as 'AnswerCount' FROM Question q LEFT JOIN Answer a ON q.Id =a.QuestionId GROUP By q.Id) as qac
On q.Id = qac.Id) as qdat 
INNER JOIN Users u On u.Id = qdat.AskedBy;
go;

DROP VIEW AnswerDetails;
GO;

Create view AnswerDetails As
SELECT adatq.AnswerId, adatq.LikeCount, adatq.DislikeCount, adatq.Content, adatq.AnsweredBy, adatq.AnsweredOn, adatq.IsBestSolution , adatq.QuestionId ,u.Name as 'AnsweredByName', q.AskedBy  FROM (
SELECT adat.AnswerId, adat.LikeCount, adat.DislikeCount, a.Content, a.AnsweredBy, a.AnsweredOn, a.IsBestSolution , a.QuestionId FROM
	(SELECT a.Id as 'AnswerId',
	(SELECT COUNT(*) FROM [CorporateQ&A].dbo.AnswerActivity aa1 WHERE aa1.AnswerId = a.Id AND aa1.ActivityType = 0) as 'LikeCount',
	(SELECT COUNT(*) FROM [CorporateQ&A].dbo.AnswerActivity aa1 WHERE aa1.AnswerId = a.Id AND aa1.ActivityType = 1) as 'DislikeCount'
	FROM [CorporateQ&A].dbo.Answer a GROUP BY a.Id) as adat
INNER JOIN
[CorporateQ&A].dbo.Answer a On a.Id = adat.AnswerId
) as adatq
INNER JOIN [CorporateQ&A].dbo.Users u ON u.Id = adatq.AnsweredBy
INNER JOIN [CorporateQ&A].dbo.Question q On q.Id = adatq.QuestionId;
Go;

DROP PROCEDURE GetAnswers;
GO;

CREATE Procedure GetAnswers(@questionId as INT, @userId as INT) AS BEGIN
Select * , 
(SELECT Count(*) FROM [CorporateQ&A].dbo.AnswerActivity aa WHERE aa.ActivityType = 0 AND aa.UserId = @userId AND aa.AnswerId = ad.AnswerId) as 'LikedByUser' ,
(SELECT Count(*) FROM [CorporateQ&A].dbo.AnswerActivity aa WHERE aa.ActivityType = 1 AND aa.UserId = @userId AND aa.AnswerId = ad.AnswerId) as 'DisLikedByUser' 
from [CorporateQ&A].dbo.AnswerDetails ad WHERE ad.QuestionId = @questionId; 
END;
Go;

DROP VIEW UserDetails;
GO;
CREATE VIEW UserDetails AS
SELECT u.Id, u.Name, u.Department, u.Location, u.Position, u.Email,
(SELECT SUM(ad.LikeCount) FROM AnswerDetails ad WHERE ad.AnsweredBy = u.Id GROUP BY ad.AnsweredBy) as 'TotalLikes',
(SELECT SUM(ad.DislikeCount) FROM AnswerDetails ad WHERE ad.AnsweredBy = u.Id GROUP BY ad.AnsweredBy) as 'TotalDislikes',
(SELECT COUNT(*) FROM Question q WHERE q.AskedBy  = u.Id) as 'QuestionAsked',
(SELECT COUNT(*) FROM Question q WHERE EXISTS (SELECT * FROM Answer a WHERE a.AnsweredBy = u.Id AND a.QuestionId = q.Id)) as 'QuestionAnswered',
(SELECT COUNT(*) FROM QuestionActivity qa WHERE qa.UserId = u.Id AND qa.ActivityType = 3) as 'QuestionResolved'
from Users u;
GO;

DROP PROCEDURE FilterByKeywordAndShowType;
GO;
CREATE PROCEDURE FilterByKeywordAndShowType(@keyword as NVARCHAR(MAX), @show AS SMALLINT, @userId AS INT)
AS BEGIN
DECLARE @results TABLE(
	QuestionId INT,
	CategoryId INT,
	UserName NVARCHAR(MAX),
	QuestionTitle NVARCHAR(MAX),
	Content NVARCHAR(MAX),
	AskedBy INT,
	AskedOn DATETIME,
	LikeCount INT,
	ViewCount INT,
	Resolved INT,
	AnswerCount INT
)
DECLARE @query as NVARCHAR(MAX);
SET @query = CONCAT('%',LOWER(@keyword),'%')

IF @show = 1 
	INSERT INTO @results 
	SELECT * FROM QuestionDetails q WHERE LOWER(q.QuestionTitle) LIKE @query AND q.AskedBy = @userId;
ELSE IF @show = 2
	INSERT INTO @results 
	SELECT * FROM QuestionDetails q WHERE LOWER(q.QuestionTitle) LIKE @query AND EXISTS(SELECT 1 FROM Answer a WHERE a.QuestionId = q.QuestionId AND a.AnsweredBy = @userId);
ELSE IF @show = 3
	INSERT INTO @results 
	SELECT * FROM QuestionDetails q WHERE LOWER(q.QuestionTitle) LIKE @query AND q.Resolved > 0;
ELSE IF @show = 4
	INSERT INTO @results 
	SELECT * FROM QuestionDetails q WHERE LOWER(q.QuestionTitle) LIKE @query AND q.Resolved = 0;
ELSE IF @show = 5
	INSERT INTO @results 
	SELECT * FROM QuestionDetails q WHERE LOWER(q.QuestionTitle) LIKE @query ORDER BY q.AskedOn DESC;
ELSE
	INSERT INTO @results 
	SELECT * FROM QuestionDetails q WHERE LOWER(q.QuestionTitle) LIKE @query;

SELECT * FROM @results;
END;
GO;

DROP PROCEDURE SearchQuestion;
GO;

CREATE PROCEDURE SearchQuestion(@keyword as NVARCHAR(MAX), @show AS SMALLINT, @userId AS INT,@categoryId as INT, @sortBy as SMALLINT)
AS BEGIN
DECLARE @keywordFilterResult TABLE(
	QuestionId INT,
	CategoryId INT,
	UserName NVARCHAR(MAX),
	QuestionTitle NVARCHAR(MAX),
	Content NVARCHAR(MAX),
	AskedBy INT,
	AskedOn DATETIME,
	LikeCount INT,
	ViewCount INT,
	Resolved INT,
	AnswerCount INT
)

DECLARE @sortByResult TABLE(
	QuestionId INT,
	CategoryId INT,
	UserName NVARCHAR(MAX),
	QuestionTitle NVARCHAR(MAX),
	Content NVARCHAR(MAX),
	AskedBy INT,
	AskedOn DATETIME,
	LikeCount INT,
	ViewCount INT,
	Resolved INT,
	AnswerCount INT
);
INSERT INTO @keywordFilterResult
EXEC FilterByKeywordAndShowType @keyword, @show, @userId

IF(@sortBy = 2)
	INSERT INTO @sortByResult 
	SELECT * FROM @keywordFilterResult as kf WHERE kf.AskedOn >= DATEADD(DAY,-10,GETDATE());
ELSE IF (@sortBy = 3)
	INSERT INTO @sortByResult 
	SELECT * FROM @keywordFilterResult as kf WHERE kf.AskedOn >= DATEADD(DAY,-10,GETDATE());
ELSE IF (@sortBy = 1)
	INSERT INTO @sortByResult 
	SELECT * FROM @keywordFilterResult as kf WHERE kf.AskedOn >= DATEADD(DAY,-7,GETDATE());
ELSE
	INSERT INTO @sortByResult
	SELECT * FROM @keywordFilterResult;

IF @categoryId = 0
	SELECT * FROM @sortByResult
ELSE
	SELECT * FROM @sortByResult ss WHERE ss.CategoryId = @categoryId;
END
GO;