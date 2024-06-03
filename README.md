# Rent Project - Rent Services
Repositório com arquivos do projeto de Serviço e Consumidor de Fila

>[!IMPORTANTE]
>Caso você ainda não tenha visitado a página inicial deste projeto, recomendo começar por ela, pois há informações importantes sobre o projeto e configurações de ambiente.
>[Pagina inicial](https://github.com/cteotonio-rent)

## API Endpoints

### `GET /rentservice/login`

| Propriedade | Descrição   |
| ----------- | ----------- |
| Descrição   | Este endpoint é usado para autenticar um usuário e retornar um token de autenticação. |
| Parâmetros  | Nenhum. |
| Resposta    | Retorna um token de autenticação no formato JSON. |
| Erros       | Se ocorrer um erro durante a autenticação, o servidor retornará um código de status HTTP 400 e uma mensagem de erro no formato JSON. |

#### Exemplo de resposta:
{ "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" }
