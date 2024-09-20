CREATE DATABASE IF NOT EXISTS infini;
use infini; 

CREATE TABLE IF NOT EXISTS Usuarios (
    nombreDeCuenta varchar(20) NOT NULL,
    nombreVisible varchar(20) NOT NULL,
    email varchar(50) NOT NULL,
    descripcion varchar(100),
    foto VARCHAR(150) NOT NULL,
    configuraciones varchar(200) NOT NULL,
    genero varchar(20) NOT NULL,
    fechaDeNacimiento DATE NOT NULL,
    estadoDeCuenta ENUM('publica', 'privada') NOT NULL DEFAULT 'publica',
    PRIMARY KEY (nombreDeCuenta),
    CHECK (email LIKE '%@%'),
    CHECK (fechaDeNacimiento < '2024-08-30')
    );


CREATE TABLE IF NOT EXISTS Grupos (
    nombreReal VARCHAR(20) NOT NULL,
    nombreVisible VARCHAR(20) NOT NULL,
    configuracion TinyText,
    descripcion VARCHAR(100),
    foto VARCHAR(100),
    PRIMARY KEY (nombreReal)
);

CREATE TABLE IF NOT EXISTS Eventos (
    idEvento INT NOT NULL AUTO_INCREMENT,
    titulo VARCHAR(20) NOT NULL,
    ubicacion VARCHAR(50),
    fechaYhora_Inicio DATETIME NOT NULL,
    fechaYhora_Final DATETIME NOT NULL,
    foto VARCHAR(100),
    descripcion VARCHAR(100),
    PRIMARY KEY (idEvento)
);


CREATE TABLE IF NOT EXISTS Posts (
    idPost INT NOT NULL AUTO_INCREMENT,
    nombreDeCuenta varchar(20) NOT NULL,
    texto VARCHAR(100),    
    video VARCHAR(50),
    imagen VARCHAR(100),
    categoria VARCHAR(100) NOT NULL,
    fechaYhora DATETIME,
    comentarios BOOL NOT NULL DEFAULT TRUE,
    PRIMARY KEY (idPost, nombreDeCuenta),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
    CHECK (
    (texto IS NOT NULL OR video IS NOT NULL OR imagen IS NOT NULL) -- Al menos uno debe tener un valor
    AND NOT (video IS NOT NULL AND imagen IS NOT NULL))-- No se permite video e imagen juntos 
);


CREATE TABLE IF NOT EXISTS PostPublico (
    idPost INT NOT NULL,
    nombreDeCuenta varchar(20) NOT NULL,
    PRIMARY KEY (idPost, nombreDeCuenta),
    FOREIGN KEY (nombreDeCuenta,idPost) REFERENCES Posts(nombreDeCuenta,idPost)
);

CREATE TABLE IF NOT EXISTS PostGrupo (
    idPost INT NOT NULL,
    nombreDeCuenta VARCHAR(20) NOT NULL,
    nombreReal VARCHAR(20) NOT NULL,
    PRIMARY KEY (idPost, nombreDeCuenta, nombreReal),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Posts(nombreDeCuenta),
    FOREIGN KEY (idPost) REFERENCES Posts(idPost),
    FOREIGN KEY (nombreReal) REFERENCES Grupos(nombreReal)
);

CREATE TABLE IF NOT EXISTS PostEvento(
    idPost INT NOT NULL,
    nombreDeCuenta VARCHAR(20) NOT NULL,
    idEvento INT NOT NULL,
    PRIMARY KEY (idPost, nombreDeCuenta, idEvento),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Posts(nombreDeCuenta),
    FOREIGN KEY (idPost) REFERENCES Posts(idPost),
    FOREIGN KEY (idEvento) REFERENCES Eventos(idEvento)
);

