USE GIMNASIO_DB;
GO

-- ============================================================
-- 1. PLANES: Baja logica de los 3 planes iniciales (script 01)
--    y alta de los 8 planes definitivos del gimnasio
-- ============================================================
UPDATE PLANES
SET Activo = 0
WHERE Id IN (1, 2, 3);
GO

INSERT INTO PLANES (Descripcion, Precio, ClasesSemanales, IncluyeMusculacion, IncluyeClases, Activo) VALUES
('Total Pass Musculacion',  45000.00, 0, 1, 0, 1),
('Total Pass Clases',       48000.00, 0, 0, 1, 1),
('Solo clases x2',          30000.00, 2, 0, 1, 1),
('Solo clases x3',          35000.00, 3, 0, 1, 1),
('Solo clases x5',          40000.00, 5, 0, 1, 1),
('Musculacion + Clases x2', 55000.00, 2, 1, 1, 1),
('Musculacion + Clases x3', 62000.00, 3, 1, 1, 1),
('Full Total Pass',         70000.00, 0, 1, 1, 1);
GO

-- ============================================================
-- 2. HORARIOS: Baja logica de los 4 iniciales (script 01)
--    y carga de los 50 horarios semanales (L-V 7 a 22, Sab 8 a 13)
--    Actividades (script 03): 1=Funcional, 2=CrossFit, 3=Spinning, 4=Stretching
-- ============================================================
UPDATE HORARIOS
SET Activo = 0
WHERE Id IN (1, 2, 3, 4);
GO

-- TURNO MANANA (Lunes a Viernes)
INSERT INTO HORARIOS (DiaSemana, HoraInicio, HoraFin, CupoMaximo, Activo, IdActividad) VALUES
-- CrossFit: Martes y Jueves 8, 9, 10 y 11 hs (Cupo 10)
(2, '08:00', '09:00', 10, 1, 2),
(2, '09:00', '10:00', 10, 1, 2),
(2, '10:00', '11:00', 10, 1, 2),
(2, '11:00', '12:00', 10, 1, 2),
(4, '08:00', '09:00', 10, 1, 2),
(4, '09:00', '10:00', 10, 1, 2),
(4, '10:00', '11:00', 10, 1, 2),
(4, '11:00', '12:00', 10, 1, 2),

-- Funcional: Martes y Jueves 10 y 11 hs (Cupo 10)
(2, '10:00', '11:00', 10, 1, 1),
(2, '11:00', '12:00', 10, 1, 1),
(4, '10:00', '11:00', 10, 1, 1),
(4, '11:00', '12:00', 10, 1, 1),

-- Stretching: Lunes, Martes y Jueves 7 y 8 hs (Cupo 15)
(1, '07:00', '08:00', 15, 1, 4),
(1, '08:00', '09:00', 15, 1, 4),
(2, '07:00', '08:00', 15, 1, 4),
(2, '08:00', '09:00', 15, 1, 4),
(4, '07:00', '08:00', 15, 1, 4),
(4, '08:00', '09:00', 15, 1, 4),

-- Spinning: Lunes, Miercoles y Viernes 9 hs (Cupo 15)
(1, '09:00', '10:00', 15, 1, 3),
(3, '09:00', '10:00', 15, 1, 3),
(5, '09:00', '10:00', 15, 1, 3);
GO

-- TURNO TARDE (Lunes a Viernes)
INSERT INTO HORARIOS (DiaSemana, HoraInicio, HoraFin, CupoMaximo, Activo, IdActividad) VALUES
-- CrossFit: Lunes, Miercoles y Viernes 19, 20 y 21 hs (Cupo 10)
(1, '19:00', '20:00', 10, 1, 2),
(1, '20:00', '21:00', 10, 1, 2),
(1, '21:00', '22:00', 10, 1, 2),
(3, '19:00', '20:00', 10, 1, 2),
(3, '20:00', '21:00', 10, 1, 2),
(3, '21:00', '22:00', 10, 1, 2),
(5, '19:00', '20:00', 10, 1, 2),
(5, '20:00', '21:00', 10, 1, 2),
(5, '21:00', '22:00', 10, 1, 2),

-- Funcional: Martes y Jueves 17, 18 y 19 hs (Cupo 10)
(2, '17:00', '18:00', 10, 1, 1),
(2, '18:00', '19:00', 10, 1, 1),
(2, '19:00', '20:00', 10, 1, 1),
(4, '17:00', '18:00', 10, 1, 1),
(4, '18:00', '19:00', 10, 1, 1),
(4, '19:00', '20:00', 10, 1, 1),

