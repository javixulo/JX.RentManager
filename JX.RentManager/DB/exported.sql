--
--     File generated with SQLiteStudio v3.0.6 on lun ago 3 19:58:02 2015
--
--     Text encoding used: windows-1252
--
PRAGMA foreign_keys = off;
BEGIN  TRANSACTION;

--     Table: Fiador
DROP   TABLE IF EXISTS Fiador;
CREATE TABLE Fiador
(
	   dni_cif TEXT PRIMARY KEY,
	   nombre TEXT NOT NULL,
	   direccion TEXT NOT NULL,
	   telefono_1 TEXT NOT NULL,
	   telefono_2 TEXT,
	   email TEXT,
	   estado TEXT DEFAULT "DADO DE ALTA" CHECK (estado="DADO DE ALTA" OR estado="DADO DE BAJA"),
	   fecha_alta DATE DEFAULT CURRENT_DATE,
	   observaciones	TEXT
)

--     Table: Inquilino
DROP   TABLE IF EXISTS Inquilino;
CREATE TABLE Inquilino
(
	   dni_cif TEXT PRIMARY KEY,
	   nombre TEXT NOT NULL, direccion TEXT NOT NULL,
	   telefono_1 TEXT NOT NULL,
	   telefono_2 TEXT,
	   email TEXT,
	   estado TEXT DEFAULT "DADO DE ALTA" CHECK (estado="DADO DE ALTA" OR estado="DADO DE BAJA"),
	   dia_pago INTEGER CHECK(dia_pago>0 AND dia_pago<32),
	   fecha_alta DATE DEFAULT CURRENT_DATE,
	   observaciones	TEXT
)

--     Table: Propietario
DROP   TABLE IF EXISTS Propietario;
CREATE TABLE Propietario
(
	   dni_cif TEXT PRIMARY KEY,
	   nombre TEXT NOT NULL,
	   direccion TEXT NOT NULL,
	   telefono_1 TEXT NOT NULL,
	   telefono_2 TEXT, email TEXT,
	   anagrama TEXT NOT NULL, firma TEXT NOT NULL, estado TEXT DEFAULT "DADO DE ALTA" CHECK (estado="DADO DE ALTA" OR estado="DADO DE BAJA"), fecha_alta DATE DEFAULT CURRENT_DATE, observaciones	TEXT
)

--     Table: Inmueble
DROP   TABLE IF EXISTS Inmueble;
CREATE TABLE Inmueble
(
	   id INTEGER PRIMARY KEY AUTOINCREMENT,
	   nombre TEXT NOT NULL,
	   direccion TEXT UNIQUE NOT NULL,
	   tipo TEXT NOT NULL CHECK(tipo="VIVIENDA" OR tipo="BAJO COMERCIAL" OR tipo="NAVE INDUSTRIAL" OR tipo="SALON DE CELEBRACIONES" OR tipo="OTRO"),
	   gastos_comunitarios REAL,
	   referencia_catastral	TEXT NOT NULL,
	   ano_construccion INTEGER,
	   ano_compra INTEGER,
	   hipoteca REAL,
	   telefono REAL,
	   agua REAL,
	   electricidad REAL,
	   jardineria REAL,
	   gastos_varios REAL,
	   plaza_parking REAL,
	   parcela TEXT,
	   metros_construidos TEXT,
	   metros_utiles TEXT,
	   observaciones TEXT,
	   fecha_alta DATE DEFAULT CURRENT_DATE
)


--     Table: CuentaCorrienteFiador
DROP   TABLE IF EXISTS CuentaCorrienteFiador;
CREATE TABLE CuentaCorrienteFiador
(      numero TEXT NOT NULL, fiador TEXT REFERENCES Fiador(dni_cif) ON DELETE CASCADE, PRIMARY KEY(numero, fiador)
)
INSERT INTO CuentaCorrienteFiador (numero, fiador) VALUES ('12345678901234567890', 'f');

