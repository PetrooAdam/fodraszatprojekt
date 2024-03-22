use fodraszat_adatbazis;


-- Konnyu feladatok

-- 1. feladat
select * 
from fodrasz;


-- 2. feladat
insert into fodrasz 
values("+60 124 4522","Betyár Tamás","betyarvagyok@gmail.com");

-- 3. feladat
select *
from helyszin
where iranyitoszam = 2700;

-- 4. feladat
select nev
from vendeg
where email LIKE "%@ckik.hu";

-- 5. feladat
UPDATE fodrasz
set email = "lonelybarber@outlook.com"
where nev = "Sándor Marcell Szabolcs";




-- Kozepes feladatok

-- 1. feladat
select nev
from fodrasz
where telefonszam like "+3620%"
union
select nev
from fodrasz
where telefonszam like "0620%";





-- Nehez feladatok 


-- 1. feladat
select szolgaltatas_nev, vendeg_telefonszam, uzlet_id, telefonszam, nev, email
from foglalas f inner join fodrasz t on f.fodrasz_telefonszam = t.telefonszam 
where fodrasz_telefonszam = "06701923124";


-- 2. feladat
select distinct t.nev
from foglalas f inner join fodrasz t on f.fodrasz_telefonszam = t.telefonszam inner join vendeg v on f.vendeg_telefonszam = v.telefonszam
where ROUND((LENGTH(v.nev) - LENGTH( REPLACE (v.nev, " ", "") )) / LENGTH(" ")) = 2
order by t.nev ASC;

