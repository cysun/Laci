﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Cities" (
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NULL,
    "Population" integer NOT NULL,
    CONSTRAINT "PK_Cities" PRIMARY KEY ("Id")
);

CREATE TABLE "Record" (
    "CityId" integer NOT NULL,
    "Date" timestamp without time zone NOT NULL,
    "Tests" integer NOT NULL,
    "Cases" integer NOT NULL,
    "Deaths" integer NOT NULL,
    CONSTRAINT "PK_Record" PRIMARY KEY ("CityId", "Date"),
    CONSTRAINT "FK_Record_Cities_CityId" FOREIGN KEY ("CityId") REFERENCES "Cities" ("Id") ON DELETE CASCADE
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200704215033_InitialSchema', '3.1.5');

