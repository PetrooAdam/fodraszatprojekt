create database fodraszat_adatbazis;

use fodraszat_adatbazis;

create table helyszin(
    id int primary key auto_increment,
    iranyitoszam int not null,
    cim varchar(50) not null
);

create table uzlet(
    id int primary key auto_increment,
    uzletvezetp varchar(50) not null,
    helyszin_id int not null,
    CONSTRAINT FK_Uzlet_Helyszin FOREIGN KEY (helyszin_id) REFERENCES helyszin(id)
);

create table fodrasz(
    telefonszam varchar(50) primary key not null,
    nev varchar(50) not null,
    email varchar(50) not null
);

create table vendeg(
    telefonszam varchar(50) primary key not null,
    nev varchar(50) not null,
    email varchar(50) not null
);

create table szolgaltatas(
    nev varchar(50) primary key not null,
    ar int not null,
    ido time not null
);

create table foglalas(
    id int primary key auto_increment,
    szolgaltatas_nev varchar(50),
    CONSTRAINT FK_Foglalas_Szolgaltatas FOREIGN KEY (szolgaltatas_nev) REFERENCES szolgaltatas(nev),
    vendeg_telefonszam varchar(50),
    CONSTRAINT FK_Foglalas_Vendeg FOREIGN KEY (vendeg_telefonszam) REFERENCES vendeg(telefonszam),
    fodrasz_telefonszam varchar(50),
    CONSTRAINT FK_Foglalas_Fodrasz FOREIGN KEY (fodrasz_telefonszam) REFERENCES fodrasz(telefonszam),
    uzlet_id int,
    CONSTRAINT FK_Foglalas_uzlet FOREIGN KEY (uzlet_id) REFERENCES uzlet(id)
);