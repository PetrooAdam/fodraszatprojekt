use fodraszat_adatbazis;

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
set email = "lonelybarber@outlook.com "
where nev = "Sándor Marcell Szabolcs";