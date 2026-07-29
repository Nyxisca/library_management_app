# Library Management System

A C# console application that models a real-world library workflow. It handles book inventory management, patron registrations, borrowing/returning processes, and persistent data storage using JSON files.

---

## Features

* **Inventory & Book Search:** Display the entire bookshelf, search books by title, and check real-time availability.
* **Patron Management:** Register standard and premium patrons, track card numbers, and list active members.
* **Borrow & Return System:** Link books to specific patrons upon check-out and update availability instantly upon return.
* **Object-Oriented Design:** Demonstrates key OOP principles including inheritance (`Patron` vs. `PremiumPatron`), polymorphism, and encapsulation.
* **JSON Data Persistence:** Automatically loads database files (`library_books.json` and `library_patrons.json`) on startup and saves updated states upon exit using `System.Text.Json`.

---

## Tech Stack

* **Language:** C#
* **Framework:** .NET Console Application
* **Serialization:** `System.Text.Json`

---

## Project Architecture

* **`Book`**: Stores book titles, authors, and availability status.
* **`Patron`**: Manages basic patron details and their list of borrowed books.
* **`PremiumPatron`**: Inherits from `Patron`, adding custom membership levels and specialized borrow behavior.
* **`Library`**: Holds the overall bookshelf state and provides display operations.

---

## How to Run

1. Ensure the [.NET SDK](https://dotnet.microsoft.com/) is installed on your system.
2. Clone this repository:
   ```bash
   git clone [https://github.com/Nyxisca/library-management-system.git](https://github.com/Nyxisca/library-management-system.git)