CREATE TABLE IF NOT EXISTS Comentarios (
    id int NOT NULL AUTO_INCREMENT,
    nombreDeCuenta varchar(20) NOT NULL,
    idPost int NOT NULL,
    nombreCreador VARCHAR(20) NOT NULL,
    texto varchar(100) NOT NULL,
    fechaYhora DATETIME NOT NULL,    
    PRIMARY KEY (id, nombreDeCuenta),
    FOREIGN KEY (idPost,nombreCreador) REFERENCES Posts(idPost,nombreDeCuenta),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta)
);



CREATE TABLE IF NOT EXISTS Ban (
    idBan INT NOT NULL AUTO_INCREMENT,
    nombreDeUsuario VARCHAR(20) NOT NULL,
    fechaInicio DATETIME NOT NULL,
    fechaFinalizacion DATETIME,
    PRIMARY KEY (idBan, nombreDeUsuario),
    FOREIGN KEY (nombreDeUsuario) REFERENCES Usuarios(nombreDeCuenta),
    CHECK (fechaFinalizacion > fechaInicio)
);

CREATE TABLE IF NOT EXISTS Reportes (
    numeroDeReporte INT NOT NULL AUTO_INCREMENT,
    nombreDeCuenta VARCHAR(20) NOT NULL,
    cuentaReporteUsuario VARCHAR(20),
    idPost INT,
    creadorDelPost VARCHAR(20),
    idComentario INT,
    creadorDelComentario VARCHAR(20),
    nombreGrupo VARCHAR(20),
    idEvento INT,
    tipo ENUM('Sexual', 'Violento o repugnante', 'Vejatorio', 'Hostigamiento o acoso', 'Actividades dañinas o peligrosas',
    'Desinformacion', 'Maltrato infantil', 'Terrorismo', 'Fraude', 'Problema legal', 'Otros') NOT NULL DEFAULT 'Terrorismo',
    descripcion TINYTEXT,
    PRIMARY KEY(numeroDeReporte, nombreDeCuenta),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
    FOREIGN KEY (cuentaReporteUsuario ) REFERENCES Usuarios(nombreDeCuenta),
    FOREIGN KEY (idPost, creadorDelPost) REFERENCES Posts(idPost, nombreDeCuenta),
    FOREIGN KEY (idComentario, creadorDelComentario) REFERENCES Comentarios(id, nombreDeCuenta),
    FOREIGN KEY (nombreGrupo) REFERENCES Grupos(nombreReal),
    FOREIGN KEY (idEvento) REFERENCES Eventos(idEvento),
    -- Solución 1: Check. Evita que no se inserten muchos datos, pero no garantiza que se inserte sólo uno
    CHECK ( 
		(cuentaReporteUsuario IS NOT NULL) + 
		(idPost IS NOT NULL AND creadorDelPost IS NOT NULL) + 
		(idComentario IS NOT NULL AND creadorDelComentario IS NOT NULL) + 
		(nombreGrupo IS NOT NULL) + 
		(idEvento IS NOT NULL) = 1 
		),
         CHECK (
        (cuentaReporteUsuario IS NULL OR cuentaReporteUsuario != nombreDeCuenta)
		)

);

-- Solución 2: Trigger. Más complejo y permite que sólo se inserte un dato.
-- DELIMITER //

-- CREATE TRIGGER check_uno_lleno
-- BEFORE INSERT ON Reportes
-- FOR EACH ROW
-- BEGIN
  --  DECLARE conteo INT DEFAULT 0;

    -- Contar cuántos campos no son nulos
    -- SET conteo = 
      --  (NEW.cuenta Report Usuario IS NOT NULL) +
      --  (NEW.idPost IS NOT NULL) +
      --  (NEW.creadorDelPost IS NOT NULL) +
      --  (NEW.idComentario IS NOT NULL) +
      --  (NEW.creadorDelComentario IS NOT NULL) +
      --  (NEW.nombreGrupo IS NOT NULL) +
      --  (NEW.idEvento IS NOT NULL);

    -- Si más de uno está lleno, se cancela la inserción
   -- IF conteo <> 1 THEN
    --    SIGNAL SQLSTATE '45000'
     --   SET MESSAGE_TEXT = 'Error: Sólo uno de los campos cuentaReporteUsuario, id Post, creadorDelPost, idComentario, creadorDelComentario, nombreGrupo, idEvento debe estar lleno.';
