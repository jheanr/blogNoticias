# Tech Challenge 2: Blog de Notícias com Autenticação e Implantação no Azure CI/CD

## Introdução

Este documento descreve o projeto de um blog de notícias com autenticação, baseado na estrutura Domain-Driven Design (DDD) e utilizando o Azure DevOps para Integração Contínua (CI) e Implantação Contínua (CD) com o Azure Container Instances (ACI) para a aplicação de servidor.

## Visão Geral do Projeto

O projeto consiste em uma aplicação web desenvolvido em C# com .NET 7 e Minimal API. A aplicação permite que os usuários se registrem, façam login, criem, editem e excluam notícias.

## Estrutura DDD

O projeto segue um padrão baseado na arquitetura Domain-Driven Design (DDD) para uma melhor organização e manutenção do código. A estrutura DDD divide o código em camadas com responsabilidades bem definidas, as camadas que estamos utilizando no projeto são as seguintes:

- **Camada de Domínio**: O coração do sistema, onde a lógica de negócios reside. Inclui entidades, agregados, serviços de domínio e objetos de valor.

- **Camada de Infraestrutura**: Gerencia aspectos técnicos, como acesso a banco de dados, serviços externos e comunicação com a camada de domínio. Nesta camada, utiliza-se o Entity Framework Core para interagir com o banco de dados SQL Server.

## Autenticação e Autorização

A autenticação é realizada utilizando JSON Web Tokens (JWT). Esse token é enviado nas requisições para os endpoints protegidos da API para garantir a autenticação e autorização.

## Implantação no Azure CI/CD

A implantação da aplicação é automatizada usando o Azure DevOps para CI/CD. O processo de CI envolve a construção, teste e empacotamento da aplicação em um artefato pronto para implantação. O CD envolve a implantação do artefato em um contêiner no Azure Container Instances (ACI).

- O pipeline de CI é definido no arquivo `azure-pipelines.yml`.
- As chaves de acesso necessárias para implantação no Azure são armazenadas como segredos no Azure DevOps.
- O Azure Container Registry (ACR) é usado para armazenar as imagens de contêiner.
- O Azure Container Instances (ACI) é usado para implantar os contêineres.

## Testando o projeto

Deixamos disponíveis dois usuários com _roles_ distintas para teste, sendo:
**Role**: Admin
- login: admin@admin.com
- senha: pass123

**Role**: User
- login: user@user.com
- senha: pass123

> Há possibilidade de criar outros usuários conforme necessidade através do endpoint disponibilizado.

Para a remoção de uma notícia, é obrigatório a _role_ de **admin** para executar a operação.

## Conclusão

Este projeto de blog de notícias com autenticação e implantação no Azure DevOps demonstra a integração da estrutura DDD, autenticação JWT e a automação de CI/CD usando o Azure DevOps.

> Implementado por: [Daniela Miranda de Almeida](https://github.com/danimiran), [Jhean Ricardo Ramos](https://github.com/jheanr), [Lucas dos anjos Varela](https://github.com/LucasVarela42), [Marcelo de Moraes Andrade](https://github.com/MM-Andrade) e [Wellington Chida de Oliveira](https://github.com/WellingtonChidaOliveira).
