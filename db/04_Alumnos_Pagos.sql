USE GIMNASIO_DB;
GO

-- Agrega fecha de registro en ALUMNOS para calcular antigüedad

ALTER TABLE ALUMNOS
ADD FechaAlta DATETIME NOT NULL DEFAULT GETDATE();
GO