# Gerenciamento de Clientes, Pedidos e Vendedores

API para gerenciamento de clientes, vendedores e pedidos, utilizando .NET Core, SQL Server, e aplicando os conceitos de Clean Architecture.

---

## 🚀 **Descrição do Projeto**

Esta API fornece funcionalidades para gerenciar clientes, vendedores e pedidos, permitindo operações como criação, atualização, desativação e consulta. Segue princípios de **Domain-Driven Design (DDD)**, boas práticas de programação e utiliza padrões de projeto como o **Repository Pattern**.

---

## 🛠️ **Estrutura do Projeto**

O projeto é organizado em camadas seguindo os princípios do **DDD**:

- **Domain**: Contém as entidades, interfaces de repositórios e lógica de domínio.
- **Application**: Contém os DTOs, serviços e lógica de aplicação.
- **Infrastructure**: Contém os repositórios concretos, contexto do banco de dados e configurações.
- **API**: Camada de apresentação (controllers) que expõe as rotas da aplicação.

---

## 🔗 **Rotas da API**

### **Clientes**
| Método | Rota                          | Descrição                                                                                  |
|--------|-------------------------------|------------------------------------------------------------------------------------------|
| GET    | `/api/clientes`               | Obtém todos os clientes cadastrados.                                                     |
| GET    | `/api/clientes/{id}`          | Obtém os detalhes de um cliente específico pelo ID.                                      |
| POST   | `/api/clientes`               | Cria um cliente consumindo uma API externa para buscar dados pelo CNPJ.                  |
| PUT    | `/api/clientes/{id}`          | Atualiza os dados de um cliente específico pelo ID.                                      |
| DELETE | `/api/clientes/{id}`          | Desativa um cliente específico pelo ID.                                                  |
| GET    | `/api/clientes/ComprasNoPeriodo` | Retorna o valor total das compras realizadas em um período.                              |

### **Pedidos**
| Método | Rota                          | Descrição                                                                                  |
|--------|-------------------------------|------------------------------------------------------------------------------------------|
| GET    | `/api/pedidos`                | Obtém todos os pedidos cadastrados.                                                      |
| GET    | `/api/pedidos/{id}`           | Obtém os detalhes de um pedido específico pelo ID.                                       |
| POST   | `/api/pedidos`                | Cria um novo pedido (somente se vendedor e cliente estiverem ativos).                    |
| PUT    | `/api/pedidos/{id}`           | Atualiza os dados de um pedido específico pelo ID.                                       |
| DELETE | `/api/pedidos/{id}`           | Exclui um pedido específico pelo ID (somente se não autorizado).                         |

### **Vendedores**
| Método | Rota                           | Descrição                                                                               |
|--------|--------------------------------|---------------------------------------------------------------------------------------|
| GET    | `/api/vendedores`             | Obtém todos os vendedores cadastrados.                                                |
| GET    | `/api/vendedores/{id}`        | Obtém os detalhes de um vendedor específico pelo ID.                                  |
| POST   | `/api/vendedores`             | Cria um novo vendedor.                                                                |
| PUT    | `/api/vendedores/{id}`        | Atualiza os dados de um vendedor específico pelo ID.                                  |
| DELETE | `/api/vendedores/{id}`        | Desativa um vendedor específico pelo ID.                                              |
| GET    | `/api/vendedores/VendasNoPeriodo` | Retorna o valor total das vendas realizadas em um período.                            |
| GET    | `/api/vendedores/MelhorCliente` | Retorna o cliente que mais comprou (valor total de pedidos).                          |

### **Seeder**
| Método | Rota                          | Descrição                                                                                  |
|--------|-------------------------------|------------------------------------------------------------------------------------------|
| POST   | `/api/seeder/PopularBanco`    | Popula o banco de dados com dados mockados para clientes, vendedores e pedidos.          |

---

## 🧰 **Padrões de Projeto e Boas Práticas**

O projeto utiliza os seguintes padrões e práticas:

- **Domain-Driven Design (DDD)**:
  - Camadas separadas para domínio, aplicação, infraestrutura e apresentação.
- **Dependency Injection**:
  - Gerenciada por meio do contêiner de serviços do ASP.NET Core.
- **Repository Pattern**:
  - Interfaces para abstrair a lógica de acesso ao banco de dados.
- **DTOs e AutoMapper**:
  - Separação entre as entidades de domínio e os objetos transferidos via API.
- **Validações**:
  - Implementadas com FluentValidation para garantir consistência nos dados de entrada.
- **Swagger**:
  - Documentação completa da API gerada automaticamente.

---

## 🗄️ **Configuração do Banco de Dados**

### Requisitos
- SQL Server instalado localmente ou disponível em uma instância remota.
- Ferramenta para executar scripts SQL, como **SSMS** ou o terminal do **SQL Server**.

### Configuração Rápida
1. **Execução dos Scripts de Tabelas**:
   - A criação de um novo database, das tabelas, da procedure e da view já estão totalmente configuradas dentro dos arquivos, sendo apenas necessário fazer a execução dos mesmos.
   - Execute os scripts de criação na ordem:
     - `Criacao tabelas.sql`
     - `Criacao procedure.sql`
     - `Criacao view.sql`

3. **Configurar a Connection String**:
   - Se estiver utilizando um banco local, não existe a necessidade de fazer a configuração de conexão, caso contrário, faça conforme abaixo.
   - Dentro do SQL Server Management Studio, conecte em qualquer banco e rode o script:
     ```sql
     SELECT 'Server=' + @@SERVERNAME + ';Database=test_fullstack;Trusted_Connection=True;TrustServerCertificate=True;' AS ConnectionString
     ```
   - Vá para o arquivo `appsettings.json`, e troque a `ConnectionString` pela gerada acima:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server={servidor que esteja alocado o banco};Database=test_fullstack;TrustServerCertificate=True;"
     }
     ```

---

## 💻 **Como Executar o Projeto com Docker**

### **Pré-requisitos**
- Docker instalado em sua máquina.

### **Passos**
1. Clone o repositório:  
   `git clone https://github.com/SEU-USUARIO/Teste-Dev-Fullstack-2-Backend.git`

2. Navegue até o diretório do projeto:  
   `cd Teste-Dev-Fullstack-2-Backend`

3. Construa a imagem Docker:  
   `docker build -t gerenciamento-backend .`

4. Execute o container Docker:  
   `docker run -d -p 5000:80 -p 5001:443 gerenciamento-backend`

### **Acessar a API**
- Após iniciar o container, acesse o Swagger UI para testar as rotas:
  - **HTTP**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
  - **HTTPS**: [https://localhost:5001/swagger](https://localhost:5001/swagger)

### **Banco de Dados com Docker**
Caso você queira subir o SQL Server com Docker, pode usar o comando abaixo:  
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest`

Depois, configure a connection string no `appsettings.json` para apontar para o container:  
`
"ConnectionStrings": {  
  "DefaultConnection": "Server=localhost,1433;Database=test_fullstack;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"  
}
`

### Acessar a API
- Assim que o projeto for compilado, será automaticamente redirecionado para o endpoint do swagger

---