--     Table: Contrato
DROP   TABLE IF EXISTS Contrato;
CREATE TABLE Contrato
(      id INTEGER PRIMARY KEY AUTOINCREMENT, codigo TEXT UNIQUE NOT NULL, inmueble INTEGER NOT NULL REFERENCES Inmueble(id) ON DELETE CASCADE, inquilino TEXT NOT NULL REFERENCES Inquilino(dni_cif) ON DELETE CASCADE, fiador TEXT REFERENCES Fiador(dni_cif) ON DELETE CASCADE, valor REAL NOT NULL, tipo TEXT NOT NULL CHECK (tipo="VENTA" OR tipo="ALQUILER"), fecha_alta DATE DEFAULT CURRENT_DATE, fecha_caducidad DATE NOT NULL, fecha_rev_renta DATE NOT NULL, fianza REAL NOT NULL, meses_sin_renta INTEGER NOT NULL DEFAULT 0, forma_pago TEXT NOT NULL CHECK(forma_pago="PAGARE" OR forma_pago="TRANSFERENCIA BANCARIA" OR forma_pago="GIRO DOMICILIADO" OR forma_pago="EFECTIVO"), estado TEXT NOT NULL CHECK(estado="ACTIVO" OR estado="DADO DE BAJA" OR estado="VENCIDO" OR estado="INTERRUMPIDO") DEFAULT "ACTIVO", poliza_seguro TEXT, compania_seguro TEXT, capital_continente REAL, franquicia TEXT, fecha_vencimiento_seguro	DATE, observaciones_seguro TEXT, copia_local TEXT, observaciones TEXT
)

--     Table: CuentaCorrientePropietario
DROP   TABLE IF EXISTS CuentaCorrientePropietario;
CREATE TABLE CuentaCorrientePropietario (numero TEXT NOT NULL, propietario TEXT REFERENCES Propietario (dni_cif) ON DELETE CASCADE, PRIMARY KEY (numero, propietario))
INSERT INTO CuentaCorrientePropietario (numero, propietario) VALUES ('123456', '23051886K');

--     Table: Recibo
DROP   TABLE IF EXISTS Recibo;
CREATE TABLE Recibo
(      id INTEGER PRIMARY KEY AUTOINCREMENT, numero INTEGER UNIQUE NOT NULL, estado TEXT NOT NULL DEFAULT "PENDIENTE DE PAGO" CHECK(estado ="PENDIENTE DE PAGO" OR estado="PARCIALMENTE PAGADO" OR estado="LIQUIDADO"), propietario TEXT NOT NULL REFERENCES Propietario(dni_cif) ON DELETE CASCADE, contrato INTEGER NOT NULL REFERENCES Contrato(id) ON DELETE CASCADE, renta REAL NOT NULL, IVA REAL NOT NULL, IRPF REAL NOT NULL, suplidos REAL, descuento_temp	REAL, importe REAL NOT NULL, resto_por_pagar	REAL, observaciones	TEXT, fecha_emision	DATE NOT NULL
)

--     Table: CuentaCorrienteInquilino
DROP   TABLE IF EXISTS CuentaCorrienteInquilino;
CREATE TABLE CuentaCorrienteInquilino
(      numero TEXT NOT NULL, inquilino	TEXT REFERENCES Inquilino(dni_cif) ON DELETE CASCADE, PRIMARY KEY(numero, inquilino)
)
INSERT INTO CuentaCorrienteInquilino (numero, inquilino) VALUES ('123456789', '22222222X');
INSERT INTO CuentaCorrienteInquilino (numero, inquilino) VALUES ('11111111111111', '33333333X');
INSERT INTO CuentaCorrienteInquilino (numero, inquilino) VALUES ('2222222222', '33333333X');
INSERT INTO CuentaCorrienteInquilino (numero, inquilino) VALUES ('33333333333333', '33333333X');

--     Table: Pago
DROP   TABLE IF EXISTS Pago;
CREATE TABLE Pago
(      id INTEGER PRIMARY KEY AUTOINCREMENT, recibo INTEGER REFERENCES Recibo(id) ON DELETE CASCADE, importe REAL NOT NULL, forma_pago TEXT NOT NULL CHECK(forma_pago="PAGARE" OR forma_pago="TRANSFERENCIA BANCARIA" OR forma_pago="GIRO DOMICILIADO" OR forma_pago="EFECTIVO"), vencimiento_pagare_giro DATE, gastos_impago_pagare REAL, nombre_banco_transfer_giro	TEXT, numero_cuenta_transfer_giro	TEXT, numero_recibo_giro TEXT, localidad_expedicion_giro	TEXT, fecha_expedicion_giro DATE, oficina_giro TEXT, nombre_librado_giro TEXT, domicilio_librado_giro TEXT, observaciones TEXT, fecha DATE NOT NULL
)

