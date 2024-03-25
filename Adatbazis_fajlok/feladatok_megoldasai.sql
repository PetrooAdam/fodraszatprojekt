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



-- 2. feladat

select *,
    case    
        when ar = 0 then ""
        when ar = 5000 then "EO kezelés"
        when ar > 5000 then "összetett kezelés"
        when ar < 5000 then "egyszerű kezelés"
        else "?"
    end as kezeles  
from szolgaltatas;



-- 3. feladat

select szolgaltatas_nev as szolgaltatas, COUNT(*) as DB
from foglalas
group by szolgaltatas_nev
order by DB DESC
limit 3;



-- 4. feladat

select u.uzletvezetp as Tulaj, COUNT(f.id) as FoglalasSzam
from uzlet u
left join foglalas f on u.id = f.uzlet_id
group by u.uzletvezetp
order by FoglalasSzam desc
limit 1;



-- 5. feladat

select u.uzletvezetp as Tulaj, COUNT(distinct f.szolgaltatas_nev) as szolgaltatasszam
from uzlet u
left join foglalas f on u.id = f.uzlet_id
group by u.id, u.uzletvezetp
order by szolgaltatasszam desc
limit 1;







-- Nehez feladatok 


-- 1. feladat

select szolgaltatas_nev, vendeg_telefonszam, uzlet_id, telefonszam, nev, email
from foglalas f inner join fodrasz t on f.fodrasz_telefonszam = t.telefonszam 
where fodrasz_telefonszam = "06701923124";



-- 2. feladat

select distinct t.nev
from foglalas f inner join fodrasz t on f.fodrasz_telefonszam = t.telefonszam inner join vendeg v on f.vendeg_telefonszam = v.telefonszam
where ROUND((length(v.nev) - length( REPLACE(v.nev, " ", "") )) / length(" ")) = 2
order by t.nev ASC;



-- 3. feladat

select foglalas.*
from foglalas
join vendeg on foglalas.vendeg_telefonszam = vendeg.telefonszam
join fodrasz on foglalas.fodrasz_telefonszam = fodrasz.telefonszam
where (SUBSTRING(vendeg.telefonszam, 1, 3) = '+36' and SUBSTRING(fodrasz.telefonszam, 1, 3) = '+36' and SUBSTRING(vendeg.telefonszam, 4, 2) = SUBSTRING(fodrasz.telefonszam, 4, 2))
    or
    (SUBSTRING(vendeg.telefonszam, 1, 2) = '06' and SUBSTRING(fodrasz.telefonszam, 1, 2) = '06' and SUBSTRING(vendeg.telefonszam, 3, 2) = SUBSTRING(fodrasz.telefonszam, 3, 2))
    or
    (SUBSTRING(vendeg.telefonszam, 1, 3) = '+36' and SUBSTRING(fodrasz.telefonszam, 1, 2) = '06' and SUBSTRING(vendeg.telefonszam, 4, 2) = SUBSTRING(fodrasz.telefonszam, 3, 2))
    or
    (SUBSTRING(vendeg.telefonszam, 1, 2) = '06' and SUBSTRING(fodrasz.telefonszam, 1, 3) = '+36' and SUBSTRING(vendeg.telefonszam, 3, 2) = SUBSTRING(fodrasz.telefonszam, 4, 2));



-- 4. feladat

select vendeg_telefonszam, koltseg
from (
    select v.telefonszam as vendeg_telefonszam, SUM(s.ar) as koltseg
    from vendeg v
    join foglalas f on v.telefonszam = f.vendeg_telefonszam
    join szolgaltatas s on f.szolgaltatas_nev = s.nev
    group by v.telefonszam
) as koltseg
order by koltseg desc
limit 5;



-- 5. feladat

select count(distinct vendeg_telefonszam) as osszes_vevo, count(distinct case when foglalszam > 1 then vendeg_telefonszam end) as visszatero_vevok,
CONCAT(ROUND((count(distinct case when foglalszam > 1 then vendeg_telefonszam end) / count(distinct vendeg_telefonszam)) * 100,2),"%") as visszateres_arány
from (
    select 
        vendeg_telefonszam, 
        count(*) as foglalszam
    from 
        foglalas
    group by 
        vendeg_telefonszam
) as foglalasok_szama_per_vevo;
