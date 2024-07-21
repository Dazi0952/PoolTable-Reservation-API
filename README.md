### PoolTable Reservation API

PoolTable Reservation API to aplikacja internetowa zbudowana przy użyciu ASP.NET Core, która umożliwia zarządzanie rezerwacjami stołów bilardowych oraz obsługę płatności. Aplikacja zapewnia uwierzytelnianie i autoryzację użytkowników przy użyciu JSON Web Tokens (JWT).

## Funkcje

- Rejestracja i logowanie użytkowników z uwierzytelnianiem JWT.
- Operacje CRUD dla stołów bilardowych, rezerwacji i płatności.
- Testy jednostkowe i integracyjne.
- Integracja Swaggera do dokumentacji API.

## Technologie

- ASP.NET Core
- Entity Framework Core
- JWT (JSON Web Tokens)
- xUnit, Moq, FluentAssertions do testowania
- Swagger do dokumentacji API

## Jak zacząć

### Wymagania

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (lub opcjonalnie localdb)

### Instalacja

1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/twojanazwa/pooltable-reservation-api.git
   cd pooltable-reservation-api
   ```

2. Skonfiguruj string połączenia z bazą danych w `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=PoolTableReservationDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. Zaktualizuj bazę danych:
   ```bash
   dotnet ef migrations add InitialCreate --project Infrastructure --startup-project WebApi
   dotnet ef database update --project Infrastructure --startup-project WebApi
   ```

### Dokumentacja API
Dokumentacja API jest dostępna za pośrednictwem Swaggera. Po uruchomieniu aplikacji, przejdź do `https://localhost:7062/swagger/index.html`, aby zobaczyć dokumentację API.

### Testowanie
Testy jednostkowe i integracyjne są realizowane za pomocą xUnit, Moq i FluentAssertions.

Przejdź do katalogu projektu testowego:
```bash
cd Tests
```

Uruchom testy:
```bash
dotnet test
```

### Użycie

#### Uwierzytelnianie
Aby uwierzytelnić użytkownika, wyślij żądanie POST do `/api/authentication/login` z następującym ładunkiem JSON:

```json
{
  "loginName": "WielkiChlop",
  "password": "Kochampieski123@"
}
```
Jeśli poświadczenia są poprawne, otrzymasz token JWT w odpowiedzi. Użyj tego tokena, aby uwierzytelniać kolejne żądania, dodając go w nagłówku Authorization:

```http
Authorization: Bearer <your_token>
```

#### Endpointy

##### Płatności
```
GET /api/Payments - Pobierz wszystkie płatności
GET /api/Payments/{id} - Pobierz płatność po ID
POST /api/Payments - Utwórz nową płatność
PUT /api/Payments/{id} - Zaktualizuj płatność
DELETE /api/Payments/{id} - Usuń płatność
```

##### Stoły bilardowe
```
GET /api/PoolTables - Pobierz wszystkie stoły bilardowe
GET /api/PoolTables/{id} - Pobierz stół bilardowy po ID
POST /api/PoolTables - Utwórz nowy stół bilardowy
PUT /api/PoolTables/{id} - Zaktualizuj stół bilardowy
DELETE /api/PoolTables/{id} - Usuń stół bilardowy
```

##### Rezerwacje
```
GET /api/Reservations - Pobierz wszystkie rezerwacje
GET /api/Reservations/{id} - Pobierz rezerwację po ID
POST /api/Reservations - Utwórz nową rezerwację
PUT /api/Reservations/{id} - Zaktualizuj rezerwację
DELETE /api/Reservations/{id} - Usuń rezerwację
```

### Twórcy:
- Kacper Piłat
- Patryk Lichoń
