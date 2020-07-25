CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS store_event;

CREATE EXTENSION IF NOT EXISTS pgcrypto;
CREATE EXTENSION IF NOT EXISTS tablefunc;

CREATE TABLE store_event.tbl_stored_event (
    id uuid NOT NULL,
    action text NULL,
    aggregateid uuid NOT NULL,
    creationdate timestamp without time zone NOT NULL,
    data json NULL,
    usuario text NULL,
    CONSTRAINT "PK_tbl_stored_event" PRIMARY KEY (id)
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200331195414_EventSourcingDbInit', '3.1.5');