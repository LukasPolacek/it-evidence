# IT evidence

![Testy](https://github.com/LukasPolacek/it-evidence/actions/workflows/tests.yml/badge.svg)

Webová aplikace pro evidenci IT majetku, licencí a požadavků na IT (helpdesk) v menší
firmě – kdo má jaké zařízení, kdy končí záruky a licence, inventura, servisní záznamy
a řešení požadavků v termínu podle priority.

Jde o mou **programovací úlohu v C# / .NET**, vyvíjenou metodou **test-driven development
(TDD)** podle předem stanovených požadavků na systém:

1. požadavky na systém jsou sepsané v **[zadání](docs/ZADANI.md)**,
2. pro každou část nejdřív vzniknou rozhraní a **testy, které zpočátku neprocházejí**,
3. teprve potom následuje implementace, dokud všechny testy neprojdou,
4. testy se spouštějí automaticky na GitHub Actions u každého commitu – stav ukazuje
   odznak nahoře.

Projekt postupně roste po jednotlivých milnících, historie commitů ukazuje celý postup.

> **EN:** An IT asset, licence and helpdesk management web app for small companies.
> My C# / .NET programming exercise built test-first (TDD) from a written specification –
> tests for each milestone come first, the implementation follows.

## Stav vývoje

| Milník | Obsah | Stav |
|---|---|---|
| 1 | Majetek – zařízení, stavy, přidělení a historie, inventární čísla, odchod zaměstnance | 🔴 testy připravené |
| 2 | Licence – počty, přiřazení, končící platnost | ⬜ |
| 3 | Kalendář a termíny (SLA) – pracovní doba, české svátky, pozastavení | ⬜ |
| 4 | Helpdesk – požadavky, stavy, komentáře, termíny, oprávnění | ⬜ |
| 5 | Servisní záznamy s přílohami, inventura, auditní log | ⬜ |
| 6 | Databáze (MS SQL, EF Core), přihlášení a role, integrační testy | ⬜ |
| 7 | Uživatelské rozhraní (Blazor), čeština a angličtina | ⬜ |

## Technologie

- **.NET 10**, C#, ASP.NET Core, Blazor
- Entity Framework Core, **MS SQL Server**
- **xUnit**, GitHub Actions

## Struktura

```
docs/ZADANI.md                  požadavky na systém
src/ItEvidence.Domain           entity a pravidla domény
tests/ItEvidence.Domain.Tests   unit testy domény
```

## Spuštění testů

```bash
dotnet test ItEvidence.slnx
```

## Licence

Všechna práva vyhrazena – zdrojový kód je zveřejněn pouze k nahlédnutí.
Podrobnosti v souboru [LICENSE](LICENSE).