--    END IF;
-- END //

-- DELIMITER ;



CREATE TABLE IF NOT EXISTS Login (
    nombreDeCuenta VARCHAR(20) NOT NULL,
    contrasena VARCHAR(50) NOT NULL,
    PRIMARY KEY (nombreDeCuenta, contrasena)
);

CREATE TABLE IF NOT EXISTS Notificaciones(
	idNotificacion INT NOT NULL,
    tipo ENUM('recibeLike', 'etiquetado', 'nuevoMensaje', 'comentario', 'solicitud', 'video', 'configuracion', 'extras', 'baneo') NOT NULL,
    texto TINYTEXT NOT NULL,
    fechaYHora DATETIME NOT NULL,
    imagen VARCHAR(100) NOT NULL,
    nombreDeCuenta VARCHAR(20) NOT NULL,
    PRIMARY KEY (idNotificacion),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta)
);

CREATE TABLE IF NOT EXISTS Mensajes(
    idMensaje INT AUTO_INCREMENT NOT NULL,
    nombreDeCuenta VARCHAR(20) NOT NULL,
    nombreReal VARCHAR(20) NOT NULL,
    texto TINYTEXT NOT NULL,
    fechaYHora DATETIME NOT NULL,
    video TINYTEXT,
    imagen TINYTEXT,
    PRIMARY KEY (idMensaje),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
    FOREIGN KEY (nombreReal) REFERENCES Grupos(nombreReal)
);

-- Usuario - Usuario
CREATE TABLE IF NOT EXISTS Interactua(
    nombreDeCuenta VARCHAR(20)  NOT NULL,
    nombreDeCuenta2 VARCHAR(20) NOT NULL,
    tipoInteraccion ENUM('seguir', 'bloquear') NOT NULL DEFAULT 'seguir',
    PRIMARY KEY (nombreDeCuenta, nombreDeCuenta2),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
    FOREIGN KEY (nombreDeCuenta2) REFERENCES Usuarios(nombreDeCuenta)    
);

-- Usuario - Grupo
CREATE TABLE IF NOT EXISTS Participa(
    nombreDeCuenta VARCHAR(20) NOT NULL,
    nombreReal VARCHAR(20) NOT NULL,
    rol ENUM('usuario', 'admin', 'creador') DEFAULT 'usuario',
   PRIMARY KEY (nombreDeCuenta, nombreReal),
   FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
   FOREIGN KEY (nombreReal) REFERENCES Grupos(nombreReal)
);

DELIMITER //

CREATE TRIGGER validar_rol_participa
BEFORE INSERT ON Participa
FOR EACH ROW
BEGIN
    DECLARE total_usuarios INT;

    -- Contar cuántos usuarios ya están en el grupo
    SELECT COUNT(*) INTO total_usuarios
    FROM Participa
    WHERE nombreReal = NEW.nombreReal;

    -- Si ya hay más de 2 usuarios en el grupo, el rol no puede ser NULL
    IF total_usuarios > 1 AND NEW.rol IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El rol no puede ser nulo si hay más de 2 usuarios en el grupo.';
    END IF;

    -- Si hay 2 o menos usuarios, el rol debe ser NULL
    IF total_usuarios <= 1 THEN
        SET NEW.rol = NULL;
    END IF;
END //

DELIMITER ;


-- Usuario - Evento
CREATE TABLE IF NOT EXISTS ParticipaEvento(
 nombreDeCuenta VARCHAR(20) NOT NULL,
 idEvento INT NOT NULL, 
 rol ENUM('usuario', 'admin', 'creador') DEFAULT 'usuario',
PRIMARY KEY (nombreDeCuenta, idEvento),
FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
FOREIGN KEY (idEvento) REFERENCES Eventos(idEvento)
);



