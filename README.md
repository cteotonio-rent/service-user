# Rent Project - Rent Services
Repositório com arquivos do projeto de Serviço e Consumidor de Fila

>[!IMPORTANTE]
>Caso você ainda não tenha visitado a página inicial deste projeto, recomendo começar por ela, pois há informações importantes sobre o projeto e configurações de ambiente.
>[Pagina inicial](https://github.com/cteotonio-rent)

## Estrutura do Projeto
- **backend**
  - api (Projeto Web API)
  - application (Projeto com os Casos de Use e Validadores)
  - domain (Projetos com as interfaces e entidades do projeto)
  - infrastructure (Projeto com a implementação de componentes externos)
  - consumer (Projeto consumidor da fila de novos pedidos)
- **shared**
  - communication (Projeto com os objetos ViewModels (Requests e Responses)
  - exceptions (Projeto com as exceções do projeto e Recuso para multi idiomas)
- **tests**
  - CommomTestUtilities (Projeto com os Buiders dos objetos necessários para realização dos testes)
  - UseCase Test -> (Projeto para testes unitários dos Casos de Usos)
  - Validators Test -> (Projeto para testes unitários dos Validadores)
  - WebApi Test -> (Projeto para testes integrados: Api -> Application -> Infrastructure -> MongoDB)

## Blibliotecas utilizadas
- **Newtonsoft.Json** (amplamente utilizada para manimulação de documentos Json)
- **Serilog** (Utilizado para captura de logs da aplicação e registrá-los na ferramenta Grafana
  - AspNetCore
  - Skins.GrafanaLoki
- **AutoMapper** (componente para mapear objetos)
- **FluenteValidation** (Ela permite que você crie regras de validação usando uma sintaxe fluente, o que torna o código de validação mais legível e fácil de escrever.)
- **SixLabors.ImageSharp** (Foi utilizada esta biblioteca para manipulação de imagens pois ela da suporte a outras plataformas, não ficando limitada somente ao windows como no caso da System.Drawing)
- **MongoDB.Bson** (biblioteca utilizada para manipulação de documentos BSON (Binary JSON), que é o formato de dados usado pelo MongoDB)
- **AWSSDK**
  - Extensions.NETCore.Setup (biblioteca para configuração geral com a AWS)
  - S3 (bilioteca para manipulação buckets S3
  - SQS (biblioteca para manipulação de filas
- **MongoDB.EntityFrameworkCore** (provedor do banco de dados MongoDB para EntityFrameworCore
- **System.IdentityModel.Tokens.Jwt** (biblioteca para geração de tokens)
- **Testcontainers.MongoDb** (biblioteca para instanciar containers MongoDb para testes)
- **Bogus** (biblioteca para gerar dados Fake)
- **Moq** (Mock -> biblioteca para geração de objetos falsos a partir de classes ou interfaces, para simulação de dependências reais)
- **coverlet.collector** (biblioteca para coletar informações de cobertura de código durante a execução de testes unitários)
- **FluentAssertions** (biblioteca que fornece um conjunto de extensões de método de fácil leitura para ajudar a tornar seus testes mais claros e legíveis.
- **xUnit** (ferramenta open source e é a estrutura de testes recomendada para projetos .NET Core)
- **microsoft.aspnetcore.mvc.testing** (biblioteca para .NET Core que fornece uma maneira de escrever testes de integração funcionais)

## API Endpoints
Os EndPoints pode ser vistos neste [link](https://documenter.getpostman.com/view/3894025/2sA3Qy4oiY#83fb3122-2d87-4b0a-a87c-ae62937d42d7) via publicação do PostMan 

## Teste Unitários e Integrados
1) É necessário que o Docker esteja rodando pois os testes integrados fazem registros de informações em conteiners do MongoDB
2) O que é testado?
- Validators
- Use Cases
- End Points

3) Para testes usando o PostMan recomendo a utilização do ambiente do docker-compose ver em [repositório docker](https://github.com/cteotonio-rent/docker)
