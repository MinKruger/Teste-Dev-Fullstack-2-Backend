-- Usa o banco de dados test_fullstack
USE test_fullstack;
GO

-- Verifica e recria a view vw_PedidosClientesVendedores
IF OBJECT_ID('dbo.vw_PedidosClientesVendedores', 'V') IS NOT NULL
BEGIN
    DROP VIEW dbo.vw_PedidosClientesVendedores;
END
GO

CREATE VIEW dbo.vw_PedidosClientesVendedores AS
SELECT 
    p.descricao_pedido AS DescricaoPedido,
    p.valor_total AS ValorTotal,
    p.data_criacao AS DataCriacao,
    p.observacao AS Observacao,
    p.autorizado AS Autorizado,
    c.nome_fantasia AS NomeFantasia,
    c.cnpj AS CNPJ,
    v.nome AS NomeVendedor
FROM dbo.Pedido AS p
INNER JOIN dbo.Cliente AS c ON p.cliente_id = c.cliente_id
INNER JOIN dbo.Vendedor AS v ON p.vendedor_id = v.vendedor_id;
GO
