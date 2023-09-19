USE [PointOfSaleDB]
GO

/****** Object:  StoredProcedure [dbo].[updateuser]    Script Date: 9/19/2023 10:30:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateuser]
@userid int,
@firstname varchar(20)=null,
@lastname varchar(20)=null,
@phoneno varchar(15)=null,
@pword varchar(100)=null,
@updateby int
AS
BEGIN
Update Users 
SET firstName=ISNULL(@firstname,firstName),lastName=ISNULL(@lastname,lastName),phoneNo=ISNULL(@phoneno,phoneNo),password=ISNULL(@pword,password),updatedby=@updateby,updatetime=CURRENT_TIMESTAMP
Where userId=@userid
END
GO


