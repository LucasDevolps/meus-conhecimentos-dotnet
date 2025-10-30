# 🚀 Manager

API REST desenvolvida em **ASP.NET Core 8**, estruturada com base em **Clean Architecture (DDD)**, utilizando **Entity Framework Core** e **SQL Server**.  
O objetivo deste projeto é demonstrar uma arquitetura limpa, modular e escalável para sistemas corporativos modernos.

---

## 🧱 Estrutura do Projeto
``` bash
Manager/
│
├── Manager.Domain/ # Entidades e contratos de domínio
│ ├── Entities/
│ └── Interfaces/
│
├── Manager.Application/ # Camada de aplicação (DTOs, Services)
│ ├── DTOs/
│ ├── Interfaces/
│ └── Services/
│
├── Manager.Infrastructure/ # Camada de infraestrutura (DbContext, Repositórios)
│ ├── Data/
│ ├── Repositories/
│ └── DependencyInjection.cs
│
└── Manager/ # Camada de apresentação (Controllers, Configurações)
├── Controllers/
├── appsettings.json
└── Program.cs

```

---

## ⚙️ Tecnologias Utilizadas

- 🟦 **.NET 8**
- 🧠 **Entity Framework Core**
- 🗄️ **SQL Server**
- 🧩 **Clean Architecture**
- 🧰 **Dependency Injection**
- 📜 **Swagger / OpenAPI**

---

## 🧠 Conceitos Aplicados

- Separação clara entre **Domain**, **Application**, **Infrastructure** e **WebAPI**
- Uso de **Repository Pattern**
- Implementação de **Injeção de Dependência (IoC)** centralizada
- Entidades desacopladas de qualquer tecnologia de persistência
- Uso de **DTOs** para transferência de dados entre camadas

---

## 💾 Configuração do Banco de Dados

Edite o arquivo `appsettings.json` em `Manager` com sua string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProjetoUsuarios;User Id=sa;Password=1234as;TrustServerCertificate=True;"
}

```

---

🧩 Criando o Banco e Aplicando Migrations

Execute os seguintes comandos no terminal, dentro da pasta raiz:

# Cria a migration inicial
dotnet ef migrations add InitialCreate --project Manager.Infrastructure --startup-project Manager

# Atualiza o banco
dotnet ef database update --project Manager.Infrastructure --startup-project Manager

🚀 Executando o Projeto

dotnet run --project Manager


A API estará disponível em:
👉 https://localhost:5001/swagger

🧩 Endpoints Disponíveis
Método	Endpoint	Descrição
GET	/api/usuarios	Lista todos os usuários
GET	/api/usuarios/{id}	Busca um usuário por ID
POST	/api/usuarios	Cadastra um novo usuário
PUT	/api/usuarios	Atualiza um usuário existente
DELETE	/api/usuarios/{id}	Remove um usuário
🧠 Próximos Passos (Sugestões de Evolução)

🔐 Implementar autenticação com JWT

🧾 Adicionar FluentValidation

🔄 Incluir AutoMapper para mapeamento entre entidades e DTOs

🧪 Criar testes unitários com xUnit / NUnit

🧰 Adicionar observabilidade (Serilog, Health Checks, etc.)

👨‍💻 Autor Lucas De Souza
💼 Desenvolvedor .NET | Apaixonado por arquitetura limpa e boas práticas
📧 lucasribeirodesouza2000@gmail.com

🐙 GitHub