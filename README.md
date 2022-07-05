# AzureKeyVaultPractice
Repositório para exemplificar como utilizar o azure key vault

Utilizando minha conta pessoal, adicionei um novo recurso nela chamado de AZURE KEY VAULT que serve para armazenar e acessar segredos de maneira segura. Um segredo é qualquer coisa a qual você queira controlar rigidamente o acesso, como conection string, chaves de API, senhas, certificados ou chaves criptográficas.
Diante disso adicionei uma nova secret com o valor da connection string de acesso ao banco da minha aplicação.
Para conectar com o recurso do azure através da aplicação foi necessario a instalaçao de 3 pacotes para que busque informações referentes ao key vault criado
- Azure.Extensions.AspNetCore.Configuration.Secrets
- Azure.Identity
- Azure.Security.KeyVault.Secrets

Classes que foram modificadas para acessar o azure keyvault
- appsettings.json
- startup.cs
