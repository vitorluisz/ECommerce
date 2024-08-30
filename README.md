# Sobre o projeto

ECommerce é uma Aplicação Web desenvolvida em ASP.NET MVC, um site de compras, que inclui funcionalidades como autenticação de usuários, gerenciar os produtos, carrinho de compras, gerenciamento de produtos, filtro de produtos, e envio de emails.

![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_132.png)

## Tecnologias utilizadas
- C#
- HTML / CSS / JavaScript
- Entity Framework
- **Framework:** ASP.NET MVC
- **IDE:** Visual Studio
- **Banco de Dados:** SQL Server
- **Envio de Emails:** Mailgun
- **Autenticação de Login:** ASP.NET Core Identity

# Funcionalidades

## Carrinho de Compras
Adicione, remova e atualize a quantidade de itens no carrinho.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_134.png)

## Filtro
Navegue pelos produtos utilizando o filtro por categorias.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_136.png)
Ou pelo filtro de palavras.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_137.png)

## Painel de Administração
Gerencie produtos, categorias e pedidos (somente para administradores).
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_138.png)

## Finalização de Compra
Conclua a compra e receba um email de confirmação.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_140.png)
Email exemplo de confirmação.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_135.png)

## Autenticação de Usuário
Registre-se ou faça login.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_139.png)
Quando estiver logado, será visível.
![Layout 1](https://raw.githubusercontent.com/vitorluisz/ECommerce/master/ECommerce/wwwroot/img/Screenshot_141.png)

# Projeto
O projeto é feito na arquitetura MVC, que é a sigla para Models Views e Controllers, que são as pastas principais que se comunicam entre si, para o funcionamento da aplicação.
## Controllers
Pasta onde ficam os controladores, que gerenciam o fluxo e o manupulação dos dados.
## Models
Models é a pasta onde é definido os objetos que carregam os dados necessários para o projeto.
## Views
Onde ficam os arquivos .cshtml que serão as telas na aplicação, como o Menu, AddProduct, Basket, Login, Resgister, etc.
## Outros
- **Data:** Onde é feita a conexão com o servidor do Banco de Dados do SQL Server.
- **Migrations:** Criada pelo Entity Framework, onde foi feita a criação automática do Banco de Dados e das tabelas.
- **Properties:** Configurações de inicalização, criada automaticamente.
- **wwwroot:** Onde fica os arquivos css do projeto, os scripts, imagens, e as bibliotecas.
- **Program.cs** Classe main em que inicia o projeto, em que contém as confugurações básicas nessárias para a aplicação web, além da configuração do banco, e dos serviços.
- **ECommerce.csproj e appsettings.Development.json** Dados de configuração do ambiente, criado automaticamente pelo Visual Studio.

# Como executar o projeto
```bash
# No terminal ou prompt de comando, clone o repositório
git clone https://https://github.com/vitorluisz/ECommerce
```
Obs: não contém o arquivo appsettings.json, será necessário adicionar ele manualmente, e apontar para o seu servidor local. Também é necessário apontar as informações de configuração do MailGun.

# Autor

Vitor Luis Zampronha

https://www.linkedin.com/in/vitor-zampronha
