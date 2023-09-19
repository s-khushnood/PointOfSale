USE [PointOfSaleDB]
GO

/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 9/19/2023 10:30:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserLogin]
@email varchar(30),
@pword varchar(100)
AS
BEGIN
SELECT * FROM Users WHERE userMail=@email AND password=@pword
END
GO