--     Table: LineaLibroMayor
DROP   TABLE IF EXISTS LineaLibroMayor;
CREATE TABLE LineaLibroMayor
(      numero INTEGER, inquilino	TEXT REFERENCES Inquilino(dni_cif) ON DELETE CASCADE, concepto	TEXT, fecha DATE DEFAULT CURRENT_DATE, debe REAL NOT NULL DEFAULT 0, haber REAL NOT NULL DEFAULT 0, PRIMARY KEY (numero, inquilino)
)

--     Table: PropietarioInmueble
DROP   TABLE IF EXISTS PropietarioInmueble;
CREATE TABLE PropietarioInmueble
(      propietario TEXT REFERENCES Propietario(dni_cif) ON DELETE CASCADE, inmueble INTEGER REFERENCES Inmueble(id) ON DELETE CASCADE, PRIMARY KEY (propietario, inmueble)
)
INSERT INTO PropietarioInmueble (propietario, inmueble) VALUES ('23051886K', 0);
INSERT INTO PropietarioInmueble (propietario, inmueble) VALUES ('23051886K', 1);
INSERT INTO PropietarioInmueble (propietario, inmueble) VALUES ('23051886K', 2);
INSERT INTO PropietarioInmueble (propietario, inmueble) VALUES ('23051886K', 3);
INSERT INTO PropietarioInmueble (propietario, inmueble) VALUES ('a', 0);
INSERT INTO PropietarioInmueble (propietario, inmueble) VALUES ('a', 1);

--     Trigger: UPDATE_libromayor_delete_pago
DROP   TRIGGER IF EXISTS UPDATE_libromayor_delete_pago;
CREATE TRIGGER UPDATE_libromayor_delete_pago BEFORE DELETE ON Pago
BEGIN  INSERT INTO linealibromayor (numero, inquilino) SELECT	max(llm.numero)+1 AS numero, llm.inquilino AS inquilino FROM	linealibromayor llm WHERE	llm.inquilino IN ( SELECT	c.inquilino FROM	Contrato c WHERE	c.id IN ( SELECT	r.contrato FROM	Recibo r WHERE	OLD.recibo=r.id ) ); UPDATE linealibromayor SET debe=OLD.importe, haber=0, concepto="Pago "||OLD.fecha||" eliminado" WHERE	inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.recibo ) AND	numero IN ( SELECT	MAX(lm.numero) FROM	linealibromayor lm WHERE	lm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.recibo ) );
END

--     Trigger: UPDATE_libromayor_delete_recibo
DROP   TRIGGER IF EXISTS UPDATE_libromayor_delete_recibo;
CREATE TRIGGER UPDATE_libromayor_delete_recibo BEFORE DELETE ON Recibo
BEGIN  INSERT INTO linealibromayor (numero, inquilino) SELECT	max(llm.numero)+1 AS numero, llm.inquilino AS inquilino FROM	linealibromayor llm WHERE	llm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.id ); UPDATE linealibromayor SET debe=0, haber=OLD.importe, concepto="Recibo "||OLD.fecha_emision||" eliminado" WHERE	inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.id ) AND	numero IN ( SELECT	MAX(lm.numero) FROM	linealibromayor lm WHERE	lm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.id ) );
END

--     Trigger: SET_resto_por_pagar_UPDATE
DROP   TRIGGER IF EXISTS SET_resto_por_pagar_UPDATE;
CREATE TRIGGER SET_resto_por_pagar_UPDATE AFTER UPDATE OF importe ON Recibo
BEGIN  UPDATE Recibo SET resto_por_pagar=OLD.resto_por_pagar+NEW.importe-OLD.importe WHERE id=NEW.id; UPDATE Recibo SET estado="PARCIALMENTE PAGADO" WHERE id=NEW.id AND resto_por_pagar<NEW.importe AND resto_por_pagar>0; UPDATE Recibo SET estado="LIQUIDADO" WHERE resto_por_pagar<=0 AND id=NEW.id;
END

