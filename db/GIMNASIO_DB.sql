
USE master
GO

IF DB_ID('GIMNASIO_DB') IS NOT NULL
BEGIN
    ALTER DATABASE GIMNASIO_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE GIMNASIO_DB
END
GO

CREATE DATABASE GIMNASIO_DB
GO

USE GIMNASIO_DB
GO

CREATE TABLE PLANES (
    Id              int IDENTITY(1,1)   NOT NULL,
    Descripcion     varchar(50)         NOT NULL,
    Precio          decimal(10,2)       NOT NULL,
    ClasesSemanales int                 NOT NULL,
    Activo          bit                 NOT NULL DEFAULT 1,
    CONSTRAINT PK_PLANES PRIMARY KEY (Id)
)
GO

CREATE TABLE HORARIOS (
    Id          int IDENTITY(1,1)   NOT NULL,
    DiaSemana   int                 NOT NULL,
    HoraInicio  time                NOT NULL,
    HoraFin     time                NOT NULL,
    CupoMaximo  int                 NOT NULL,
    Activo      bit                 NOT NULL DEFAULT 1,
    CONSTRAINT PK_HORARIOS PRIMARY KEY (Id)
)
GO

INSERT INTO PLANES (Descripcion, Precio, ClasesSemanales) VALUES
    ('Plan 2 dias', 18000.00, 2),
    ('Plan 3 dias', 24000.00, 3),
    ('Pase Libre',  32000.00, 5)
GO

INSERT INTO HORARIOS (DiaSemana, HoraInicio, HoraFin, CupoMaximo) VALUES
    (1, '18:00', '19:00', 15),
    (1, '19:00', '20:00', 15),
    (3, '18:00', '19:00', 15),
    (5, '18:00', '19:00', 20)
GO
