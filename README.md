
# 📋 ТЕХНИКӢ ВАЗИФА / TECHNICAL SPECIFICATION / ТЕХНИЧЕСКОЕ ЗАДАНИЕ
# 🌍 Tourist Booking System — TouristSystem

> **Муаллиф / Author / Автор:** Muhammadsoleh Nusratzoda  
> **Санаи тартиб / Date / Дата:** 1.06.2026  
> **Версия / Version / Версия:** 1.0.0  
> **Забони лоиҳа / Project Language / Язык проекта:** C# / .NET 9

---

## 📑 МУНДАРИҶА / TABLE OF CONTENTS / ОГЛАВЛЕНИЕ

1. [Тавсифи умумӣ / Project Overview / Общее описание](#1-overview)
2. [Стек технологӣ / Technology Stack / Технологический стек](#2-stack)
3. [Сохтори лоиҳа / Solution Structure / Структура решения](#3-structure)
4. [Нақшаи пойгоҳи маълумот / Database Design / Дизайн базы данных](#4-database)
5. [Классҳои Entity / Entity Classes / Классы Entity](#5-entities)
6. [DTOs](#6-dtos)
7. [Interfaces & Repositories](#7-interfaces)
8. [Services / Хизматҳо / Сервисы](#8-services)
9. [Controllers / Контроллерҳо / Контроллеры](#9-controllers)
10. [Middleware / Миёнабор / Промежуточное ПО](#10-middleware)
11. [Program.cs & appsettings.json](#11-programcs)
12. [EF Core Configuration & Migrations](#12-efcore)
13. [Background Services](#13-background)
14. [Blazor Frontend](#14-blazor)
15. [Нақшаи қадамба-қадам / Step-by-Step Plan / Пошаговый план](#15-plan)
16. [Swagger & JWT Setup](#16-swagger)
17. [Seed Data](#17-seed)
18. [Deployment Notes](#18-deployment)

---

<a name="1-overview"></a>
## 1. 🌐 Тавсифи умумӣ / Project Overview / Общее описание

### TJ — Тоҷикӣ
Ин лоиҳа як **платформаи ҷаҳонгардии онлайн** мебошад, ки корбарон метавонанд:
- Ҷойҳои сайёҳӣ, меҳмонхонаҳо, тарабхонаҳо, нақлиёт ва роҳнамоёнро бубинанд
- Бронь созанд ва ташриф оваранд
- Баҳо ва шарҳ нависанд
- Маъмур ҳамаи маълумотҳоро идора кунад

### EN — English
This project is an **online tourism booking platform** where users can:
- Browse tourist places, hotels, restaurants, transport, and guides
- Make and manage bookings
- Write reviews and ratings
- Admin manages all data and users

### RU — Русский
Данный проект — **онлайн платформа для туристического бронирования**, где пользователи могут:
- Просматривать туристические места, отели, рестораны, транспорт и гидов
- Создавать и управлять бронированиями
- Писать отзывы и оценки
- Администратор управляет всеми данными и пользователями

---

<a name="2-stack"></a>
## 2. 🛠️ Стек технологӣ / Technology Stack / Технологический стек

| Қисмат / Part | Технология / Technology |
|---|---|
| Backend Framework | ASP.NET Core Web API (.NET 9) |
| Language | C# 13 |
| Database | PostgreSQL 16 |
| ORM | Entity Framework Core 9 |
| Authentication | JWT Bearer Tokens |
| Authorization | Role-based (Policy) |
| Architecture | Clean Architecture (4 layers) |
| Logging | Serilog (Console + File) |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| API Docs | Swagger / Swashbuckle + JWT |
| Background | BackgroundService (IHostedService) |
| Frontend | Blazor WebAssembly |
| UI Framework | Bootstrap 5 |
| API Client | HttpClient + IHttpClientFactory |
| Token Storage | localStorage (JS Interop) |
| CORS | Configured for Blazor origin |
| API Versioning | Microsoft.AspNetCore.Mvc.Versioning |

---

<a name="3-structure"></a>
## 3. 🗂️ Сохтори лоиҳа / Solution Structure / Структура решения

```
TouristSystem/                          ← Solution root (папка решения)
│
├── Domain/               ← Layer 1: Entities, Enums, Interfaces
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Place.cs
│   │   ├── Hotel.cs
│   │   ├── Restaurant.cs
│   │   ├── Transport.cs
│   │   ├── Guide.cs
│   │   ├── Booking.cs
│   │   └── Review.cs
│   ├── Enums/
│   │   ├── BookingStatus.cs
│   │   ├── BookingType.cs
│   │   ├── TransportType.cs
│   │   └── UserRole.cs
│   └── Interfaces/
│       ├── Repositories/
│       │   ├── IPlaceRepository.cs
│       │   ├── IHotelRepository.cs
│       │   ├── IRestaurantRepository.cs
│       │   ├── ITransportRepository.cs
│       │   ├── IGuideRepository.cs
│       │   ├── IBookingRepository.cs
│       │   └── IReviewRepository.cs
│       └── Services/
│           ├── IPlaceService.cs
│           ├── IHotelService.cs
│           ├── IRestaurantService.cs
│           ├── ITransportService.cs
│           ├── IGuideService.cs
│           ├── IBookingService.cs
│           ├── IReviewService.cs
│           └── IAuthService.cs
│
├── Application/          ← Layer 2: Business Logic, DTOs, Mappings
│   ├── DTOs/
│   │   ├── Auth/
│   │   │   ├── RegisterDto.cs
│   │   │   ├── LoginDto.cs
│   │   │   └── AuthResponseDto.cs
│   │   ├── Place/
│   │   │   ├── PlaceDto.cs
│   │   │   ├── CreatePlaceDto.cs
│   │   │   └── UpdatePlaceDto.cs
│   │   ├── Hotel/
│   │   │   ├── HotelDto.cs
│   │   │   ├── CreateHotelDto.cs
│   │   │   └── UpdateHotelDto.cs
│   │   ├── Restaurant/
│   │   │   ├── RestaurantDto.cs
│   │   │   ├── CreateRestaurantDto.cs
│   │   │   └── UpdateRestaurantDto.cs
│   │   ├── Transport/
│   │   │   ├── TransportDto.cs
│   │   │   ├── CreateTransportDto.cs
│   │   │   └── UpdateTransportDto.cs
│   │   ├── Guide/
│   │   │   ├── GuideDto.cs
│   │   │   ├── CreateGuideDto.cs
│   │   │   └── UpdateGuideDto.cs
│   │   ├── Booking/
│   │   │   ├── BookingDto.cs
│   │   │   └── CreateBookingDto.cs
│   │   └── Review/
│   │       ├── ReviewDto.cs
│   │       └── CreateReviewDto.cs
│   ├── Common/
│   │   ├── ApiResponse.cs             ← Generic response wrapper
│   │   ├── PagedResult.cs             ← Pagination wrapper
│   │   └── FilterParams.cs            ← Filter query params
│   ├── Mappings/
│   │   └── MappingProfile.cs          ← AutoMapper profile
│   ├── Validators/
│   │   ├── RegisterValidator.cs
│   │   ├── CreatePlaceValidator.cs
│   │   ├── CreateHotelValidator.cs
│   │   ├── CreateBookingValidator.cs
│   │   └── CreateReviewValidator.cs
│   └── Services/
│       ├── AuthService.cs
│       ├── PlaceService.cs
│       ├── HotelService.cs
│       ├── RestaurantService.cs
│       ├── TransportService.cs
│       ├── GuideService.cs
│       ├── BookingService.cs
│       └── ReviewService.cs
│
├── Infrastructure/       ← Layer 3: EF Core, Repos, DB Config
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── Seed/
│   │       └── DataSeeder.cs
│   ├── Configurations/
│   │   ├── PlaceConfiguration.cs
│   │   ├── HotelConfiguration.cs
│   │   ├── BookingConfiguration.cs
│   │   └── ReviewConfiguration.cs
│   ├── Repositories/
│   │   ├── PlaceRepository.cs
│   │   ├── HotelRepository.cs
│   │   ├── RestaurantRepository.cs
│   │   ├── TransportRepository.cs
│   │   ├── GuideRepository.cs
│   │   ├── BookingRepository.cs
│   │   └── ReviewRepository.cs
│   ├── BackgroundServices/
│   │   └── BookingExpirationService.cs
│   └── DependencyInjection.cs         ← Extension method for DI registration
│
├── WebApi/               ← Layer 4: API Entry Point
│   ├── Controllers/
│   │   ├── v1/
│   │   │   ├── AuthController.cs
│   │   │   ├── PlacesController.cs
│   │   │   ├── HotelsController.cs
│   │   │   ├── RestaurantsController.cs
│   │   │   ├── TransportController.cs
│   │   │   ├── GuidesController.cs
│   │   │   ├── BookingsController.cs
│   │   │   ├── ReviewsController.cs
│   │   │   └── AdminController.cs
│   ├── Middleware/
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   └── RequestLoggingMiddleware.cs
│   ├── Program.cs
│   └── appsettings.json
│
└── BlazorClient/         ← Frontend: Blazor WebAssembly
    ├── Pages/
    │   ├── Index.razor                 ← Home page
    │   ├── Login.razor
    │   ├── Register.razor
    │   ├── Places/
    │   │   ├── Places.razor
    │   │   └── PlaceDetail.razor
    │   ├── Hotels/
    │   │   ├── Hotels.razor
    │   │   └── HotelDetail.razor
    │   ├── Restaurants/
    │   │   └── Restaurants.razor
    │   ├── Transport/
    │   │   └── Transport.razor
    │   ├── Guides/
    │   │   └── Guides.razor
    │   ├── Bookings/
    │   │   ├── CreateBooking.razor
    │   │   └── MyBookings.razor
    │   └── Admin/
    │       ├── AdminDashboard.razor
    │       ├── ManagePlaces.razor
    │       ├── ManageHotels.razor
    │       └── ManageUsers.razor
    ├── Services/
    │   ├── AuthService.cs
    │   ├── PlaceClientService.cs
    │   ├── HotelClientService.cs
    │   ├── BookingClientService.cs
    │   └── HttpClientAuthHandler.cs
    ├── Models/                         ← Client-side DTOs (mirror of Application DTOs)
    ├── Shared/
    │   ├── MainLayout.razor
    │   ├── NavMenu.razor
    │   └── RedirectToLogin.razor
    ├── wwwroot/
    │   └── index.html
    └── Program.cs
```

---

<a name="4-database"></a>
## 4. 🗄️ Нақшаи пойгоҳи маълумот / Database Design / Дизайн базы данных

### Тавзеҳ / Explanation / Объяснение

> **TJ:** Ҳар як ҷадвал як объекти воқеии дунёи мо аст. Масалан `Hotels` = меҳмонхонаҳо дар бораи меҳмонхона маълумот нигоҳ медорад.  
> **EN:** Each table = a real-world object. Example: `Hotels` table stores data about hotels.  
> **RU:** Каждая таблица = реальный объект мира. Например: таблица `Hotels` хранит данные об отелях.

### ERD (Entity Relationship Diagram — текстӣ / text / текстовый)

```
Users ──────────────────────────────────────────────┐
  │                                                  │
  ├──< Bookings >──── Places                         │
  │         │──────── Hotels                         │
  │         │──────── Guides                         │
  │         └──────── Transport                      │
  │                                                  │
  └──< Reviews >───── Places                         │
              │─────── Hotels                        │
              └─────── Guides                        │
                                                     │
Guides ──────────────────────────────────────────────┘
         (Guide has a UserId FK to Users)
```
