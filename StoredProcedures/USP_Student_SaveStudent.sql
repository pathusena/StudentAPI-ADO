SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pathum>
-- Create date: <10/16/2023>
-- Description:	<Save Student>
-- =============================================
ALTER PROCEDURE [dbo].[USP_Student_SaveStudent] 
@pInt_Flag INT,
@pStr_FirstName VARCHAR(20),
@pStr_LastName VARCHAR(20),
@pStr_Email VARCHAR(20),
@pInt_Age INT

AS
BEGIN
	SET NOCOUNT ON;
	IF (@pInt_Flag = 0)
	BEGIN
		DECLARE @pInt_Id INT;
		SET @pInt_Id = NEXT VALUE FOR Student_Id_Sequence;
		INSERT INTO Students (Id, FirstName, LastName, Email, Age)
		VALUES (@pInt_Id, @pStr_FirstName, @pStr_LastName, @pStr_Email, @pInt_Age);

		SELECT @pInt_Id;
	END
END