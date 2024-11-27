-- Usa o banco de dados test_fullstack
USE test_fullstack;
GO

-- Verifica e recria a procedure BuscarVendasPorVendedor
IF OBJECT_ID('dbo.BuscarVendasPorVendedor', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.BuscarVendasPorVendedor;
END
GO

CREATE PROCEDURE dbo.BuscarVendasPorVendedor
    @CodigoVendedor NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.descricao_pedido AS DescricaoPedido,
        p.valor_total AS ValorTotal,
        p.data_criacao AS DataCriacao,
        p.observacao AS Observacao,
        p.autorizado AS Autorizado
    FROM dbo.Pedido AS p
    INNER JOIN dbo.Vendedor AS v ON p.vendedor_id = v.vendedor_id
    WHERE v.codigo_vendedor = @CodigoVendedor;
END;
GO
