USE GIMNASIO_DB;
GO

--USUARIOS

CREATE TABLE USUARIOS (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Clave VARCHAR(100) NOT NULL,
    Rol INT NOT NULL, -- 1- Admin, 2- Alumno
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

--ALUMNOS

CREATE TABLE ALUMNOS (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NULL, 
    DNI VARCHAR(20) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NULL,
    Telefono VARCHAR(20) NULL,
    FechaNacimiento DATE NULL,
    IdPlan INT NOT NULL,
    FechaVencimientoPlan DATE NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_ALUMNOS_USUARIOS FOREIGN KEY (IdUsuario) REFERENCES USUARIOS(Id),
    CONSTRAINT FK_ALUMNOS_PLANES FOREIGN KEY (IdPlan) REFERENCES PLANES(Id)
);
GO


-- ------------------------------------------------------------
-- DATOS DE PRUEBA: USUARIOS
-- Rol: 1 = Administrador, 2 = Alumno
-- ------------------------------------------------------------
INSERT INTO USUARIOS (Email, Clave, Rol, Nombre, Apellido, Activo) 
VALUES 
('admin@gimnasio.com', 'admin123', 1, 'Lucia',  'Benitez', 1),
('sofia@mail.com',     'sofia123', 2, 'Sofia',  'Ramirez', 1),
('diego@mail.com',     'diego123', 2, 'Diego',  'Lopez', 1);
GO

-- ------------------------------------------------------------
-- DATOS DE PRUEBA: ALUMNOS
-- ------------------------------------------------------------

INSERT INTO ALUMNOS (IdUsuario, DNI, Nombre, Apellido, Email, Telefono, FechaNacimiento, IdPlan, FechaVencimientoPlan, Activo) 
VALUES 
-- Alumno 1: Cuenta web vinculada (IdUsuario = 2)
(2, '38452109', 'Sofia',  'Ramirez', 'sofia@mail.com',  '3415550101', '1995-04-12', 1, '2026-11-30', 1),

-- Alumno 2: Caso sin cuenta web (IdUsuario = NULL) y sin Email ni teléfono (Probando nulos)
(NULL, '41203877', 'Martin', 'Gomez',   NULL,              NULL,         '2000-09-30', 2, '2026-11-15', 1),

-- Alumno 3: Caso sin cuenta web (IdUsuario = NULL) con Pase Libre
(NULL, '29887345', 'Carla',  'Ferreyra','carla@mail.com',  '3415550103', '1983-01-25', 3, '2026-12-05', 1),

-- Alumno 4: Cuenta web vinculada (IdUsuario = 3) con plan a punto de vencer
(3, '35123456', 'Diego',  'Lopez',   'diego@mail.com',  '3415550104', '1990-07-08', 1, '2026-11-05', 1),

-- Alumno 5: Caso sin cuenta web (IdUsuario = NULL) y sin fecha de nacimiento
(NULL, '43987654', 'Laura',  'Martinez','laura@mail.com',  '3415550105', NULL,         2, '2026-12-20', 1);
GO