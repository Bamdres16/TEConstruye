Este documento tiene como finalidad especificar las diferentes peticiones que se le puede hacer al API de TEConstruye

##Clientes
-Métodos implementados
	-GET: http://teconstruyeapi.azurewebsites.net/api/Clientes
		Descripción: Retorna un JSON con todos los Clientes existentes en la base de datos de TEConstruye
	-GET: http://teconstruyeapi.azurewebsites.net/api/Clientes/{id}
		Descripción: Retorna un JSON con los datos del Cliente que corresponda al id si existe, si no existe genera un 404 Not found
-	-POST: http://teconstruyeapi.azurewebsites.net/api/Clientes
		Entradas:
			{
  				"nombre": "sample string 1",
  				"apellido1": "sample string 2",
  				"apellido2": "sample string 3",
 			 	"cedula": "sample string 4",
  				"numero_telefono": "sample string 5"
  			}
Descripción: Añade un nuevo Cliente a la base de datos de TEConstruye, pero si la cédula ya se encuentra registrada en TEConstruye genera "Ya existe ese cliente en TEConstruye" o sí existe en la base de datos de TECres "Ya existe ese cliente en TECres" y genera un código de error 409 Conflict
	-DELETE: http://teconstruyeapi.azurewebsites.net/api/Clientes/{id}
		Descripción: Elimina el cliente que corresponda al id, si no existe genera un 404 Not found
##Etapas:
Métodos implementados:
GET: http://teconstruyeapi.azurewebsites.net/api/Etapas
Descripción: Obtiene una lista de todas las etapas
POST:  http://teconstruyeapi.azurewebsites.net/api/Etapas
Entradas:
{
  "nombre": "sample string 1",
  "descripcion": "sample string 2"
}
PUT: http://teconstruyeapi.azurewebsites.net/api/Etapas/{id}
Entrada:
	{
  "nombre": "sample string 1",
  "descripcion": "sample string 2",
  "id": 3
}

Nota: El JSON debe incluir el id ya que se compara para evitar que sea diferente al id del link
Descripción: Permite actualizar un registro de etapa mediante un id y el nuevo registro
DELETE:  http://teconstruyeapi.azurewebsites.net/api/Etapas/{id}
Descripción: Elimina el cliente que corresponda al id, si no existe genera un 404 Not found
##Ubicación:
	Métodos implementados:
GET: http://teconstruyeapi.azurewebsites.net/api/Ubicacion
		Descripción: Retorna la lista de provincias de Costa Rica
GET: http://teconstruyeapi.azurewebsites.net/api/Ubicacion?Provincia=Nombre
Descripción: Retorna la lista de los cantones que corresponden a la Provincia dada
GET: http://teconstruyeapi.azurewebsites.net/api/Ubicacion?Provincia=Nombre&Canton=Nombre
Descripción: Retorna la lista de los distritos que corresponde a la Provincia y Cantón dados.

##Especialidad:
	Métodos implementados
GET: http://teconstruyeapi.azurewebsites.net/api/Especialidad
		Descripción: Retorna la lista de especialidades
GET: http://teconstruyeapi.azurewebsites.net/api/Especialidad/{id}
Descripción: Retorna una especialidad por su id
PUT: http://teconstruyeapi.azurewebsites.net/api/Especialidad/{id}
Entrada:
	{
  "nombre": "sample string 1",
  "id": 2
}
Nota: El JSON debe incluir el id ya que se compara para evitar que sea diferente al id del link
Descripción: Permite actualizar un registro de especialidad mediante un id y el nuevo registro
POST: http://teconstruyeapi.azurewebsites.net/api/Especialidad
Entrada:
	{
  "nombre": "sample string 1" 
}
Descripción: Permite añadir una nueva especialidad, debe incluir solo el nombre en el JSON.







