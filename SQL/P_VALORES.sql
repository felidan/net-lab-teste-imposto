USE [master]
GO
CREATE PROCEDURE [dbo].[P_VALORES]
AS
SELECT [Cfop], 
		SUM(BaseIcms), 
		SUM(ValorIcms), 
		ISNULL(SUM(ValorIpi), 0) 
FROM [dbo].[NotaFiscalItem]
GROUP BY [Cfop]

RETURN 
GO