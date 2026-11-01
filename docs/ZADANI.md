# Zadání – IT evidence

Webová aplikace pro evidenci IT majetku, licencí a požadavků na IT (helpdesk) v menší
firmě (20–200 zaměstnanců), kde IT spravuje jeden až tři lidé.

Aplikace odpovídá na tři otázky:
1. **Kdo má co** – jaký počítač, telefon, monitor nebo licenci má který zaměstnanec.
2. **Co končí** – záruky zařízení a platnosti licencí.
3. **Co je rozbité** – požadavky zaměstnanců na IT a jejich řešení v termínu.

Vývoj probíhá metodou **test-driven development**: pro každý milník nejdřív vzniknou
rozhraní a testy podle tohoto zadání a teprve potom implementace.

---

## 1. Role

| Role | Oprávnění |
|---|---|
| **Zaměstnanec** | vidí svoje zařízení a licence, zakládá požadavky, vidí jen svoje požadavky a jejich veřejné komentáře, nemůže měnit prioritu |
| **Technik IT** | spravuje zařízení, licence, přidělení, servisní záznamy; řeší požadavky, vidí interní poznámky |
| **Inventurista** | provádí inventuru – potvrzuje nalezení zařízení a jejich umístění; ostatní data jen čte |
| **Administrátor** | vše co technik + správa uživatelů a rolí, číselníků, nastavení SLA a mazání |

Uživatel může mít více rolí (např. technik a inventurista).

---

## 2. Majetek (milník 1)

### Zařízení
- typ (notebook, stolní PC, monitor, telefon, tablet, tiskárna, síťový prvek, ostatní),
  výrobce, model, sériové číslo, datum nákupu, cena, záruka do, umístění
- **inventární číslo** ve tvaru `IT-RRRR-NNNN` (např. `IT-2026-0001`), unikátní,
  pořadí se čísluje zvlášť pro každý rok a navazuje na nejvyšší existující číslo daného roku
- výrobce a model jsou povinné, cena nesmí být záporná, záruka nesmí končit před datem nákupu

### Stavy zařízení
```
Skladem ──přidělit──> Přiděleno ──vrátit──> Skladem
   │                     │
   └──servis──┐   ┌──servis
              v   v
            V servisu ──dokončit servis──> Přiděleno (měl-li držitele) / Skladem
Skladem ──vyřadit──> Vyřazeno (konečný stav)
```
- přidělit lze jen zařízení **skladem** a jen **aktivnímu** zaměstnanci
- zařízení má v jednu chvíli nejvýše jednoho držitele
- každé přidělení se ukládá do **historie** (kdo, od kdy, do kdy, poznámka k vrácení);
  vrácení nemůže být dřív než přidělení
- zařízení v servisu si ponechává držitele; po dokončení servisu se mu vrací
- zařízení v servisu lze vrátit (např. při odchodu zaměstnance) – po servisu pak jde na sklad
- vyřadit lze jen zařízení skladem (přidělené se musí nejdřív vrátit), s povinným důvodem;
  s vyřazeným zařízením už nejde nic dělat
- záruka „končí brzy“ = končí dnes nebo během příštích 30 dní (počet dní lze změnit);
  již skončená záruka „nekončí brzy“, ale je „skončená“

### Odchod zaměstnance
- přehled všeho, co má zaměstnanec vrátit (včetně zařízení v servisu)
- hromadné vrácení všech jeho zařízení najednou a deaktivace zaměstnance
- deaktivovanému zaměstnanci nelze nic přidělit

---

## 3. Licence (milník 2)

- software, typ (na uživatele / na zařízení), počet licencí, platnost do (nebo trvalá),
  cena, dodavatel
- přiřazení zaměstnanci nebo zařízení podle typu licence
- **nesmí se přiřadit víc licencí, než je jejich počet**; snížit počet pod počet
  přiřazených nelze
- stejná licence nejde stejnému zaměstnanci / zařízení přiřadit dvakrát
- licence „končí brzy“ = do 30 dní; po skončení platnosti nelze přiřazovat
- při vyřazení zařízení a odchodu zaměstnance se jeho licence uvolní

---

## 4. Kalendář a termíny (SLA) (milník 3)

Termín vyřešení požadavku se počítá podle **priority**, každá priorita má vlastní
**režim** a dobu:

| Priorita | Režim | Doba |
|---|---|---|
| Kritická | nepřetržitě (24/7) | 4 h |
| Vysoká | pracovní doba | 8 h |
| Normální | pracovní doba | 24 h |
| Nízká | pracovní doba | 40 h |

