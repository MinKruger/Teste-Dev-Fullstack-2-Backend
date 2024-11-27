-- Verifica e cria o banco de dados test_fullstack
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'test_fullstack')
BEGIN
    CREATE DATABASE test_fullstack;
END
GO

-- Usa o banco de dados test_fullstack
USE test_fullstack;
GO

-- Validação e criação da tabela Vendedor
IF OBJECT_ID('dbo.Vendedor', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Vendedor;
END
GO

CREATE TABLE Vendedor (
    vendedor_id INT IDENTITY(1,1) PRIMARY KEY, -- Chave primária auto-incremental
    nome NVARCHAR(100) NOT NULL, -- Nome do vendedor
    codigo_vendedor NVARCHAR(50) NOT NULL UNIQUE, -- Código único do vendedor
    apelido NVARCHAR(50) NULL, -- Apelido do vendedor
    ativo BIT NOT NULL DEFAULT 1 -- Indica se o vendedor está ativo
);
GO

-- Validação e criação da tabela Cliente
IF OBJECT_ID('dbo.Cliente', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Cliente;
END
GO

CREATE TABLE Cliente (
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
GO

-- Validação e criação da tabela Pedido
IF OBJECT_ID('dbo.Pedido', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Pedido;
END
GO

CREATE TABLE Pedido (
    pedido_id INT IDENTITY(1,1) PRIMARY KEY, -- Chave primária auto-incremental
    descricao_pedido NVARCHAR(255) NOT NULL, -- Descrição do pedido
    valor_total DECIMAL(18,2) NOT NULL, -- Valor total do pedido
    data_criacao DATETIME NOT NULL DEFAULT GETDATE(), -- Data de criação do pedido
    observacao NVARCHAR(MAX) NULL, -- Observações do pedido
    autorizado BIT NOT NULL DEFAULT 0, -- Indica se o pedido foi autorizado
    cliente_id INT NOT NULL, -- FK para tabela Cliente
    vendedor_id INT NOT NULL, -- FK para tabela Vendedor
    CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (cliente_id) REFERENCES Cliente(cliente_id), -- Chave estrangeira para Cliente
    CONSTRAINT FK_Pedido_Vendedor FOREIGN KEY (vendedor_id) REFERENCES Vendedor(vendedor_id) -- Chave estrangeira para Vendedor
);
GO

-- Criação de índices adicionais para otimização
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cliente_CNPJ')
BEGIN
    CREATE INDEX IX_Cliente_CNPJ ON Cliente(cnpj); -- Índice para busca rápida por CNPJ
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pedido_ClienteId')
BEGIN
    CREATE INDEX IX_Pedido_ClienteId ON Pedido(cliente_id); -- Índice para relacionamentos com Cliente
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pedido_VendedorId')
BEGIN
    CREATE INDEX IX_Pedido_VendedorId ON Pedido(vendedor_id); -- Índice para relacionamentos com Vendedor
END
GO
