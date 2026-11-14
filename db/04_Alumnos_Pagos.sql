USE GIMNASIO_DB;
GO

-- Agrega fecha de registro en ALUMNOS para calcular antigüedad

ALTER TABLE ALUMNOS
ADD FechaAlta DATETIME NOT NULL DEFAULT GETDATE();
GO


-------------------------------------------------------
-- Creación de tabla PAGOS para trazabilidad de cuotas
-------------------------------------------------------------
CREATE TABLE PAGOS (
    Id          INT IDENTITY(1,1)   NOT NULL,
    IdAlumno    INT                 NOT NULL,
    FechaPago   DATETIME            NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_PAGOS PRIMARY KEY (Id),
    CONSTRAINT FK_PAGOS_ALUMNOS FOREIGN KEY (IdAlumno) REFERENCES ALUMNOS(Id)
);
GO


-- Datos de prueba iniciales vinculados a los alumnos del script 02
INSERT INTO PAGOS (IdAlumno, FechaPago) VALUES
    (1, DATEADD(DAY, -10, GETDATE())),
    (2, DATEADD(DAY, -15, GETDATE())),
    (3, DATEADD(DAY, -5,  GETDATE()));
GO