Este documento tiene como finalidad especificar las diferentes peticiones que se le puede hacer al API de TEConstruye

## Clientes
### Métodos implementados
#### Retorna un JSON con todos los Clientes existentes en la base de datos de TEConstruye
- GET: http://teconstruyeapi.azurewebsites.net/api/Clientes
#### Retorna un JSON con los datos del Cliente que corresponda al id si existe, si no existe genera un 404 Not found
- GET: http://teconstruyeapi.azurewebsites.net/api/Clientes/{id}
#### Añade un nuevo Cliente a la base de datos de TEConstruye, pero si la cédula ya se encuentra registrada en TEConstruye genera "Ya existe ese cliente en TEConstruye" o sí existe en la base de datos de TECres "Ya existe ese cliente en TECres" y genera un código de error 409 Conflict
- POST: http://teconstruyeapi.azurewebsites.net/api/Clientes
#### Entradas
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
#### Entradas
```json
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
#### Entradas
```json
{
	  "nombre": "sample string 1",
	  "id": 2
}
``` 
Nota: El JSON debe incluir el id ya que se compara para evitar que sea diferente al id del link
#### Permite añadir una nueva especialidad, debe incluir solo el nombre en el JSON.
- POST: http://teconstruyeapi.azurewebsites.net/api/Especialidad

Entrada:
```json
{
  	"nombre": "sample string 1" 
}
``` 

## Ingenieros
### Métodos implementados
#### Para retonar la lista de todos los ingenieros
- GET http://teconstruyeapi.azurewebsites.net/api/Ingenieros
#### Para obtener un ingeniero en específico se debe proveer el id del ingeniero
- GET http://teconstruyeapi.azurewebsites.net/api/Ingenieros/{id}
#### Para añadir un nuevo ingeniero, se toma en cuenta que el número de cédula y código de ingeniero no se repiten, además el código de ingeniero, cedula y numero de telefono son tipo string, esto ya que el valor de estos puede ser mayor que el valor máximo del tipo integer. Si la cédula ya existe retorna el mensaje: "Esa cédula ya está en TEConstruye" con un código 409 Conflict, y si el código de ingeniero ya existe retorna "Esa código de ingeniero ya está en TEConstruye" con un código 409 Conflict.
- POST http://teconstruyeapi.azurewebsites.net/api/Ingenieros
#### Entradas
```json
{
	  "nombre": "sample string 1",
	  "apellido1": "sample string 2",
	  "apellido2": "sample string 3",
	  "cedula": "sample string 4",
	  "numero_telefono": "sample string 5",
	  "codigo_ingeniero": "sample string 6",
	  "id_especialidad": 1
}
```
#### Para logearse como ingeniero
- POST http://teconstruyeapi.azurewebsites.net/api/Ingenieros/login
#### Entradas:
```json
{
	  "codigo_ingeniero": "sample string 1",
	  "contrasena": "sample string 2"
}
```
 
#### Para eliminar un ingeniero, se debe proveer únicamente el id
- DELETE http://teconstruyeapi.azurewebsites.net/api/Ingenieros/{id}
#### Para actualizar el registro de un ingeniero existente, se debe proveer el id actual por parámetro y además debe venir en el JSON ya que se compara si estos son iguales.
- PUT http://teconstruyeapi.azurewebsites.net/api/Ingenieros/{id}
#### Entradas
```json
{
	  "nombre": "sample string 1",
	  "apellido1": "sample string 2",
	  "apellido2": "sample string 3",
	  "cedula": "sample string 4",
	  "contrasena" : "sample string 5",
	  "numero_telefono": "sample string 6",
	  "codigo_ingeniero": "sample string 7",
	  "id_especialidad": 1,
	  "id" : 1
}
``` 

## Arquitectos

### Métodos implementados

#### Para retonar la lista de todos los Arquitectos
- GET http://teconstruyeapi.azurewebsites.net/api/Arquitectos
#### Para obtener un arquitecto en específico se debe proveer el id del arquitecto
- GET http://teconstruyeapi.azurewebsites.net/api/Arquitectos/{id}
#### Para añadir un nuevo arquitecto, se toma en cuenta que el número de cédula y código de arquitecto no se repiten, además el código de ingeniero, cedula y numero de telefono son tipo string, esto ya que el valor de estos puede ser mayor que el valor máximo del tipo integer. Si la cédula ya existe retorna el mensaje: "Esa cédula ya está en TEConstruye" con un código 409 Conflict, y si el código de arquitecto ya existe retorna "Ese código de arquitecto ya está en TEConstruye" con un código 409 Conflict.
- POST http://teconstruyeapi.azurewebsites.net/api/Arquitectos
#### Entradas
```json
{
	  "nombre": "sample string 1",
	  "apellido1": "sample string 2",
	  "apellido2": "sample string 3",
	  "cedula": "sample string 4",
	  "contrasena" : "sample string 5",
	  "numero_telefono": "sample string 6",
	  "codigo_arquitecto": "sample string 7",
	  "id_especialidad": 1
}
``` 
#### Para logearse como arquitecto
- POST http://teconstruyeapi.azurewebsites.net/api/Arquitectos/login
#### Entradas:
```json
{
	  "codigo_arquitecto": "sample string 1",
	  "contrasena": "sample string 2"
}
```
#### Para eliminar un arquitecto, se debe proveer únicamente el id
- DELETE http://teconstruyeapi.azurewebsites.net/api/Arquitectos/{id}
#### Para actualizar el registro de un arquitecto existente, se debe proveer el id actual por parámetro y además debe venir en el JSON ya que se compara si estos son iguales.
- PUT http://teconstruyeapi.azurewebsites.net/api/Arquitectos/{id}
#### Entradas
```json
{
	  "nombre": "sample string 1",
	  "apellido1": "sample string 2",
	  "apellido2": "sample string 3",
	  "cedula": "sample string 4",
	  "numero_telefono": "sample string 5",
	  "codigo_arquitecto": "sample string 6",
	  "id_especialidad": 1,
	  "id" : 1
}
``` 
## Admin

### Métodos implementados

#### Para logearse
- POST http://teconstruyeapi.azurewebsites.net/api/Admin
#### Entradas
```json
{
  "usuario": "sample string 1",
  "contrasena": "sample string 2"
}
```

## Materiales

### Métodos implementados

#### Para obtener la lista de todos los materiales
- GET http://teconstruyeapi.azurewebsites.net/api/Material

#### Para agregar un nuevo material
- POST http://teconstruyeapi.azurewebsites.net/api/Material
#### Entradas
```json
{
  "nombre": "sample string 1",
  "precio_unitario": 1.1,
  "codigo": "sample string 2"
}
```

#### Para eliminar un material se debe proveer el código de material
- DELETE http://teconstruyeapi.azurewebsites.net/api/Material/{id}

## Empleados

### Métodos implementados

#### Para obtener la lista de todos los empleados
- GET http://teconstruyeapi.azurewebsites.net/api/Empleados

#### Para registrar un nuevo empleado
- POST http://teconstruyeapi.azurewebsites.net/api/Empleados

#### Entradas:

```json
{
  "nombre": "sample string 1",
  "apellido1": "sample string 2",
  "apellido2": "sample string 3",
  "cedula": "sample string 4",
  "numero_telefono": "sample string 5",
  "pago_hora": 1.1
}
```

#### Para eliminar un empleado se debe colocar el id
- DELETE http://teconstruyeapi.azurewebsites.net/api/Empleados/{id}

## Obra

### Métodos implementados

#### Para obtener la lista de todas las obras existentes en la base de datos
- GET http://teconstruyeapi.azurewebsites.net/api/Obras

#### Para registrar una obra se siguen los siguientes pasos
1) Se registran las descripciones de la obra con
	- POST http://teconstruyeapi.azurewebsites.net/api/Obras
	##### Entradas
	```json
		{
		  "nombre_obra": "sample string 1",
		  "ubicacion": 1,
		  "cantidad_habitaciones": 1,
		  "cantidad_banos": 1,
		  "cantidad_pisos": 1,
		  "area_construccion": 1.1,
		  "area_lote": 1,
		  "propietario": 1
		}
	```
2) Para asignar una etapa a una obra
	- PUT http://teconstruyeapi.azurewebsites.net/api/Proyecto/asignaretapa
	##### Entradas
	```json
		{
			"id_etapa": 2,
			"id_obra": 2,
			"fecha_incio": "2019-11-17",
			"fecha_finalizacion": "2019-11-17"
		}
	```
	##### Para obtener la lista de etapas asociadas a un proyecto
	- GET http://teconstruyeapi.azurewebsites.net/api/Proyecto/etapas/{id_obra}
	