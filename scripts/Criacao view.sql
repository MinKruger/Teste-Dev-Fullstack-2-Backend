-- Criação da view vw_PedidosClientesVendedores no schema test_fullstack
CREATE VIEW test_fullstack.vw_PedidosClientesVendedores AS
SELECT 
    p.Descricao AS DescricaoPedido,
    p.ValorTotal,
    p.DataCriacao,
    p.Observacao,
    p.Autorizado,
    c.NomeFantasia,
    c.CNPJ,
    v.Nome AS NomeVendedor
FROM test_fullstack.Pedido AS p
INNER JOIN test_fullstack.Cliente AS c ON p.ClienteId = c.ClienteId
INNER JOIN test_fullstack.Vendedor AS v ON p.VendedorId = v.VendedorId;
