-- Criação da procedure BuscarVendasPorVendedor no schema test_fullstack
CREATE PROCEDURE test_fullstack.BuscarVendasPorVendedor
    @CodigoVendedor NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.Descricao AS DescricaoPedido,
        p.ValorTotal,
        p.DataCriacao,
        p.Observacao,
        p.Autorizado
    FROM test_fullstack.Pedido AS p
    INNER JOIN test_fullstack.Vendedor AS v ON p.VendedorId = v.VendedorId
    WHERE v.CodigoVendedor = @CodigoVendedor;
END;
