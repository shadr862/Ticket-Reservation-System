# 🚌 Bus Ticket Reservation System

A full-stack **Bus Ticket Reservation System** built using **.NET 9 (C#)** and **Angular**, following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.
The system allows users to search for available buses, view seat layouts, and book tickets with a clean and modular architecture.

---

## 🎯 Objective

Design and develop a scalable, maintainable bus ticket booking system with:

* Clear separation of concerns (Domain, Application, Infrastructure, WebApi, ClientApp)
* Clean business logic and DDD-based services
* Usable Angular frontend integrated with REST APIs
* Comprehensive unit testing for reliability

---

## ⚙️ Technology Stack

### 🖥️ Backend

* **.NET 9 (C#)**
* **Entity Framework Core** (PostgreSQL)
* **Clean Architecture + DDD**
* **xUnit** for Unit Testing

### 🌐 Frontend

* **Angular (latest stable version)**
* **TypeScript**
* **TailwindCSS**

---

## 🧩 Architecture Overview

```
/src
 ├── /Domain                → Entities, Value Objects, Domain Services
 ├── /Application           → Business logic, Use cases, Application Services
 ├── /Application.Contracts → DTOs and Interfaces
 ├── /Infrastructure        → EF Core DbContext, Repository Implementations
 ├── /WebApi                → REST API Controllers and Endpoints
 └── /ClientApp             → Angular Frontend Application
```

Each layer follows **Clean Architecture** separation and **DDD** principles.

---

## 🚀 Features

### 1️⃣ Search Available Buses

Users can search by:

* From City
* To City
* Journey Date

**Results display:**

* Company Name
* Bus Name
* Start/Arrival Time
* Seats Left (TotalSeats − BookedSeats)
* Price

### 2️⃣ View Seat Plan & Book Seats

* View bus seat layout with color-coded seat statuses:

  * 🟩 Available
  * 🟧 Booked
  * 🟥 Sold
* Select seats, input passenger details, and confirm booking.
* Booking updates seat status in real time.

### 3️⃣ Unit Testing

* Covers:

  * Search functionality
  * Booking logic
  * Seat availability validation
* Uses **xUnit** with repository mocking or InMemoryDatabase.

---

## 🧰 Setup & Run Instructions

### 🗄️ Backend (.NET)

1. Clone the repository:

   ```bash
   git clone https://github.com/shadr862/Ticket-Reservation-System.git
   cd Ticket-Reservation-System
   ```
2. Navigate to backend project:

   ```bash
   cd Ticket-Reservation-System-API
   ```
3. Update your PostgreSQL connection string in `appsettings.json`.
4. Apply migrations and run the server:
   ```bash
   dotnet ef database update
   dotnet run
   ```
5. Backend should run on `https://localhost:44358` (or configured port).

---

### 💻 Frontend (Angular)

1. Navigate to Angular client:

   ```bash
   cd ClientApp
   ```
2. Install dependencies:

   ```bash
   npm install
   ```
3. Run the Angular app:

   ```bash
   ng serve
   ```
4. Access it at:
   👉 `http://localhost:4200`

---

## 🧪 Testing

Run backend unit tests:

```bash
dotnet test
```

---

## 🎥 Video Walkthrough

A **3–5 minute screen recording** is included demonstrating:

1. Architecture overview (Clean Architecture layers)
2. System workflow (Search → View Seats → Booking)
3. Unit test execution

---

## 📜 License

This project is created for educational purposes as part of a software architecture and development assignment.

---

**Author:** [Riaz Mehadi](https://www.linkedin.com/in/riaz-mehedi-7584031a3)
**GitHub:** [shadr862](https://github.com/shadr862)