-- Stretching: Miercoles y Jueves 18, 19 y 20 hs (Cupo 15)
(3, '18:00', '19:00', 15, 1, 4),
(3, '19:00', '20:00', 15, 1, 4),
(3, '20:00', '21:00', 15, 1, 4),
(4, '18:00', '19:00', 15, 1, 4),
(4, '19:00', '20:00', 15, 1, 4),
(4, '20:00', '21:00', 15, 1, 4),

-- Spinning: Martes y Jueves 20 y 21 hs (Cupo 15)
(2, '20:00', '21:00', 15, 1, 3),
(2, '21:00', '22:00', 15, 1, 3),
(4, '20:00', '21:00', 15, 1, 3),
(4, '21:00', '22:00', 15, 1, 3);
GO

-- SABADOS (8:00 a 13:00)
INSERT INTO HORARIOS (DiaSemana, HoraInicio, HoraFin, CupoMaximo, Activo, IdActividad) VALUES
-- Funcional: Sabados 9 y 10 hs (Cupo 10)
(6, '09:00', '10:00', 10, 1, 1),
(6, '10:00', '11:00', 10, 1, 1),

-- CrossFit: Sabados 10 y 11 hs (Cupo 10)
(6, '10:00', '11:00', 10, 1, 2),
(6, '11:00', '12:00', 10, 1, 2);
GO

-- ============================================================
-- 3. USUARIOS: Alta de cuentas adicionales (se mantienen Ids 1 a 3 del script 02)
-- ============================================================
INSERT INTO USUARIOS (Email, Clave, Rol, Nombre, Apellido, Activo, DebeCambiarClave) VALUES
('recepcion@gimnasio.com', 'admin123',    1, 'Marcos',    'Vega',    1, 0),
('valentina@mail.com',     'valen123',    2, 'Valentina', 'Rios',    1, 0),
('lucas@mail.com',         'lucas123',    2, 'Lucas',     'Pereyra', 1, 0),
('tomas@mail.com',         'tomas123',    2, 'Tomas',     'Herrera', 1, 0),
('julieta@mail.com',       'temp1234',    2, 'Julieta',   'Castro',  1, 1),
('camila@mail.com',        'camila123',   2, 'Camila',    'Suarez',  1, 0),
('nicolas@mail.com',       'nico123',     2, 'Nicolas',   'Mendoza', 1, 0),
('federico@mail.com',      'fede123',     2, 'Federico',  'Molina',  1, 0),
('sinficha@mail.com',      'sinficha123', 2, 'Bruno',     'Paredes', 1, 0),
('renata@mail.com',        'renata123',   2, 'Renata',    'Gimenez',   1, 0),
('ignacio@mail.com',       'nacho123',    2, 'Ignacio',   'Dominguez', 1, 0);
GO


-- ============================================================
-- 4. ALUMNOS: Actualizacion de los 5 alumnos del script 02
--    (se reasignan a planes activos y fechas de antiguedad reales)
-- ============================================================
-- Sofia Ramirez: Musculacion + Clases x3 | Vence 30/11 (proximo a vencer el 25/11) | 9 meses
UPDATE ALUMNOS
SET IdPlan = (SELECT Id FROM PLANES WHERE Descripcion = 'Musculacion + Clases x3' AND Activo = 1),
    FechaVencimientoPlan = '2026-11-30',
    FechaAlta = '20260215'
WHERE DNI = '38452109';

-- Martin Gomez: Total Pass Musculacion | Atrasado (15/11) | 1 mes | Email/Tel NULL
UPDATE ALUMNOS
SET IdPlan = (SELECT Id FROM PLANES WHERE Descripcion = 'Total Pass Musculacion' AND Activo = 1),
    FechaVencimientoPlan = '2026-11-15',
    FechaAlta = '20261010'
WHERE DNI = '41203877';

-- Carla Ferreyra: Full Total Pass | Al dia | Sin cuenta web (lista para vincular) | 11 meses
UPDATE ALUMNOS
SET IdPlan = (SELECT Id FROM PLANES WHERE Descripcion = 'Full Total Pass' AND Activo = 1),
    FechaVencimientoPlan = '2026-12-18',
    FechaAlta = '20251210'
WHERE DNI = '29887345';

