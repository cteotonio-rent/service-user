# Rent Project - Rent Services
Repositório com arquivos do projeto de Serviço e Consumidor de Fila

>[!IMPORTANTE]
>Caso você ainda não tenha visitado a página inicial deste projeto, recomendo começar por ela, pois há informações importantes sobre o projeto e configurações de ambiente.
>[Pagina inicial](https://github.com/cteotonio-rent)

## API Endpoints

Os EndPoints pode ser vistos neste [link](https://documenter.getpostman.com/view/3894025/2sA3Qy4oiY#83fb3122-2d87-4b0a-a87c-ae62937d42d7) via publicação do PostMan 

## Teste Unitários e Integrados
1) É necessário que o Docker esteja rodando pois os testes integrados fazem registros de informações em conteiners do MongoDB
2) O que é testado?
- Validators
- Use Cases
- End Points

3) Para testes usando o PostMan recomendo a utilização do ambiente do docker-compose ver em [repositório docker](https://github.com/cteotonio-rent/docker)

## Estrutura do Projeto
- backend
- - API (Projeto Web API)
  - Application (Projeto com os Casos de Use e Validadores)
  - Domain (Projetos com as interfaces e entidades do projeto)
  - Infrastructure (Projeto com a implementação de componentes externos)
  - Consumer (Projeto consumidor da fila de novos pedidos)
- 
