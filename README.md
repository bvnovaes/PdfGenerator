| Código | Descrição                  | Explicação de Uso                                                                 	 | Verbos HTTP Aplicáveis |
|--------|----------------------------|------------------------------------------------------------------------------------------|------------------------|
| 200    | OK                         | A requisição foi bem-sucedida e o servidor retornou os dados solicitados.         	     | GET, POST, PUT, DELETE |
| 201    | Created                    | A requisição foi bem-sucedida e um novo recurso foi criado.                       	     | POST, PUT              |
| 204    | No Content                 | A requisição foi bem-sucedida, mas não há conteúdo para retornar.                 	     | DELETE, PUT            |
| 400    | Bad Request                | A requisição é inválida ou malformada.                                            	     | GET, POST, PUT, DELETE |
| 401    | Unauthorized               | A requisição requer autenticação do usuário.                                      	     | GET, POST, PUT, DELETE |
| 403    | Forbidden                  | O servidor entendeu a requisição, mas se recusa a autorizá-la.                    	     | GET, POST, PUT, DELETE |
| 404    | Not Found                  | O recurso solicitado não foi encontrado no servidor.                              	     | GET, POST, PUT, DELETE |
| 409    | Conflict                   | A requisição não pôde ser completada devido a um conflito com o estado atual do recurso. | POST, PUT, DELETE      |
| 422    | Unprocessable Entity       | A requisição está bem formada, mas não pode ser processada devido a erros semânticos.    | POST, PUT              |
| 500    | Internal Server Error      | Ocorreu um erro inesperado no servidor.                                                  | GET, POST, PUT, DELETE |
| 503    | Service Unavailable        | O servidor está temporariamente indisponível, geralmente devido a manutenção.            | GET, POST, PUT, DELETE | 
