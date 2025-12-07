# Flashcards – Fullstack-applikation

En komplett lokal fullstack-app byggd med .NET API, SQL Server och React.  
Projektet låter användare skapa, redigera, söka och studera flashcards organiserade i listor och taggar.

---

# 1. Projektbeskrivning

Flashcards-systemet gör det möjligt att:

- Skapa egna listor med flashcards  
- Lägga till, uppdatera och ta bort flashcards  
- Tagga varje flashcard  
- Sökning och filtrering av flashcards i frontend  
- Studera kort i slumpmässigt läge  
- Arbeta med ren arkitektur, DTOs, repositories och enhetstester  

Backend är uppbyggt i en Clean Architecture-liknande struktur:

- Domain-lager  
- Application-lager (CQRS, MediatR, DTOs, mapping)  
- Infrastructure-lager (EF Core, DbContext, repositories)  
- API-lager (Controllers + global error handling)

Frontend byggs i React med flera sidor, formulär, validering, och kommunikation med API:et.

---

# 2. Arkitekturöversikt

## Backend

- Domain: entiteter (Users, Flashcard, FlashcardList, Tag, FlashcardTag), repository-interfaces, service-interfaces, Enums 

- Application: 
  - MediatR-handlers, DTOs, AutoMapper-profiler, OperationResult  
  - Centraliserad logging och validering via MediatR-behavior (Logging/ValidationBehavior)  
  - FluentValidation + validering  
  - Dependency Injection: MediatR-handlers, AutoMapper, FluentValidation och behaviors registreras via `ServiceCollectionExtensions`

- Infrastructure: 
  - EF Core, DbContext, migrationer, repositories  
  - Services (implementationer av service-interfaces definierade i Domain-lagret)  
  - Dependency Injection: DbContext, repositories och services registreras via `ServiceCollectionExtension`

- API: 
  - Controllers, global exception-handling  
  - Dependency Injection: JWT-baserad authentication registreras via `AuthenticationServiceExtension`

### Databasrelationer:

- 1 → många mellan List och Flashcards  
- många ↔ många mellan Flashcard och Tag  

## Frontend (React)

- Sidor: Login, Register, FlashcardManager, RandomFlashcard, Profile  
- Listvy för flashcards  
- Detaljvy  
- Formulär för create och update  
- Sök- och filterfunktion  
- Loading- och error-hantering  
- Tailwind CSS-styling och komponentstruktur  

## CI/CD (GitHub Actions)

- Workflow kör:  
  - dotnet restore  
  - dotnet build  
  - dotnet test  
- Triggas vid push och pull request  

---

# 3. Instruktioner – Starta projektet

## Backend

- Öppna projektet i Visual Studio eller terminal  
- Starta API:et:  
  - `dotnet run`  

## Databas

- Starta SQL Server lokalt  
- Kontrollera connection string i `appsettings.json`  
- Databasen kan skapas via SQL-skriptet i repot:  
  - `Sql_Scripts/CreateDatabase.sql`

## Frontend

- Navigera till frontend-mappen:  
  - `cd frontend`  
- Installera paket:  
  - `npm install`  
- Starta dev-server:  
  - `npm run dev`  

---

# 4. API Endpoints

## Auth

- POST `/api/Auth/register` – Registrera ny användare  
- POST `/api/Auth/login` – Logga in användare  

## FlashcardLists

- POST `/api/FlashcardLists/create-flashcard-list` – Skapa ny lista  
- GET `/api/FlashcardLists/get-flashcard-lists` – Hämta alla listor  
- GET `/api/FlashcardLists/{flashcardListId}/flashcards` – Hämta flashcards i en lista  
- PATCH `/api/FlashcardLists/{id}` – Uppdatera lista  
- DELETE `/api/FlashcardLists/{id}` – Ta bort lista  

## Flashcards

- GET `/api/Flashcards/get-random-flashcard/{flashcardListId}` – Hämta random flashcard (utan upprepningar innan alla visats)  
- POST `/api/Flashcards/create-flashcard` – Skapa nytt flashcard  
- PATCH `/api/Flashcards/{flashcardId}` – Uppdatera flashcard  
- DELETE `/api/Flashcards/{flashcardId}` – Ta bort flashcard  
- GET `/api/Flashcards/{flashcardId}` – Hämta ett specifikt flashcard  

## Users

- GET `/api/Users/current` – Hämta information om nuvarande användare  
- PATCH `/api/Users/current` – Uppdatera nuvarande användare  
- DELETE `/api/Users/current` – Ta bort nuvarande användare  

---

# 5. Tester

Backend tester inkluderar:

- Flashcards (CRUD, random study, edge cases som unauthorized och not found)
- FlashcardLists (CRUD)
- Users (Login, Register, validering)
- Tester körs automatiskt via GitHub Actions  

---

# 6. Kända buggar och förbättringar

- CRUD för Tag
- Random-studieläget kan förbättras för att visa varje kort endast en gång innan alla visats  
- Validering i frontend är grundläggande  
- UI saknar pagination (men har sök/filter)  
- API-felmeddelanden kan förbättras ytterligare  
- Styling kan förbättras visuellt och strukturellt  