CREATE TABLE IF NOT EXISTS DaLike(
    nombreDeCuenta VARCHAR(20) NOT NULL,
    idPost INT NOT NULL, 
    nombredeCreador VARCHAR(20) NOT NULL,
    PRIMARY KEY (nombreDeCuenta, idPost, nombredeCreador),
    FOREIGN KEY (nombreDeCuenta) REFERENCES Usuarios(nombreDeCuenta),
    FOREIGN KEY (nombredeCreador,idPost) REFERENCES PostPublico(nombreDeCuenta,idPost)
);

CREATE TABLE IF NOT EXISTS DaLikeComentario(
    nombreDeCuenta VARCHAR(20) NOT NULL,
    idComentario INT NOT NULL, 
    quienDaLike VARCHAR(20) NOT NULL,
    PRIMARY KEY (nombreDeCuenta, idComentario, quienDaLike),
    FOREIGN KEY (nombreDeCuenta,idComentario) REFERENCES Comentarios(nombreDeCuenta,id),
    FOREIGN KEY (quienDaLike ) REFERENCES Usuarios(nombreDeCuenta)
);


-- Boceto de vistas.
CREATE VIEW ParticipaPropios AS
SELECT * FROM Participa
WHERE nombreDeCuenta = CURRENT_USER(); -- O el usuario actual según tu sistema.

CREATE VIEW ParticipaUsuarios AS
SELECT * FROM Participa 
WHERE rol = "usuario";

CREATE VIEW ComentariosPropios AS
SELECT * FROM Comentarios
WHERE nombreDeCuenta = CURRENT_USER(); -- O el usuario actual según tu sistema.

CREATE VIEW PostsPropios AS
SELECT * FROM Posts
WHERE nombreDeCuenta = CURRENT_USER(); -- O el usuario actual según tu sistema.

CREATE VIEW  ParticipaPropiosEventos AS
SELECT * FROM ParticipaEvento
WHERE nombreDeCuenta = CURRENT_USER(); -- O el usuario actual según tu sistema.




-- Roles

-- Administrador Backoffice
CREATE ROLE IF NOT EXISTS administrador_backoffice;

GRANT INSERT ON infini.Ban TO 'administrador_backoffice';  -- Solo puede insertar en la tabla Ban
GRANT SELECT ON infini.Usuarios TO 'administrador_backoffice';
GRANT SELECT ON infini.Grupos TO 'administrador_backoffice';
GRANT SELECT ON infini.Posts TO 'administrador_backoffice';
GRANT SELECT ON infini.PostPublico TO 'administrador_backoffice';
GRANT SELECT ON infini.PostGrupo TO 'administrador_backoffice';
GRANT SELECT ON infini.PostEvento TO 'administrador_backoffice';
GRANT SELECT ON infini.Comentarios TO 'administrador_backoffice';
GRANT SELECT ON infini.Reportes TO 'administrador_backoffice';
GRANT SELECT ON infini.Eventos TO 'administrador_backoffice';
GRANT SELECT ON infini.Participa TO 'administrador_backoffice';
GRANT SELECT ON infini.ParticipaEvento TO 'administrador_backoffice';

GRANT UPDATE ON infini.Grupos TO 'administrador_backoffice';
GRANT UPDATE ON infini.Posts TO 'administrador_backoffice';
GRANT UPDATE ON infini.PostPublico TO 'administrador_backoffice';
GRANT UPDATE ON infini.PostGrupo TO 'administrador_backoffice';
GRANT UPDATE ON infini.PostEvento TO 'administrador_backoffice';
GRANT UPDATE ON infini.Comentarios TO 'administrador_backoffice';
GRANT UPDATE ON infini.Reportes TO 'administrador_backoffice';
GRANT UPDATE ON infini.Eventos TO 'administrador_backoffice';
GRANT UPDATE ON infini.Participa TO 'administrador_backoffice';
GRANT UPDATE ON infini.ParticipaEvento TO 'administrador_backoffice';
GRANT UPDATE ON infini.Ban TO 'administrador_backoffice';

