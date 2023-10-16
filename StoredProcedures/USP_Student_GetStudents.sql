SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pathum>
-- Create date: <10/15/2023>
-- Description:	<Get Students>
-- =============================================
ALTER PROCEDURE [dbo].[USP_Student_GetStudents] 
@pInt_Flag INT,
@pInt_Id INT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@pInt_Flag = 0)
	BEGIN
		SELECT *
		FROM Students;
	END
	ELSE IF (@pInt_Flag = 1)
	BEGIN
		SELECT *
		FROM Students
		WHERE Id = @pInt_Id;
	END
END
