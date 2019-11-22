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
	 cedula varchar(30),
	 numero_telefono varchar(30),
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
	 contrasena varchar (100),
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
	 contrasena varchar (100),
	 id int GENERATED ALWAYS AS IDENTITY,
	 PRIMARY KEY (id),
	 UNIQUE(cedula),
	 UNIQUE(codigo_ingeniero)
 );
 
	create table Admin (
	usuario varchar(30),
	contrasena varchar(100),
	correo varchar (100),
	id int GENERATED ALWAYS AS Identity,
	UNIQUE (usuario),
	UNIQUE (correo),
	PRIMARY KEY (id)
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
	 id_obra int,
	 cantidad int,
	 PRIMARY KEY(id_etapa,codigo_material,id_obra)
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
	 semana int,
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
 
 create table Gasto (
	 proveedor varchar (40) not null,
	 foto varchar(150) not null,
	 numero_factura varchar(30) not null,
	 id_compra int GENERATED ALWAYS AS IDENTITY,
	 id_etapa int not null,
	 semana int,
	 id_obra int not null,
	 PRIMARY KEY (id_compra)
 );
 create table compra (
	 id_compra int,
	 codigo_material varchar(25),
	 PRIMARY KEY (id_compra, codigo_material)
 );
 
ALTER TABLE Gasto
ADD CONSTRAINT FK_ETAPAGASTO FOREIGN KEY (id_etapa) REFERENCES Etapa(id);

ALTER TABLE Gasto
ADD CONSTRAINT FK_GASTOBRA FOREIGN KEY (id_obra) REFERENCES Obra(id);

ALTER TABLE Compra
ADD CONSTRAINT FK_COMPRAID FOREIGN KEY (id_compra) REFERENCES Gasto(id_compra);

ALTER TABLE Compra
ADD CONSTRAINT FK_COMPRAMATERIAL FOREIGN KEY (codigo_material) REFERENCES Material(codigo);
 
 
ALTER TABLE ingeniero
ADD CONSTRAINT FK_ESPECIALIDAD FOREIGN KEY (id_especialidad) REFERENCES especialidad(id);

ALTER TABLE arquitecto
ADD CONSTRAINT FK_ESPECIALIDAD FOREIGN KEY (id_especialidad) REFERENCES especialidad(id);

ALTER TABLE requiere
ADD CONSTRAINT FK_MATERIAL FOREIGN KEY (codigo_material) REFERENCES material(codigo);

ALTER TABLE requiere
ADD CONSTRAINT FK_ETAPAREQUIERE FOREIGN KEY (id_etapa ) REFERENCES etapa(id);

ALTER TABLE requiere
ADD CONSTRAINT FK_OBRAREQUIERE FOREIGN KEY (id_obra) REFERENCES obra(id);

 ALTER TABLE diseña	
ADD CONSTRAINT FK_INGENIERO FOREIGN KEY (id_ingeniero) REFERENCES ingeniero(id);	

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


CREATE OR REPLACE FUNCTION Presupuesto1() 
RETURNS TABLE (
	Nombre_etapa varchar (60),
	nombre_obra varchar(60),
	Precio_Etapa float
)
AS $$
BEGIN
RETURN QUERY
    SELECT E.nombre, O.nombre_obra,SUM(R.cantidad * M.precio_unitario) as Precio_Etapa
	FROM Requiere R
	INNER JOIN Obra O
	ON R.id_obra = O.id
	INNER JOIN Material M
	ON M.codigo = R.codigo_material
	INNER JOIN Etapa E
	ON E.id = R.id_etapa
	GROUP BY O.id, E.nombre;
END; $$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION Planilla() 
RETURNS TABLE (
	nombre_obra varchar (60),
	id int,
	nombre varchar(60),
	semana int,
	pago_semana float
)
AS $$
BEGIN
	RETURN QUERY
	SELECT	O.nombre_obra, O.id, E.nombre,L.semana,(E.pago_hora * L.horas_laboradas) as Pago_Semana
	FROM Labora_en L
	INNER JOIN Empleado E
	ON L.id_empleado = E.id
	INNER JOIN Obra O
	ON O.id = L.id_obra;
END;
$$ LANGUAGE 'plpgsql';	