GRANT DELETE ON infini.Comentarios TO 'administrador_backoffice';
GRANT DELETE ON infini.Posts TO 'administrador_backoffice';
GRANT DELETE ON infini.PostPublico TO 'administrador_backoffice';
GRANT DELETE ON infini.PostGrupo TO 'administrador_backoffice';
GRANT DELETE ON infini.PostEvento TO 'administrador_backoffice';
GRANT DELETE ON infini.Grupos TO 'administrador_backoffice';
GRANT DELETE ON infini.Reportes TO 'administrador_backoffice';
GRANT DELETE ON infini.Eventos TO 'administrador_backoffice';
GRANT DELETE ON infini.Participa TO 'administrador_backoffice';
GRANT DELETE ON infini.ParticipaEvento TO 'administrador_backoffice';


-- Usuario
CREATE ROLE IF NOT EXISTS usuario;

GRANT SELECT ON infini.Usuarios TO 'usuario';
GRANT SELECT ON infini.Grupos TO 'usuario';
GRANT SELECT ON infini.Posts TO 'usuario';
GRANT SELECT ON infini.PostPublico TO 'usuario';
GRANT SELECT ON infini.PostGrupo TO 'usuario';
GRANT SELECT ON infini.PostEvento TO 'usuario';
GRANT SELECT ON infini.Comentarios TO 'usuario';
GRANT SELECT ON infini.Notificaciones TO 'usuario';
GRANT SELECT ON infini.DaLike TO 'usuario';
GRANT SELECT ON infini.DaLikeComentario TO 'usuario';
GRANT SELECT ON infini.Reportes TO 'usuario';
GRANT SELECT ON infini.Participa TO 'usuario';
GRANT SELECT ON infini.ParticipaEvento TO 'usuario';
GRANT SELECT ON infini.Mensajes TO 'usuario';

GRANT INSERT ON infini.Posts TO 'usuario';
GRANT INSERT ON infini.PostPublico TO 'usuario';
GRANT INSERT ON infini.PostGrupo TO 'usuario';
GRANT INSERT ON infini.PostEvento TO 'usuario';
GRANT INSERT ON infini.Comentarios TO 'usuario';
GRANT INSERT ON infini.Reportes TO 'usuario';
GRANT INSERT ON infini.Grupos TO 'usuario';
GRANT INSERT ON infini.Eventos TO 'usuario';
GRANT INSERT ON infini.Interactua TO 'usuario';
GRANT INSERT ON infini.Participa TO 'usuario';
GRANT INSERT ON infini.ParticipaEvento TO 'usuario';
GRANT INSERT ON infini.DaLike TO 'usuario';
GRANT INSERT ON infini.DaLikeComentario TO 'usuario';
GRANT INSERT ON infini.Notificaciones TO 'usuario';
GRANT INSERT ON infini.Mensajes TO 'usuario';
GRANT UPDATE ON infini.Posts TO 'usuario';
GRANT UPDATE ON infini.PostPublico TO 'usuario';
GRANT UPDATE ON infini.PostGrupo TO 'usuario';
GRANT UPDATE ON infini.PostEvento TO 'usuario';
GRANT UPDATE ON infini.Comentarios TO 'usuario';
GRANT UPDATE ON infini.Usuarios TO 'usuario';
GRANT UPDATE ON infini.DaLike TO 'usuario';
GRANT UPDATE ON infini.DaLikeComentario TO 'usuario';
GRANT UPDATE ON infini.Eventos TO 'usuario';

