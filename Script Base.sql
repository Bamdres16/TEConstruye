 create table Cliente(
	 nombre varchar(40),
	 apellido1 varchar(40),	
	 apellido2 varchar(40),
	 cedula varchar(15),
	 numero_telefono varchar(15),
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
	 propietario int,
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
	 nombre varchar(60),
	 descripcion varchar(250),
	 id int GENERATED ALWAYS AS IDENTITY,
	 UNIQUE (nombre),
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
	 cedula varchar(30),
	 numero_telefono varchar(30),
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
	 cedula varchar(30),
	 numero_telefono varchar(30),
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
	 PRIMARY KEY (id),
	 UNIQUE(nombre)
 );
 
    create table Requiere(
	 id_etapa int,
	 codigo_material varchar(25),
	 cantidad int,
	 PRIMARY KEY(id_etapa,codigo_material)
 );
 
    create table Tiene(
	 id_etapa int,
	 id_obra int,
	 fecha_incio date,
	 fecha_finalizacion date,
	 PRIMARY KEY (id_etapa,id_obra)
 );
 
    create table Labora_en(
	 id_empleado int,
	 id_obra int,
	 horas_laboradas float,
	 PRIMARY KEY(id_empleado, id_obra)
 );
 
    create table Trabaja_en(
	 id_arquitecto int,
	 id_obra int,
	 horas_laboradas float,
	 PRIMARY KEY (id_arquitecto, id_obra)
 );
 
    create table Diseña(
	 id_ingeniero int,
	 id_obra int,
	 horas_laboradas float,
	 PRIMARY KEY (id_ingeniero, id_obra)
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

ALTER TABLE Obra
ADD CONSTRAINT FK_OBRAUbicacion FOREIGN KEY (ubicacion) REFERENCES Ubicacion(id);

ALTER TABLE Obra
ADD CONSTRAINT FK_PROPIETARIO_OBRA FOREIGN KEY (propietario) REFERENCES Cliente(id);


CREATE FUNCTION etapa_default() RETURNS trigger AS $etapa_default$
    BEGIN
        IF OLD.id >= 1 AND OLD.id <= 20 THEN
            RAISE EXCEPTION 'No se puede eliminar o actualizar, es un valor default';
		ELSE
			RETURN OLD;
		END IF;
		
    END;
$etapa_default$ LANGUAGE plpgsql;

CREATE TRIGGER etapa_default BEFORE DELETE OR UPDATE ON etapa
    FOR EACH ROW EXECUTE PROCEDURE etapa_default();


CREATE FUNCTION especialidad_default() RETURNS trigger AS $especialidad_default$
    BEGIN
        IF OLD.id >= 1 AND OLD.id <= 3 THEN
            RAISE EXCEPTION 'No se puede eliminar o actualizar, es un valor default';
		ELSE
			RETURN OLD;
		END IF;
    END;
$especialidad_default$ LANGUAGE plpgsql;

CREATE TRIGGER especialidad_default BEFORE DELETE OR UPDATE ON especialidad
    FOR EACH ROW EXECUTE PROCEDURE especialidad_default();
