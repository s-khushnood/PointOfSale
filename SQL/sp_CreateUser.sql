USE [PointOfSaleDB]
GO

/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 9/19/2023 10:29:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateUser]
@firstname VARCHAR(30),
@lastname VARCHAR(30),
@cnic VARCHAR(20),
@phone VARCHAR(20),
@email VARCHAR(100),
@pword VARCHAR(150),
@isadmin BIT
AS
BEGIN
INSERT INTO Users(firstName, lastName, CNIC, phoneno, userMail, [password], isAdmin, isActive)
VALUES (@firstname, @lastname, @cnic, @phone, @email, @pword, @isadmin, 'True')
END
GO