GRANT DELETE ON infini.PostsPropios TO 'usuario';
GRANT DELETE ON infini.ComentariosPropios TO 'usuario';
GRANT DELETE ON infini.ParticipaPropios TO 'usuario';
GRANT DELETE ON infini.ParticipaPropiosEventos TO 'usuario';
GRANT DELETE ON infini.Eventos TO 'usuario';



-- Dueño de Grupo
CREATE ROLE IF NOT EXISTS dueno_grupo;

GRANT SELECT ON infini.Grupos TO dueno_grupo;
GRANT SELECT ON infini.Eventos TO dueno_grupo;

GRANT INSERT ON infini.Eventos TO dueno_grupo;

GRANT UPDATE ON infini.Grupos TO dueno_grupo;
GRANT UPDATE ON infini.Eventos TO dueno_grupo;

GRANT DELETE ON infini.Grupos TO dueno_grupo;
GRANT DELETE ON infini.Eventos TO dueno_grupo;
GRANT DELETE ON infini.Participa TO dueno_grupo;


-- Administrador de Grupo
CREATE ROLE IF NOT EXISTS administrador_grupo;

GRANT SELECT ON infini.Grupos TO administrador_grupo;
GRANT SELECT ON infini.Eventos TO administrador_grupo;

GRANT INSERT ON infini.Eventos TO administrador_grupo;

GRANT UPDATE ON infini.Grupos TO administrador_grupo;
GRANT UPDATE ON infini.Eventos TO administrador_grupo;

GRANT DELETE ON infini.Eventos TO administrador_grupo;
GRANT DELETE ON infini.ParticipaUsuarios TO administrador_grupo;



-- Datos de ejemplo:

-- Insertar datos en Usuarios
INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta)
VALUES 
('juan123', 'Juan Pérez', 'juan.perez@email.com', 'Descripción de Juan', 'foto_juan.jpg', 'config1', 'Masculino', '1990-05-15', 'publica'),
('maria456', 'María López', 'maria.lopez@email.com', 'Descripción de María', 'foto_maria.jpg', 'config2', 'Femenino', '1985-11-23', 'publica'),
('luis789', 'Luis Fernández', 'luis.fernandez@email.com', 'Descripción de Luis', 'foto_luis.jpg', 'config3', 'Masculino', '1992-08-30', 'publica');

-- Insertar datos en Grupos
INSERT INTO Grupos (nombreReal, nombreVisible, configuracion, descripcion, foto)
VALUES 
('grupo1', 'Grupo Uno', 'Configuración del Grupo Uno', 'Descripción del Grupo Uno', 'foto_grupo1.jpg'),
('grupo2', 'Grupo Dos', 'Configuración del Grupo Dos', 'Descripción del Grupo Dos', 'foto_grupo2.jpg');

-- Insertar datos en Eventos
INSERT INTO Eventos (titulo, ubicacion, fechaYhora_Inicio, fechaYhora_Final, foto, descripcion)
VALUES 
('Evento A', 'Ubicación A', '2024-10-01 18:00:00', '2024-10-01 22:00:00', 'foto_evento_a.jpg', 'Descripción del Evento A'),
('Evento B', 'Ubicación B', '2024-11-15 09:00:00', '2024-11-15 17:00:00', 'foto_evento_b.jpg', 'Descripción del Evento B');

-- Insertar datos en Posts
INSERT INTO Posts (nombreDeCuenta, texto, video, imagen, categoria, fechaYhora, comentarios)
VALUES 
('juan123', 'Este es un post de Juan', NULL, 'imagen_juan.jpg', 'Categoría A', '2024-09-20 10:00:00', TRUE),
('maria456', 'Este es un post de María', NULL, 'imagen_maria.jpg', 'Categoría B', '2024-09-21 14:00:00', TRUE),
('maria456', 'Este post es nuevo', NULL, 'imagen_maria.jpg', 'Categoría B', '2024-08-21 14:00:00', TRUE),
('maria456', 'Este es un post reciente de María', NULL, 'imagen_nueva_maria.jpg', 'Categoría B', NOW(), TRUE);