- **pracovní doba**: Po–Pá 8:00–16:00 (nastavitelné), mimo **české státní svátky**
  (včetně Velkého pátku a Velikonočního pondělí, které se dopočítávají)
- požadavek založený mimo pracovní dobu se v režimu pracovní doby začne počítat od
  nejbližšího začátku pracovní doby
- **pozastavení**: ve stavu „Čeká na uživatele“ se čas nepočítá, po návratu se termín posune
- **změna priority** během řešení přepočítá termín od založení podle nové priority
  (již uplynulý čas se započítá)
- požadavek je **po termínu**, pokud není vyřešen a aktuální čas je za termínem

---

## 5. Helpdesk – požadavky (milník 4)

- název, popis, kategorie (hardware, software, účet a přístupy, síť, ostatní), priorita,
  vazba na zařízení (volitelně), zadavatel, řešitel
- stavy:
```
Nový ──přiřadit──> Přiřazený ──začít──> Řeší se ──vyřešit──> Vyřešený ──uzavřít──> Uzavřený
                                  │  ^                        │
                     čekat na uživatele                 znovu otevřít (do 7 dní)
                                  v  │                        v
                          Čeká na uživatele               Řeší se
```
- vyřešit nelze bez řešitele a bez popisu řešení
- uzavřít lze jen vyřešený požadavek; zadavatel ho může **znovu otevřít do 7 dní**
  od vyřešení, poté se uzavře automaticky
- k vyřazenému zařízení nelze založit požadavek
- **komentáře** veřejné a **interní poznámky** (vidí jen IT)
- zaměstnanec vidí jen svoje požadavky, nemůže měnit prioritu ani řešitele

---

## 6. Servis, inventura a audit (milník 5)

### Servisní záznamy
- oprava zařízení: datum, popis, dodavatel servisu, cena, záruční / pozáruční oprava
- volitelná vazba na požadavek, ze kterého oprava vznikla
- **přílohy** – PDF s podklady (protokol, faktura), max. 10 MB, jen PDF
- celkové náklady na opravy zařízení

### Inventura
- inventuru zakládá administrátor (název, datum, rozsah – vše / umístění)
- inventurista u každého zařízení potvrdí **nalezeno / nenalezeno** a případně skutečné umístění
- dokončit lze, až jsou všechna zařízení zkontrolovaná
- výstup: **protokol nesrovnalostí** (nenalezená zařízení, jiné umístění)
- vyřazená zařízení se do inventury nezahrnují

### Auditní log
- záznam každé změny: kdo, kdy, jaký objekt, jaká akce, původní a nová hodnota
- přidělení a vrácení, změny stavů zařízení a požadavků, přiřazení licencí, mazání
- log nelze upravit ani smazat

---

## 7. Databáze a aplikace (milník 6)

- **MS SQL Server** přes **Entity Framework Core**, migrace
- přihlášení a role přes **ASP.NET Core Identity**
- integrační testy proti skutečnému SQL Serveru (v CI jako služba GitHub Actions)

## 8. Uživatelské rozhraní (milník 7)

- **Blazor**, rozhraní **česky a anglicky** s přepínačem
- přehled: končící záruky a licence, otevřené požadavky a požadavky po termínu,
  zařízení skladem
- seznamy s filtrováním a hledáním, detail zařízení s historií a servisem

## 9. Na později
- přihlášení přes Microsoft Entra ID / Active Directory
- export do Excelu, import zařízení z CSV
- e-mailová upozornění (končící záruky, nové požadavky, blížící se termín)
- REST API, QR štítky na zařízení

---

## Technologie

- **.NET 10 (LTS)**, C#, ASP.NET Core, Blazor
- Entity Framework Core, MS SQL Server (lokálně v Dockeru)
- xUnit, GitHub Actions (testy při každém pushi)

## Struktura řešení

```
src/ItEvidence.Domain           entity a pravidla, bez závislostí na databázi a UI
src/ItEvidence.Application      případy užití (milník 6)
src/ItEvidence.Infrastructure   EF Core, SQL Server, Identity (milník 6)
src/ItEvidence.Web              Blazor UI (milník 7)
tests/ItEvidence.Domain.Tests   unit testy doménové logiky
```

Kód (třídy, metody, proměnné) je anglicky, zadání, dokumentace a rozhraní aplikace česky
(rozhraní i anglicky).
