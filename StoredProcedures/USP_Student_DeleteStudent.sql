USE [StudentDB]
GO
/****** Object:  StoredProcedure [dbo].[USP_Student_DeleteStudent]    Script Date: 10/22/2023 12:07:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pathum>
-- Create date: <10/21/2023>
-- Description:	<Delete Student>
-- =============================================
ALTER PROCEDURE [dbo].[USP_Student_DeleteStudent] 
@pInt_Flag INT,
@pInt_Id INT

AS
BEGIN
	--SET NOCOUNT ON;
	IF (@pInt_Flag = 0)
	BEGIN
		DELETE Students
		WHERE Id = @pInt_Id;
	END
END