--     Trigger: UPDATE_libromayor_UPDATE_pago
DROP   TRIGGER IF EXISTS UPDATE_libromayor_UPDATE_pago;
CREATE TRIGGER UPDATE_libromayor_UPDATE_pago AFTER UPDATE OF importe ON Pago
BEGIN  INSERT INTO linealibromayor (numero, inquilino) SELECT	max(llm.numero)+1 AS numero, llm.inquilino AS inquilino FROM	linealibromayor llm WHERE	llm.inquilino IN ( SELECT	c.inquilino FROM	Contrato c WHERE	c.id IN ( SELECT	r.contrato FROM	Recibo r WHERE	OLD.recibo=r.id ) ); UPDATE linealibromayor SET debe=OLD.importe, haber=NEW.importe, concepto="Pago "||OLD.fecha||" modificado" WHERE	inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.recibo ) AND	numero IN ( SELECT	MAX(lm.numero) FROM	linealibromayor lm WHERE	lm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.recibo ) );
END

--     Trigger: auto_libromayor_debe
DROP   TRIGGER IF EXISTS auto_libromayor_debe;
CREATE TRIGGER auto_libromayor_debe AFTER INSERT ON Recibo
BEGIN  INSERT INTO linealibromayor (numero, inquilino) SELECT	max(llm.numero)+1 AS numero, llm.inquilino AS inquilino FROM	linealibromayor llm WHERE	llm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=NEW.id ); UPDATE linealibromayor SET debe=NEW.importe, haber=0, concepto="Recibo "||NEW.fecha_emision WHERE	inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=NEW.id ) AND	numero IN ( SELECT	MAX(lm.numero) FROM	linealibromayor lm WHERE	lm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=NEW.id ) );
END

--     Trigger: set_resto_por_pagar
DROP   TRIGGER IF EXISTS set_resto_por_pagar;
CREATE TRIGGER set_resto_por_pagar AFTER INSERT ON Recibo
BEGIN  UPDATE Recibo SET resto_por_pagar=new.importe WHERE id=new.id;
END

--     Trigger: UPDATE_libromayor_UPDATE_recibo
DROP   TRIGGER IF EXISTS UPDATE_libromayor_UPDATE_recibo;
CREATE TRIGGER UPDATE_libromayor_UPDATE_recibo AFTER UPDATE OF importe ON Recibo
BEGIN  INSERT INTO linealibromayor (numero, inquilino) SELECT	max(llm.numero)+1 AS numero, llm.inquilino AS inquilino FROM	linealibromayor llm WHERE	llm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.id ); UPDATE linealibromayor SET debe=NEW.importe, haber=OLD.importe, concepto="Recibo "||OLD.fecha_emision||" modificado" WHERE	inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.id ) AND	numero IN ( SELECT	MAX(lm.numero) FROM	linealibromayor lm WHERE	lm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=OLD.id ) );
END

--     Trigger: init_libro_mayor
DROP   TRIGGER IF EXISTS init_libro_mayor;
CREATE TRIGGER init_libro_mayor AFTER INSERT ON Inquilino
BEGIN  INSERT INTO linealibromayor (numero, inquilino) VALUES (0, NEW.dni_cif);
END

--     Trigger: auto_libromayor_haber
DROP   TRIGGER IF EXISTS auto_libromayor_haber;
CREATE TRIGGER auto_libromayor_haber AFTER INSERT ON Pago
BEGIN  INSERT INTO linealibromayor (numero, inquilino) SELECT	max(llm.numero)+1 AS numero, llm.inquilino AS inquilino FROM	linealibromayor llm WHERE	llm.inquilino IN ( SELECT	c.inquilino FROM	Contrato c WHERE	c.id IN ( SELECT	r.contrato FROM	Recibo r WHERE	NEW.recibo=r.id ) ); UPDATE linealibromayor SET debe=0, haber=NEW.importe, concepto="Pago "||NEW.fecha WHERE	inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=NEW.recibo ) AND	numero IN ( SELECT	MAX(lm.numero) FROM	linealibromayor lm WHERE	lm.inquilino IN ( SELECT	c.inquilino FROM	Recibo r, Contrato c WHERE	r.contrato=c.id AND r.id=NEW.recibo ) );
END

--     Trigger: auto_resto_por_pagar
DROP   TRIGGER IF EXISTS auto_resto_por_pagar;
CREATE TRIGGER auto_resto_por_pagar AFTER INSERT ON Pago
BEGIN  UPDATE Recibo SET estado="PARCIALMENTE PAGADO", resto_por_pagar=resto_por_pagar-NEW.importe WHERE id=NEW.recibo; UPDATE Recibo SET estado="LIQUIDADO" WHERE resto_por_pagar<=0 AND id=NEW.recibo;
END

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;


