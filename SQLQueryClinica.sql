--CREAR BASE DE DATOS
create database DB_Clinica
on primary
(
	name=DB_Clinica_Data,
	filename='E:\SENATI\PlanSesion20204-1\ING-SOFT-SEMESTRE-IV\2-LP-3\ProyectoLP\DB\DB_Clinica_Data.mdf',
	size=5mb,
	maxsize=unlimited,
	filegrowth=10%
)
log on
(
	name=DB_Clinica_Log,
	filename='E:\SENATI\PlanSesion20204-1\ING-SOFT-SEMESTRE-IV\2-LP-3\ProyectoLP\DB\DB_Clinica_log.ldf',
	size=5mb,
	maxsize=unlimited,
	filegrowth=10%
)

USE DB_Clinica

--CREAR TABLAS

CREATE TABLE EECC
(
	EECCCodigo tinyint identity primary key not null,
	EECCDescripcion varchar(10)
)

INSERT EECC VALUES ('Soltero'),('Casado'),
('Viudo'),('Divorciado')

select * from EECC

CREATE TABLE Genero
(
	GeneroCCodigo tinyint identity primary key not null,
	GeneroDescripcion varchar(9)
)

insert Genero values ('Femenino'),('Masculino')

select * from Genero

create table Paciente
(
	PacienteCodigo varchar(8) primary key not null,
	PacienteNombre varchar(30),
	PacienteApellido varchar(50),
	PacienteDireccion varchar(150),
	PacienteTelefono int,
	PacienteNacimiento date,
	EECCCodigo tinyint foreign key(EECCCodigo) references EECC,
	GeneroCCodigo tinyint foreign key(GeneroCCodigo) references Genero
)

create table HHCC
(
	HHCCCodigo int identity (10000,2) primary key not null,
	HHCCFecha datetime,
	PacienteCodigo varchar(8) foreign key (PacienteCodigo) references Paciente
)

create table Especialidad
(
	EspecialidadCodigo int identity primary key not null,
	EspecialidadDescripcion varchar(50),
)

create table Especialista
(
	EspecialistaCMP int primary key not null,
	EspecialistaNombre varchar(30),
	EspecialistaApellido varchar(50),
	EspecialidadCodigo int foreign key (EspecialidadCodigo) references Especialidad
)

create table ActoMedico
(
	ActoMedicoCodigo int identity primary key not null,
	ActoMedicoFecha datetime,
	HHCCCodigo int foreign key (HHCCCodigo) references HHCC,
	EspecialistaCMP int foreign key (EspecialistaCMP) references Especialista,
	ActoMedicoObservacion varchar(250)
)

DROP TABLE ActoMedicoDetalle

create table ActoMedicoDetalle
(
	ActoMedicoCodigo int FOREIGN KEY (ActoMedicoCodigo) REFERENCES ActoMedico,
	fiebre float,
	tos bit,
	dolorCabeza bit,
	dolorMusculares bit,
	malestarGeneral bit,
	dolorGarganta bit,
	secreciónNasal bit
)

--PROCEDIMIENTO ALMACENADO

create proc usp_InsertarPaciente
(
	@PacienteCodigo varchar(8),
	@PacienteNombre varchar(30),
	@PacienteApellido varchar(50),
	@PacienteDireccion varchar(150),
	@PacienteTelefono int,
	@PacienteNacimiento date,
	@EECCCodigo tinyint,
	@GeneroCCodigo tinyint
)
as
begin
begin try
INSERT INTO [dbo].[Paciente]
           ([PacienteCodigo]
           ,[PacienteNombre]
           ,[PacienteApellido]
           ,[PacienteDireccion]
           ,[PacienteTelefono]
           ,[PacienteNacimiento]
           ,[EECCCodigo]
           ,[GeneroCCodigo])
     VALUES
           (@PacienteCodigo,@PacienteNombre,
	@PacienteApellido,@PacienteDireccion,
	@PacienteTelefono,@PacienteNacimiento,
	@EECCCodigo,@GeneroCCodigo)
end try
begin catch
	select ERROR_NUMBER(),ERROR_MESSAGE()
end catch
end

create proc usp_EliminarPaciente
(
	@PacienteCodigo varchar(8)
)
as
begin
begin try
DELETE FROM [dbo].[Paciente]
      WHERE PacienteCodigo=@PacienteCodigo 
end try
begin catch
	select ERROR_NUMBER(),ERROR_MESSAGE()
end catch
end

create proc usp_ActualizarPaciente
(
	@PacienteCodigo varchar(8),
	@PacienteNombre varchar(30),
	@PacienteApellido varchar(50),
	@PacienteDireccion varchar(150),
	@PacienteTelefono int,
	@PacienteNacimiento date,
	@EECCCodigo tinyint,
	@GeneroCCodigo tinyint
)
as
begin
begin try
UPDATE [dbo].[Paciente]
   SET
      [PacienteNombre] = @PacienteNombre
      ,[PacienteApellido] = @PacienteApellido
      ,[PacienteDireccion] = @PacienteDireccion
      ,[PacienteTelefono] = @PacienteTelefono
      ,[PacienteNacimiento] = @PacienteNacimiento
      ,[EECCCodigo] = @EECCCodigo
      ,[GeneroCCodigo] = @GeneroCCodigo
 WHERE [PacienteCodigo] = @PacienteCodigo
