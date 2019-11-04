 create table Cliente(
	 nombre varchar(40),
	 apellido1 varchar(40),	
	 apellido2 varchar(40),
	 cedula int,
	 numero_telefono int,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id),
	 UNIQUE(cedula)
 );
 
  create table Obra(
	 nombre_obra varchar(80),
	 ubicacion int,	
	 cantidad_habitaciones int,
	 cantidad_banos int,
	 cantidad_pisos int,
	 area_construccion float,
	 area_lote int,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id)
 );
 
 CREATE TABLE Ubicacion(
	 provincia varchar(50) NOT NULL,
  	 canton varchar(50) NOT NULL,
	 distrito varchar(50) NOT NULL,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id),
	 UNIQUE(provincia, canton, distrito)
);

  create table Etapa(
	 nombre varchar(50),
	 descripcion varchar(100),
	 fecha_incio date,
	 fecha_finalizacion date,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id)
 );
 
   create table Material(
	 nombre varchar(50),
	 precio_unitario float,
	 codigo varchar(25),
	 PRIMARY KEY (codigo)
 );
 
  create table Empleado(
	 nombre varchar(40),
	 apellido1 varchar(40),	
	 apellido2 varchar(40),
	 cedula int,
	 numero_telefono int,
	 pago_hora float,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id),
	 UNIQUE(cedula)
 );
 
   create table Arquitecto(
	 nombre varchar(40),
	 apellido1 varchar(40),	
	 apellido2 varchar(40),
	 cedula int,
	 numero_telefono int,
	 codigo_arquitecto varchar(20),
	 id_especialidad int,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id),
	 UNIQUE(cedula),
	 UNIQUE(codigo_arquitecto)
 );
 
    create table Ingeniero(
	 nombre varchar(40),
	 apellido1 varchar(40),	
	 apellido2 varchar(40),
	 cedula int,
	 numero_telefono int,
	 codigo_ingeniero varchar(20),
	 id_especialidad int,
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id),
	 UNIQUE(cedula),
	 UNIQUE(codigo_ingeniero)
 );
 
    create table Especialidad(
	 nombre varchar(40),
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id)
 );
 
    create table Requiere(
	 id_etapa int,
	 codigo_material varchar(25),
	 cantidad int
 );
 
    create table Tiene(
	 id_etapa int,
	 id_obra int
 );
 
    create table Labora_en(
	 id_empleado int,
	 id_obra int,
	 horas_laboradas float
 );
 
    create table Trabaja_en(
	 id_arquitecto int,
	 id_obra int,
	 horas_laboradas float
 );
 
    create table Diseña(
	 id_ingeniero int,
	 id_obra int,
	 horas_laboradas float
 );
 
 
ALTER TABLE ingeniero
ADD CONSTRAINT FK_ESPECIALIDAD FOREIGN KEY (id_especialidad) REFERENCES especialidad(id);

ALTER TABLE arquitecto
ADD CONSTRAINT FK_ESPECIALIDAD FOREIGN KEY (id_especialidad) REFERENCES especialidad(id);

ALTER TABLE requiere
ADD CONSTRAINT FK_MATERIAL FOREIGN KEY (codigo_material) REFERENCES material(codigo);

 ALTER TABLE diseña	
ADD CONSTRAINT FK_ARQUITECTO FOREIGN KEY (id_ingeniero) REFERENCES ingeniero(id);	

ALTER TABLE diseña	
ADD CONSTRAINT FK_OBRA FOREIGN KEY (id_obra) REFERENCES obra(id);	

ALTER TABLE labora_en	
ADD CONSTRAINT FK_EMPLEADO FOREIGN KEY (id_empleado) REFERENCES empleado(id);	

ALTER TABLE labora_en	
ADD CONSTRAINT FK_OBRA FOREIGN KEY (id_obra) REFERENCES obra(id);

ALTER TABLE Trabaja_en	
ADD CONSTRAINT FK_ARQUITECTO FOREIGN KEY (id_arquitecto) REFERENCES Arquitecto(id);


ALTER TABLE Trabaja_en
ADD CONSTRAINT FK_OBRA FOREIGN KEY (id_obra) REFERENCES obra(id);

ALTER TABLE Tiene
ADD CONSTRAINT FK_ETAPA FOREIGN KEY (id_etapa) REFERENCES Etapa(id);

ALTER TABLE Tiene 
ADD CONSTRAINT FK_OBRA FOREIGN KEY (id_obra) REFERENCES obra(id);
