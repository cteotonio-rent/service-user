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


## API Endpoints

### `POST /order`

Registra um novo pedido.

| Propriedade | Descrição |
| ----------- | ----------- |
| Descrição | Este endpoint é usado para registrar um novo pedido. |
| Parâmetros | Um objeto JSON que representa o pedido a ser registrado. |
| Resposta | Retorna um objeto JSON que representa o pedido registrado. |
| Erros | Se ocorrer um erro durante o registro do pedido, o servidor retornará um código de status HTTP 400 e uma mensagem de erro no formato JSON. |

### `GET /order/{orderid}/notifieddeliveryperson`

Obtém a pessoa de entrega notificada para um pedido específico.

| Propriedade | Descrição |
| ----------- | ----------- |
| Descrição | Este endpoint é usado para obter a pessoa de entrega notificada para um pedido específico. |
| Parâmetros | O ID do pedido. |
| Resposta | Retorna um objeto JSON que representa a pessoa de entrega notificada. Se não houver pessoa de entrega notificada, retorna um código de status HTTP 204. |
| Erros | Nenhum erro específico documentado para este endpoint. |

### `POST /order/accept`

Aceita um pedido.

| Propriedade | Descrição |
| ----------- | ----------- |
| Descrição | Este endpoint é usado para aceitar um pedido. |
| Parâmetros | Um objeto JSON que representa o pedido a ser aceito. |
| Resposta | Não retorna nenhum conteúdo. Retorna um código de status HTTP 204. |
| Erros | Se ocorrer um erro durante a aceitação do pedido, o servidor retornará um código de status HTTP 400 e uma mensagem de erro no formato JSON. Se o pedido não for encontrado, retorna um código de status HTTP 404. |

### `POST /order/deliver`

Entrega um pedido.

| Propriedade | Descrição |
| ----------- | ----------- |
| Descrição | Este endpoint é usado para entregar um pedido. |
| Parâmetros | Um objeto JSON que representa o pedido a ser entregue. |
| Resposta | Não retorna nenhum conteúdo. Retorna um código de status HTTP 204. |
| Erros | Se ocorrer um erro durante a entrega do pedido, o servidor retornará um código de status HTTP 400 e uma mensagem de erro no formato JSON. Se o pedido não for encontrado, retorna um código de status HTTP 404. |

## API Endpoints

### `POST /rental`

Registra um novo aluguel.

| Propriedade | Descrição |
| ----------- | ----------- |
| Descrição | Este endpoint é usado para registrar um novo aluguel. |
| Parâmetros | Um objeto JSON que representa o aluguel a ser registrado. |
| Resposta | Retorna um objeto JSON que representa o aluguel registrado. Retorna um código de status HTTP 201 se o aluguel for registrado com sucesso. |
| Erros | Se ocorrer um erro durante o registro do aluguel, o servidor retornará um código de status HTTP 400 e uma mensagem de erro no formato JSON. |

#### Exemplo de resposta:

{ "id": "123", "propertyId": "456", "tenantId": "789", "rentalPeriod": { "start": "2022-01-01", "end": "2022-12-31" }, "price": 1000.00 }

#### Exemplo de resposta de erro:

{ "error": "Invalid request" }
