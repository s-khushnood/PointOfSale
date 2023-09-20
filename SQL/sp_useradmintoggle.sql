USE [PointOfSaleDB]
GO

/****** Object:  StoredProcedure [dbo].[useradmintoggle]    Script Date: 9/20/2023 1:27:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[useradmintoggle]
@userid int,
@updateby int,
@return int OUTPUT
AS
BEGIN
IF (SELECT isAdmin FROM users where userId=@updateby)='true'
BEGIN
IF (SELECT isAdmin FROM users where userId=@userid)='true'
BEGIN
Update users
SET isAdmin='false',updatedby=@updateby,updatetime=CURRENT_TIMESTAMP Where userId=@userid
RETURN 0
END
ELSE
BEGIN
Update users
SET isAdmin='true',updatedby=@updateby,updatetime=CURRENT_TIMESTAMP Where userId=@userid
RETURN 1
END
END
ELSE 
BEGIN
RETURN -1
END
SELECT @return AS [Return]
END
GO