-- Insertar datos en PostPublico
INSERT INTO PostPublico (idPost, nombreDeCuenta)
VALUES 
(1, 'juan123'),
(2, 'maria456'),
(4,'maria456');


-- Insertar datos en PostGrupo
INSERT INTO PostGrupo (idPost, nombreDeCuenta, nombreReal)
VALUES 
(1, 'juan123', 'grupo1'),
(2, 'maria456', 'grupo2');

-- Insertar datos en PostEvento
INSERT INTO PostEvento (idPost, nombreDeCuenta, idEvento)
VALUES 
(1, 'juan123', 1),
(2, 'maria456', 2);

-- Insertar datos en Comentarios
INSERT INTO Comentarios (nombreDeCuenta, idPost, nombreCreador, texto, fechaYhora)
VALUES ('maria456', 1, 'juan123', 'Este es un comentario de ejemplo.', '2024-09-15 12:30:00');
INSERT INTO Comentarios (nombreDeCuenta, idPost, nombreCreador, texto, fechaYhora)
VALUES ('maria456', 2, 'maria456', 'Segundo comentario','2024-09-15 12:30:00');
INSERT INTO Comentarios (nombreDeCuenta, idPost, nombreCreador, texto, fechaYhora)
VALUES ('maria456', 3, 'maria456', 'Comentrio','2024-09-15 12:30:00');



-- Insertar datos en Ban
INSERT INTO Ban (nombreDeUsuario, fechaInicio, fechaFinalizacion)
VALUES 
('juan123', '2024-09-01 00:00:00', '2024-09-30 23:59:59'),
('maria456', '2024-10-01 00:00:00', NULL);

-- Insertar datos en Reportes

-- Inserción con `cuentaReporteUsuario`
INSERT INTO Reportes (nombreDeCuenta, cuentaReporteUsuario, tipo, descripcion)
VALUES 
('juan123', 'maria456', 'Sexual', 'Post reportado por contenido sexual inapropiado');

-- Inserción con `idPost`
INSERT INTO Reportes (nombreDeCuenta, idPost, creadorDelPost, tipo, descripcion)
VALUES 
('maria456', 1, 'juan123','Violento o repugnante', 'Post reportado por contenido violento');

-- Inserción con `idComentario y creadorDelComentario`
INSERT INTO Reportes (nombreDeCuenta, idComentario, creadorDelComentario, tipo, descripcion)
VALUES 
('juan123', 1, 'maria456','Hostigamiento o acoso', 'Comentario reportado por acoso');

-- Inserción con `nombreGrupo`
INSERT INTO Reportes (nombreDeCuenta, nombreGrupo, tipo, descripcion)
VALUES 
('maria456', 'grupo1', 'Maltrato infantil', 'Post reportado por maltrato infantil');

-- Inserción con `idEvento`
INSERT INTO Reportes (nombreDeCuenta, idEvento, tipo, descripcion)
VALUES 
('juan123', 1, 'Terrorismo', 'Evento reportado por terrorismo');

-- Insertar datos en Login
INSERT INTO Login (nombreDeCuenta, contrasena)
VALUES 
('juan123', 'contrasena123'),
('maria456', 'contrasena456');

-- Insertar datos en Notificaciones
INSERT INTO Notificaciones (idNotificacion, tipo, texto, fechaYHora, imagen, nombreDeCuenta)
VALUES 
(1, 'nuevoMensaje', 'Tienes un nuevo mensaje de Juan', '2024-09-20 10:30:00', 'mensaje_notif.jpg', 'maria456'),
(2, 'recibeLike', 'Tu post recibió un nuevo like', '2024-09-21 14:30:00', 'like_notif.jpg', 'juan123');

