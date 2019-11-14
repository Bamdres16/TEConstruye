Este documento tiene como finalidad especificar las diferentes peticiones que se le puede hacer al API de TEConstruye

## Clientes
### Métodos implementados
#### Retorna un JSON con todos los Clientes existentes en la base de datos de TEConstruye
- GET: http://teconstruyeapi.azurewebsites.net/api/Clientes
#### Retorna un JSON con los datos del Cliente que corresponda al id si existe, si no existe genera un 404 Not found
- GET: http://teconstruyeapi.azurewebsites.net/api/Clientes/{id}
#### Añade un nuevo Cliente a la base de datos de TEConstruye, pero si la cédula ya se encuentra registrada en TEConstruye genera "Ya existe ese cliente en TEConstruye" o sí existe en la base de datos de TECres "Ya existe ese cliente en TECres" y genera un código de error 409 Conflict
- POST: http://teconstruyeapi.azurewebsites.net/api/Clientes
### Entradas:
```json
	{
		"nombre": "sample string 1",
		"apellido1": "sample string 2",
		"apellido2": "sample string 3",
		"cedula": "sample string 4",
		"numero_telefono": "sample string 5"
	}
```
#### Elimina el cliente que corresponda al id, si no existe genera un 404 Not found
- DELETE: http://teconstruyeapi.azurewebsites.net/api/Clientes/{id}
		
## Etapas:
### Métodos implementados:
#### Obtiene una lista de todas las etapas
- GET: http://teconstruyeapi.azurewebsites.net/api/Etapas
#### Permite almacenar una nueva etapa
- POST:  http://teconstruyeapi.azurewebsites.net/api/Etapas
``` json
Entradas:
{
  "nombre": "sample string 1",
  "descripcion": "sample string 2"
}

```
#### Permite actualizar un registro de etapa mediante un id y el nuevo registro
- PUT: http://teconstruyeapi.azurewebsites.net/api/Etapas/{id}
#### Entrada:
```json

	{
  "nombre": "sample string 1",
  "descripcion": "sample string 2",
  "id": 3
}
``` 

Nota: El JSON debe incluir el id ya que se compara para evitar que sea diferente al id del link
#### Elimina el cliente que corresponda al id, si no existe genera un 404 Not found
- DELETE:  http://teconstruyeapi.azurewebsites.net/api/Etapas/{id}

## Ubicación:
### Métodos implementados:
#### Retorna la lista de provincias de Costa Ric
- GET: http://teconstruyeapi.azurewebsites.net/api/Ubicacion
#### Retorna la lista de los cantones que corresponden a la Provincia dada	
- GET: http://teconstruyeapi.azurewebsites.net/api/Ubicacion?Provincia=Nombre
#### Retorna la lista de los distritos que corresponde a la Provincia y Cantón dados
- GET: http://teconstruyeapi.azurewebsites.net/api/Ubicacion?Provincia=Nombre&Canton=Nombre


## Especialidad:
### Métodos implementados
#### Retorna la lista de especialidades
- GET: http://teconstruyeapi.azurewebsites.net/api/Especialidad
#### Retorna una especialidad por su id
- GET: http://teconstruyeapi.azurewebsites.net/api/Especialidad/{id}
#### Permite actualizar un registro de especialidad mediante un id y el nuevo registro
- PUT: http://teconstruyeapi.azurewebsites.net/api/Especialidad/{id}
Entrada:
``` json
	{
  "nombre": "sample string 1",
  "id": 2
}
``` 
Nota: El JSON debe incluir el id ya que se compara para evitar que sea diferente al id del link
#### Permite añadir una nueva especialidad, debe incluir solo el nombre en el JSON.
- POST: http://teconstruyeapi.azurewebsites.net/api/Especialidad
Entrada:
``` json
	{
  "nombre": "sample string 1" 
}
``` 






