# DinFlow

DinFlow é uma aplicação web desenvolvida em ASP.NET MVC para gerenciamento de finanças pessoais, incluindo funcionalidades para rastrear despesas, receitas e economias. O objetivo do DinFlow é fornecer uma interface amigável e funcional para ajudar os usuários a gerenciar suas finanças de maneira eficiente.

## Funcionalidades

- Registro e visualização de despesas
- Registro e visualização de receitas
- Monitoramento de economias
- Modo escuro para melhor usabilidade em ambientes com pouca luz
- Interface responsiva para dispositivos móveis

## Tecnologias Utilizadas

- ASP.NET MVC
- Bootstrap
- jQuery
- HTML/CSS
- JavaScript

## Pré-requisitos

Antes de começar, verifique se você possui os seguintes itens instalados em seu sistema:

- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou outro banco de dados compatível)
- [Visual Studio](https://visualstudio.microsoft.com/vs/) (ou outra IDE de sua preferência)

## Instalação

Siga estas etapas para executar o projeto em sua máquina local:

1. **Clone o repositório**
2. **Abra no Visual Studio**
3. **Ferramentas -> Gerenciador de pacotes NuGet -> Console..**
4. **Digite no Console**
    ```bash
    enable-migrations
    add-migration final
    update-database
   