end try
begin catch
	select ERROR_NUMBER(),ERROR_MESSAGE()
end catch
end

create proc usp_ListarPacienteDNI
(
	@PacienteCodigo varchar(8)
)
as
begin
	begin try
	select * from Paciente
	WHERE [PacienteCodigo] = @PacienteCodigo
	end try
	begin catch
		select ERROR_NUMBER(),ERROR_MESSAGE()
	end catch
end


CREATE PROCEDURE usp_InsertarEspecialista
(
    @EspecialistaCMP int,
    @EspecialistaNombre varchar(30),
    @EspecialistaApellido varchar(50),
    @EspecialidadCodigo int
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Especialista (EspecialistaCMP, EspecialistaNombre, EspecialistaApellido, EspecialidadCodigo)
        VALUES (@EspecialistaCMP, @EspecialistaNombre, @EspecialistaApellido, @EspecialidadCodigo);
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;




CREATE PROCEDURE usp_EliminarEspecialista
(
    @EspecialistaCMP int
)
AS
BEGIN
    BEGIN TRY
        DELETE FROM Especialista WHERE EspecialistaCMP = @EspecialistaCMP;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;



CREATE PROCEDURE usp_ActualizarEspecialista
(
    @EspecialistaCMP int,
    @EspecialistaNombre varchar(30),
    @EspecialistaApellido varchar(50),
    @EspecialidadCodigo int
)
AS
BEGIN
    BEGIN TRY
        UPDATE Especialista
        SET EspecialistaNombre = @EspecialistaNombre,
            EspecialistaApellido = @EspecialistaApellido,
            EspecialidadCodigo = @EspecialidadCodigo
        WHERE EspecialistaCMP = @EspecialistaCMP;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;



CREATE PROCEDURE usp_ListarEspecialistas
AS
BEGIN
    BEGIN TRY
        SELECT * FROM Especialista;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;



CREATE PROCEDURE usp_ListarEspecialistaCMP
(
    @EspecialistaCMP int
)
AS
BEGIN
    BEGIN TRY
        SELECT * FROM Especialista WHERE EspecialistaCMP = @EspecialistaCMP;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;


INSERT INTO [dbo].[Especialista]
      ([EspecialistaCMP]
      ,[EspecialistaNombre]
      ,[EspecialistaApellido]
      ,[EspecialidadCodigo])
   VALUES
      (123426789, 'JUAN VIDAL', 'HURTADO', 1);


select * from Especialista

EXEC usp_ListarEspecialistas



CREATE PROCEDURE usp_InsertarEspecialista
(
    @EspecialistaCMP int,
    @EspecialistaNombre varchar(30),
    @EspecialistaApellido varchar(50),
    @EspecialidadCodigo int
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Especialista (EspecialistaCMP, EspecialistaNombre, EspecialistaApellido, EspecialidadCodigo)
        VALUES (@EspecialistaCMP, @EspecialistaNombre, @EspecialistaApellido, @EspecialidadCodigo);
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;




CREATE PROCEDURE usp_EliminarEspecialista
(
    @EspecialistaCMP int
)
AS
BEGIN
    BEGIN TRY
        DELETE FROM Especialista WHERE EspecialistaCMP = @EspecialistaCMP;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;



CREATE PROCEDURE usp_ActualizarEspecialista
(
    @EspecialistaCMP int,
    @EspecialistaNombre varchar(30),
    @EspecialistaApellido varchar(50),
    @EspecialidadCodigo int
)
AS
BEGIN
    BEGIN TRY
        UPDATE Especialista
        SET EspecialistaNombre = @EspecialistaNombre,
            EspecialistaApellido = @EspecialistaApellido,
            EspecialidadCodigo = @EspecialidadCodigo
        WHERE EspecialistaCMP = @EspecialistaCMP;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;



CREATE PROCEDURE usp_ListarEspecialistas
AS
BEGIN
    BEGIN TRY
        SELECT * FROM Especialista;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;



CREATE PROCEDURE usp_ListarEspecialistaCMP
(
    @EspecialistaCMP int
)
AS
BEGIN
    BEGIN TRY
        SELECT * FROM Especialista WHERE EspecialistaCMP = @EspecialistaCMP;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER(), ERROR_MESSAGE();
    END CATCH
END;


INSERT INTO [dbo].[Especialista]
      ([EspecialistaCMP]
      ,[EspecialistaNombre]
      ,[EspecialistaApellido]
      ,[EspecialidadCodigo])
   VALUES
      (123426789, 'JUAN VIDAL', 'HURTADO', 1);


select * from Especialista

EXEC usp_ListarEspecialistas



