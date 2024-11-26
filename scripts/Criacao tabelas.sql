-- Criação do schema test_fullstack
CREATE SCHEMA test_fullstack;

-- Criação da tabela Vendedor no schema test_fullstack
CREATE TABLE test_fullstack.Vendedor (
    vendedor_id INT IDENTITY(1,1) PRIMARY KEY, -- Chave primária auto-incremental
    nome NVARCHAR(100) NOT NULL, -- Nome do vendedor
    codigo_vendedor NVARCHAR(50) NOT NULL UNIQUE, -- Código único do vendedor
    apelido NVARCHAR(50) NULL, -- Apelido do vendedor
    ativo BIT NOT NULL DEFAULT 1 -- Indica se o vendedor está ativo
);

-- Criação da tabela Cliente no schema test_fullstack
CREATE TABLE test_fullstack.Cliente (
    cliente_id INT IDENTITY(1,1) PRIMARY KEY, -- Chave primária auto-incremental
    razao_social NVARCHAR(150) NOT NULL, -- Razão social
    nome_fantasia NVARCHAR(100) NULL, -- Nome fantasia
    cnpj CHAR(14) NOT NULL UNIQUE, -- CNPJ (com 14 caracteres fixos)
    logradouro NVARCHAR(200) NOT NULL, -- Logradouro
    bairro NVARCHAR(100) NOT NULL, -- Bairro
    cidade NVARCHAR(100) NOT NULL, -- Cidade
    estado CHAR(2) NOT NULL, -- Estado (ex.: SP, RJ)
    ativo BIT NOT NULL DEFAULT 1 -- Indica se o cliente está ativo
);

-- Criação da tabela Pedido no schema test_fullstack
CREATE TABLE test_fullstack.Pedido (
    pedido_id INT IDENTITY(1,1) PRIMARY KEY, -- Chave primária auto-incremental
    descricao_pedido NVARCHAR(255) NOT NULL, -- Descrição do pedido
    valor_total DECIMAL(18,2) NOT NULL, -- Valor total do pedido
    data_criacao DATETIME NOT NULL DEFAULT GETDATE(), -- Data de criação do pedido
    observacao NVARCHAR(MAX) NULL, -- Observações do pedido
    autorizado BIT NOT NULL DEFAULT 0, -- Indica se o pedido foi autorizado
    cliente_id INT NOT NULL, -- FK para tabela Cliente
    vendedor_id INT NOT NULL, -- FK para tabela Vendedor
    CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (cliente_id) REFERENCES test_fullstack.Cliente(cliente_id), -- Chave estrangeira para Cliente
    CONSTRAINT FK_Pedido_Vendedor FOREIGN KEY (vendedor_id) REFERENCES test_fullstack.Vendedor(vendedor_id) -- Chave estrangeira para Vendedor
);

-- Índices adicionais para otimização no schema test_fullstack
CREATE INDEX IX_Cliente_CNPJ ON test_fullstack.Cliente(cnpj); -- Índice para busca rápida por CNPJ
CREATE INDEX IX_Pedido_ClienteId ON test_fullstack.Pedido(cliente_id); -- Índice para relacionamentos com Cliente
CREATE INDEX IX_Pedido_VendedorId ON test_fullstack.Pedido(vendedor_id); -- Índice para relacionamentos com Vendedor
