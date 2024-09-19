# Api Responsável por receber Pedidos que estejam no formato requerido:
```json
{
  "identificador": "guid",
  "dataVenda": "datetime",
  "cliente": {
    "clienteId": "guid",
    "nome": "string",
    "cpf": "string",
    "categoria": "string"
  },
  "itens": [
    {
      "produtoId": "number",
      "descricao": "string",
      "quantidade": "decimal",
      "precoUnitario": "decimal"
    },
    {
      "produtoId": "integer",
      "descricao": "string",
      "quantidade": "decimal",
      "precoUnitario": "decimal"
    }
  ]
}
```
### Endpoint: v1/order - Metódo POST
Para a conexão com o banco de dados, é necessário ter instalado PGAdmin
  #### Registrar um servidor com as seguintes especificações de acordo com o appsettings
    General:
      Name: postgres
    Connection:
      Hostname/address: localhost 
      Port: 5432
      Passowrd: admin
    Ou se preferir, alterar o appsettings para conectar com o servidor já registrado

### Regras de negócio:
```
  - Se o indetificador já estiver sido usado, retornará status code 409 com a mensagem que o identificado já existe
  - Se o produto não existir na tabela Products, ele será cadastrado uma única vez.
  - Se o cliente não existir ele será cadastrado, caso exista o id dele será utilizado como FK na tabela orders
  - Quando tudo tiver sido criado, será enviado para a API da STI3 O pedido
  * Garantido que todo novo pedido com um novo identificador será gravado ! *
```
## Estrutura do Banco de Dados
```
Tabelas:
  Orders (Pedidos)
  	Campos:
  		Id (PK),
  		Date,
  		Guid,
  		Total,
  		Subtotal,
  		Discount,
  		CustomersId (FK para Customers.Id),
  
  Customers (Clientes) 
  	Campos:
  		Id (PK),
  		Guid,
  		Name,
  		CPF,
  		Category,
  		
  OrderItems (Itens do Pedido)
  	Campos:
  		Id (PK),
  		Quantity,
  		Total,
  		Subtotal,
  		Discount,
  		OrdersId (FK para Orders.Id),
  		ProductsId (FK para Products.Id),
    
  Products (Produtos)
  	Campos:
  		Id (PK),
  		Description,
  		Price
   

