# 🚀 Manager

![Build](https://github.com/SEU_USUARIO/Manager/actions/workflows/build.yml/badge.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-active-success)

---

## 📌 Sobre o projeto

**Manager API** é uma aplicação desenvolvida em **.NET 8** voltada para gestão e integração modular de sistemas.  
O projeto segue os princípios de **Clean Architecture**, **DDD** e **boas práticas de DevOps**, com integração contínua via **GitHub Actions**.

A API é preparada para autenticação **JWT**, banco de dados **SQL Server**, e organização em camadas (`Domain`, `Infrastructure`, `Service`, `Api`).

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

## 🧩 Criando o Banco e Aplicando Migrations

Execute os seguintes comandos no terminal, dentro da pasta raiz:

# Cria a migration inicial
```bash
dotnet ef migrations add InitialCreate --project Manager.Infrastructure --startup-project Manager
```

# Atualiza o banco
```bash
dotnet ef database update --project Manager.Infrastructure --startup-project Manager
```

## 🚀 Executando o Projeto

```bash
dotnet run --project Manager
```

A API estará disponível em:
👉 https://localhost:5001/swagger

## 🧩 Endpoints Disponíveis

| Método | Endpoint             | Descrição                     |
| :----- | :------------------- | :---------------------------- |
| GET    | `/api/usuarios`      | Lista todos os usuários       |
| GET    | `/api/usuarios/{id}` | Busca um usuário por ID       |
| POST   | `/api/usuarios`      | Cadastra um novo usuário      |
| PUT    | `/api/usuarios`      | Atualiza um usuário existente |
| DELETE | `/api/usuarios/{id}` | Remove um usuário             |

---

## 🚀 Pipeline de Build (GitHub Actions)

O projeto conta com um **workflow automatizado** que garante qualidade e estabilidade a cada commit.

```yaml
name: Build & Test .NET API
```


👨‍💻 Autor Lucas De Souza

💼 Desenvolvedor .NET | Apaixonado por arquitetura limpa e boas práticas

📧 lucasribeirodesouza2000@gmail.com

🐙 GitHub
