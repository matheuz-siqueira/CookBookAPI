# CookbookAPI

<details open="open">
    <summary>Conteúdo</summary>
    <ol>
        <li>
            <a href="#sobre-o-projeto">Sobre o projeto</a>
            <ul>
                <li><a href="#built-with">Built With</a>
                <li><a href="#features">Feautures</a>
            </ul>
        </li>
        <li>
            <a href="#getting-started">Getting Started</a>
            <ul>
                <li><a href="#requisitos">Requisitos</a>
                <li><a href="#instalação">Instalação</a>
            </ul>
        </li>
        <li><a href="#licença">Licença</a>
        <li><a href="#contato">Contate-me</a>
    </ol>
</details>

### **Sobre o projeto**

Este é um projeto de uma API ASP NET desenvolvida utilizando .NET CORE 7 e uma abordagem de desenvolvimento em camadas.

Esse projeto foi desenvolvido com o intuito de consolidar conceitos essencias do desenvolvimento de aplicações ASP NET CORE utilizando .NET, tendo por base um curso como guia mas adotando estratégias de implementação diferentes.

Essa API busca permitir que usuários consigam registrar suas receitas favoritas e também compartilhar com outras pessoas.

Desse modo a API fornece endpoints para **cadastro** do usuário e **autenticação** utilizando **token JWT**

fornece também endpoinsts para que você consiga **registrar receitas** (inserindo título, tempo de preparo, ingredientes, etc.) **atualização e deleção** além de consultas por meio de **filtros** ou id da receita.

API também fornece uma forma de se conectar a outros usuários utilizando **websocket**. Desse modo um usuário pode gerar um **QRCode** e enviar para outro usuário para que possa se conectar.

Quando um usuário possui uma conexão com outro usuário as receitas podem ser compartilhadas (ReadOnly) entre estes.

#### **Built With**

![ubuntu-shield]
![net-core]
![csharp-shield]
![swagger-shield]
![postman-shield]
![mysql-shield]
![vscode-shield]
![git-shield]

#### features

- Registrar usuário;
- Registrar receita;
- Atualizar uma receita;
- Consultar receitas por filtros;
- Gerar QRCode;
- Adicionar uma conexão com outro usuário
- Remover conexão;

E outras.

### Getting Started

#### Requisitos

- Visual Studio Code

- MySql

#### Instalação

1. Clone o repositório
   ```sh
   git clone https://github.com/matheuz-siqueira/cookbook-api.git
   ```
2. Preencher as informações no arquivo `appsettings.Development.json`

3. Execute a API

   ```sh
   dotnet watch run
   ```

   ou

   ```sh
   dotnet run
   ```

4. Ótimos testes :)

### Licença

Fique a vontade para estudar e aprender com este projeto. Você não tem permissão para utiliza-lo para distribuição ou comercialização.

### Contato

[![linkedin][linkedin-shield]][linkedin-url]
<a href="mailto:matheussiqueira.work@gmail.com" target="_blank">
<img src="https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white"></a>

<!-- Badges -->

[ubuntu-shield]: https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white
[swagger-shield]: https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white
[net-core]: https://img.shields.io/badge/.NET_%20_Core_7.0-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[postman-shield]: https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white
[csharp-shield]: https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white
[mysql-shield]: https://img.shields.io/badge/mysql-%2300f.svg?style=for-the-badge&logo=mysql&logoColor=white
[vscode-shield]: https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white
[git-shield]: https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white
[linkedin-shield]: https://img.shields.io/badge/linkedin-%230077B5.svg?style=for-the-badge&logo=linkedin&logoColor=white
[gmail-shield]: https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white

 <!-- URLs -->

[linkedin-url]: https://www.linkedin.com/in/matheussiqueira-me/
