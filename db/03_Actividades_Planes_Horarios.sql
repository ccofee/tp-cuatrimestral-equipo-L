USE GIMNASIO_DB;
GO

--ACTIVIDADES
CREATE TABLE ACTIVIDADES (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(200) NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

INSERT INTO ACTIVIDADES (Nombre, Descripcion, Activo) VALUES
('Funcional', 'Entrenamiento de circuitos de fuerza y resistencia', 1),
('CrossFit', 'Entrenamiento funcional de alta intensidad', 1),
('Spinning', 'Ciclismo indoor grupal', 1),
('Stretching', 'Movilidad articular y elongacion', 1);
GO

--PLANES
ALTER TABLE PLANES
ADD IncluyeMusculacion BIT NOT NULL DEFAULT 1,
    IncluyeClases BIT NOT NULL DEFAULT 1;
GO

--HORARIOS
ALTER TABLE HORARIOS
ADD IdActividad INT NOT NULL DEFAULT 1,
    CONSTRAINT FK_HORARIOS_ACTIVIDADES FOREIGN KEY (IdActividad) REFERENCES ACTIVIDADES(Id);
GO