-- Diego Lopez: Solo clases x2 | Atrasado (05/11) | Con usuario web | 7 meses
UPDATE ALUMNOS
SET IdPlan = (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x2' AND Activo = 1),
    FechaVencimientoPlan = '2026-11-05',
    FechaAlta = '20260405'
WHERE DNI = '35123456';

-- Laura Martinez: Solo clases x3 | Al dia | Sin cuenta web | FechaNacimiento NULL | 3 meses
UPDATE ALUMNOS
SET IdPlan = (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x3' AND Activo = 1),
    FechaVencimientoPlan = '2026-12-20',
    FechaAlta = '20260812'
WHERE DNI = '43987654';
GO

-- Alta de 16 alumnos nuevos cubriendo los 8 planes, todos los estados y casos NULL
INSERT INTO ALUMNOS (IdUsuario, DNI, Nombre, Apellido, Email, Telefono, FechaNacimiento, IdPlan, FechaVencimientoPlan, Activo, FechaAlta) VALUES
-- Valentina Rios: Full Total Pass | Al dia | 14 meses (Fidelizacion)
((SELECT Id FROM USUARIOS WHERE Email = 'valentina@mail.com'), '37451209', 'Valentina', 'Rios',    'valentina@mail.com', '3415551101', '1993-08-14',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Full Total Pass' AND Activo = 1), '2026-12-22', 1, '20250910'),

-- Lucas Pereyra: Solo clases x2 | Al dia | 4 meses (tope 2/2 esta semana)
((SELECT Id FROM USUARIOS WHERE Email = 'lucas@mail.com'),     '40112344', 'Lucas',     'Pereyra', 'lucas@mail.com',     '3415551102', '1997-02-20',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x2' AND Activo = 1), '2026-12-15', 1, '20260710'),

-- Tomas Herrera: Total Pass Musculacion | Al dia | 2 meses (sin permiso para clases)
((SELECT Id FROM USUARIOS WHERE Email = 'tomas@mail.com'),     '42334519', 'Tomas',     'Herrera', 'tomas@mail.com',     '3415551103', '2001-06-18',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Total Pass Musculacion' AND Activo = 1), '2026-12-19', 1, '20260915'),

-- Julieta Castro: Musculacion + Clases x3 | Al dia | < 1 mes | DebeCambiarClave = 1
((SELECT Id FROM USUARIOS WHERE Email = 'julieta@mail.com'),   '39881204', 'Julieta',   'Castro',  'julieta@mail.com',   '3415551104', '1996-09-27',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Musculacion + Clases x3' AND Activo = 1), '2026-12-24', 1, '20261115'),

-- Camila Suarez: Solo clases x3 | Al dia | 8 meses | 1ra en lista de espera
((SELECT Id FROM USUARIOS WHERE Email = 'camila@mail.com'),    '36998712', 'Camila',    'Suarez',  'camila@mail.com',    '3415551105', '1992-11-03',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x3' AND Activo = 1), '2026-12-21', 1, '20260310'),

-- Nicolas Mendoza: Musculacion + Clases x2 | Al dia | 6 meses | 2do en lista de espera
((SELECT Id FROM USUARIOS WHERE Email = 'nicolas@mail.com'),   '38710452', 'Nicolas',   'Mendoza', 'nicolas@mail.com',   '3415551106', '1994-05-22',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Musculacion + Clases x2' AND Activo = 1), '2026-12-17', 1, '20260510'),

-- Federico Molina: Full Total Pass | Al dia | 10 meses | Confirmado en turno lleno
((SELECT Id FROM USUARIOS WHERE Email = 'federico@mail.com'),  '35891230', 'Federico',  'Molina',  'federico@mail.com',  '3415551107', '1991-01-19',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Full Total Pass' AND Activo = 1), '2026-12-28', 1, '20260112'),

-- Florencia Diaz: Solo clases x5 | Proximo a vencer (27/11) | Sin cuenta web | 3 meses
(NULL, '43776120', 'Florencia', 'Diaz',    'flordiaz@mail.com',        '3415551108', '2002-04-09',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x5' AND Activo = 1), '2026-11-27', 1, '20260818'),

-- Mateo Benitez: Solo clases x5 | Al dia | 12 meses | Email y Telefono NULL
(NULL, '31445980', 'Mateo',     'Benitez', NULL,                       NULL,         '1985-03-11',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x5' AND Activo = 1), '2026-12-26', 1, '20251105'),

-- Gonzalo Acosta: Musculacion + Clases x2 | Atrasado (10/11) | Telefono NULL | 5 meses
(NULL, '34220918', 'Gonzalo',   'Acosta',  'gonza.acosta@mail.com',    NULL,         '1989-12-01',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Musculacion + Clases x2' AND Activo = 1), '2026-11-10', 1, '20260615'),

-- Agustina Romero: Solo clases x5 | Al dia | 4 meses | Sin cuenta web
(NULL, '41654321', 'Agustina',  'Romero',  'agustina.romero@mail.com', '3415551111', '1998-07-30',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Solo clases x5' AND Activo = 1), '2026-12-23', 1, '20260718'),

-- Joaquin Navarro: Musculacion + Clases x3 | Proximo a vencer (28/11) | Sin cuenta web | 2 meses
(NULL, '39234567', 'Joaquin',   'Navarro', 'joaquin.navarro@mail.com', '3415551112', '1995-10-14',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Musculacion + Clases x3' AND Activo = 1), '2026-11-28', 1, '20260920'),

-- Milagros Silva: Full Total Pass | Al dia | < 1 mes | FechaNacimiento NULL
(NULL, '44112233', 'Milagros',  'Silva',   'milagros.silva@mail.com',  '3415551113', NULL,
 (SELECT Id FROM PLANES WHERE Descripcion = 'Full Total Pass' AND Activo = 1), '2026-12-27', 1, '20261113'),

-- Renata Gimenez: Total Pass Clases | Al dia (25/12) | 8 meses de antiguedad
((SELECT Id FROM USUARIOS WHERE Email = 'renata@mail.com'),  '36541289', 'Renata',  'Gimenez',   'renata@mail.com',  '3415551114', '1992-03-18',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Total Pass Clases' AND Activo = 1), '2026-12-25', 1, '20260320'),

-- Ignacio Dominguez: Total Pass Clases | Proximo a vencer (29/11) | 4 meses
((SELECT Id FROM USUARIOS WHERE Email = 'ignacio@mail.com'), '40887766', 'Ignacio', 'Dominguez', 'ignacio@mail.com', '3415551115', '1998-11-09',
 (SELECT Id FROM PLANES WHERE Descripcion = 'Total Pass Clases' AND Activo = 1), '2026-11-29', 1, '20260705'),

-- Paula Sosa: Total Pass Clases | Sin cuenta web | Al dia (19/12) | FechaNacimiento NULL
(NULL, '33778899', 'Paula', 'Sosa', 'paula.sosa@mail.com', '3415551116', NULL,
 (SELECT Id FROM PLANES WHERE Descripcion = 'Total Pass Clases' AND Activo = 1), '2026-12-19', 1, '20260910');
GO

-- ============================================================
-- 5. PAGOS: Historial adicional de cuotas cobradas
-- ============================================================
INSERT INTO PAGOS (IdAlumno, FechaPago) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '37451209'), DATEADD(DAY, -32, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '37451209'), DATEADD(DAY, -2,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '40112344'), DATEADD(DAY, -8,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '42334519'), DATEADD(DAY, -4,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '39881204'), DATEADD(DAY, -8,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '36998712'), DATEADD(DAY, -3,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '38710452'), DATEADD(DAY, -6,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '35891230'), DATEADD(DAY, -35, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '35891230'), DATEADD(DAY, -5,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '43776120'), DATEADD(DAY, -26, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '31445980'), DATEADD(DAY, -4,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '34220918'), DATEADD(DAY, -43, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '41654321'), DATEADD(DAY, -1,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '39234567'), DATEADD(DAY, -25, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '44112233'), DATEADD(DAY, -10, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '36541289'), DATEADD(DAY, -5,  GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '40887766'), DATEADD(DAY, -24, GETDATE())),
((SELECT Id FROM ALUMNOS WHERE DNI = '33778899'), DATEADD(DAY, -11, GETDATE()));
GO

-- ============================================================
-- 6. RESERVAS (Semana del Lunes 23/11 al Sabado 28/11/2026)
-- ============================================================

-- A) CLASE LLENA (10/10) + 2 EN LISTA DE ESPERA
--    CrossFit - Viernes 27/11/2026 - 19:00 a 20:00 (Cupo 10)
INSERT INTO RESERVAS (IdAlumno, IdHorario, FechaClase, Estado, FechaAlta) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '35891230'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T10:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '38452109'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T10:15:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '37451209'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T10:30:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '29887345'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T11:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '43987654'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T11:20:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '43776120'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T12:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '31445980'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T14:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '41654321'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T15:30:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '39234567'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T17:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '44112233'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'Reservada', '2026-11-22T18:10:00'),
-- Lista de espera ordenada por FechaAlta: 1ra Camila Suarez, 2do Nicolas Mendoza
((SELECT Id FROM ALUMNOS WHERE DNI = '36998712'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'EnEspera',  '2026-11-23T09:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '38710452'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 5 AND HoraInicio = '19:00'), '2026-11-27', 'EnEspera',  '2026-11-23T10:30:00');
GO

-- B) CLASE CASI LLENA (9/10 - queda exactamente 1 lugar libre)
--    Funcional - Jueves 26/11/2026 - 18:00 a 19:00 (Cupo 10)
INSERT INTO RESERVAS (IdAlumno, IdHorario, FechaClase, Estado, FechaAlta) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '37451209'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T09:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '29887345'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T09:30:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '35891230'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T10:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '44112233'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T10:45:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '43987654'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T11:15:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '31445980'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T13:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '41654321'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T14:20:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '39234567'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T16:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '43776120'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 1 AND DiaSemana = 4 AND HoraInicio = '18:00'), '2026-11-26', 'Reservada', '2026-11-22T17:40:00');
GO

-- C) CLASE CON BUEN MARGEN (3/15) + DOBLE CLASE EL MISMO DIA (Valentina Rios)
--    Spinning - Jueves 26/11/2026 - 20:00 a 21:00 (Cupo 15)
INSERT INTO RESERVAS (IdAlumno, IdHorario, FechaClase, Estado, FechaAlta) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '37451209'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 3 AND DiaSemana = 4 AND HoraInicio = '20:00'), '2026-11-26', 'Reservada', '2026-11-22T18:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '31445980'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 3 AND DiaSemana = 4 AND HoraInicio = '20:00'), '2026-11-26', 'Reservada', '2026-11-22T18:30:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '41654321'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 3 AND DiaSemana = 4 AND HoraInicio = '20:00'), '2026-11-26', 'Reservada', '2026-11-22T19:00:00');
GO

-- D) CLASE DE SABADO CON MARGEN (2/10)
--    CrossFit - Sabado 28/11/2026 - 10:00 a 11:00 (Cupo 10)
INSERT INTO RESERVAS (IdAlumno, IdHorario, FechaClase, Estado, FechaAlta) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '35891230'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 6 AND HoraInicio = '10:00'), '2026-11-28', 'Reservada', '2026-11-22T20:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '44112233'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 2 AND DiaSemana = 6 AND HoraInicio = '10:00'), '2026-11-28', 'Reservada', '2026-11-22T20:15:00');
GO

-- E) SOCIO EN EL LIMITE SEMANAL (Lucas Pereyra - Solo clases x2: ya tiene 2/2 esta semana)
--    1) Stretching Miercoles 25/11 19:00 (sirve tambien para regla de 2 hs el dia del checkpoint)
--    2) Spinning Viernes 27/11 09:00
INSERT INTO RESERVAS (IdAlumno, IdHorario, FechaClase, Estado, FechaAlta) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '40112344'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 4 AND DiaSemana = 3 AND HoraInicio = '19:00'), '2026-11-25', 'Reservada', '2026-11-21T15:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '40112344'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 3 AND DiaSemana = 5 AND HoraInicio = '09:00'), '2026-11-27', 'Reservada', '2026-11-21T15:05:00');
GO

-- F) HISTORIAL: Reserva ya pasada (Lunes 23/11 08:00) y Reserva ya cancelada
INSERT INTO RESERVAS (IdAlumno, IdHorario, FechaClase, Estado, FechaAlta) VALUES
((SELECT Id FROM ALUMNOS WHERE DNI = '38452109'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 4 AND DiaSemana = 1 AND HoraInicio = '08:00'), '2026-11-23', 'Reservada', '2026-11-20T12:00:00'),
((SELECT Id FROM ALUMNOS WHERE DNI = '29887345'), (SELECT Id FROM HORARIOS WHERE Activo = 1 AND IdActividad = 3 AND DiaSemana = 1 AND HoraInicio = '09:00'), '2026-11-23', 'Cancelada', '2026-11-20T14:00:00');
GO