-- Insertar datos en Interactua
INSERT INTO Interactua (nombreDeCuenta, nombreDeCuenta2, tipoInteraccion)
VALUES 
('juan123', 'maria456', 'seguir'),
('maria456', 'juan123', 'seguir');

-- Insertar datos en Participa
INSERT INTO Participa (nombreDeCuenta, nombreReal, rol)
VALUES 
('juan123', 'grupo1', 'creador'),
('maria456', 'grupo2', 'admin');

-- Insertar datos en ParticipaEvento
INSERT INTO ParticipaEvento (nombreDeCuenta, idEvento, rol)
VALUES 
('juan123', 1, 'creador'),
('maria456', 2, 'admin');

-- Insertar datos en DaLike
INSERT INTO DaLike (nombreDeCuenta, idPost, nombredeCreador)
VALUES 
('juan123', 2, 'maria456'),
('maria456', 1, 'juan123'),
('juan123', 4, 'maria456');

-- Insertar datos en DaLikeComentario
INSERT INTO DaLikeComentario (nombreDeCuenta, idComentario, quienDaLike)
VALUES 
('maria456', 1, 'juan123'),
('maria456', 1, 'maria456');


-- 1. Mostrar todos los posts de un usuario específico
SELECT *
FROM Posts
WHERE nombreDeCuenta = 'maria456';

-- 2. Mostrar los comentarios de un post específico
SELECT *
FROM Comentarios
WHERE idPost = 1;


-- 3. Obtener todos los eventos que sucederán en 30 días
SELECT *
FROM Eventos
WHERE fechaYhora_Inicio BETWEEN NOW() AND DATE_ADD(NOW(), INTERVAL 30 DAY);


-- 4. Obtener los últimos 5 posts publicados es un grupo específico
SELECT p.*
FROM Posts p
JOIN PostGrupo pg ON p.idPost = pg.idPost
WHERE pg.nombreReal = 'grupo1'
ORDER BY p.fechaYhora DESC
LIMIT 5;


-- 5. Ordenar descendentemente los post de los últimos 30 días según la cantidad de likes 
SELECT p.idPost, p.nombreDeCuenta, p.texto, COUNT(dl.nombreDeCuenta) AS cantidad_likes
FROM Posts p
LEFT JOIN DaLike dl ON p.idPost = dl.idPost AND p.nombreDeCuenta = dl.nombredeCreador
WHERE p.fechaYhora BETWEEN DATE_SUB(NOW(), INTERVAL 30 DAY) AND NOW()
GROUP BY p.idPost, p.nombreDeCuenta, p.texto
ORDER BY cantidad_likes DESC;


-- 6. Listar todos los grupos a los que pertenece un usuario
SELECT g.*
FROM Grupos g
JOIN Participa p ON g.nombreReal = p.nombreReal
WHERE p.nombreDeCuenta = 'maria456';



-- 7. Obtener la lista de los usuarios que le dieron like a un post específico
SELECT DISTINCT dl.nombreDeCuenta
FROM DaLike dl
WHERE dl.idPost = 1;


-- 8. Obtener todos los usuarios que participaron de un evento
SELECT DISTINCT p.nombreDeCuenta
FROM ParticipaEvento p
WHERE p.idEvento = 1;


-- 9. Mostrar todos los eventos en los que participó un usuario
SELECT e.*
FROM Eventos e
JOIN ParticipaEvento pe ON e.idEvento = pe.idEvento
WHERE pe.nombreDeCuenta = 'maria456';


-- 10. Mostrar los posts de un usuario y el número de comentarios que obtuvo. Ordenarlo descendentemente según la cantidad de comentarios
SELECT p.idPost, p.texto, COUNT(c.id) AS cantidad_comentarios
FROM Posts p
LEFT JOIN Comentarios c ON p.idPost = c.idPost AND p.nombreDeCuenta = c.nombreCreador
WHERE p.nombreDeCuenta = 'maria456'
GROUP BY p.idPost, p.texto
ORDER BY cantidad_comentarios DESC;




















