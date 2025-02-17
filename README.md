# BookHaven: Online Library Management System

Welcome to **BookHaven**, an advanced online library management system designed to simplify and streamline the borrowing and purchasing of books. With a clean, modern interface, this platform offers a smooth user experience for both customers and administrators to manage their books, sales, and customer interactions.

## Features

- **Book Inventory Management**: Track available books, including their genre, author, price, and availability.
- **Book Borrowing**: Customers can borrow books, track their borrowings, and manage their returns.
- **Sales Management**: Keep records of book sales, with detailed information on each transaction.
- **Customer Accounts**: Users can register, log in, and view their borrowing history and current borrowed books.
- **Book Recommendations**: Suggest books based on customers' borrowing history and preferences.
- **Inventory Updates**: Automatic updates on book stock after borrowing or sales.

## Tech Stack

### Frontend:
- **React**: For building the user interface.
- **TypeScript**: Ensuring type safety and maintainability across the app.
- **Tailwind CSS**: For a responsive, customizable design.
- **Ant Design**: For UI components that enhance the user experience.
- **React Redux**: For global state management and handling complex application state.

### Backend:
- **C# Web API**: RESTful APIs to handle all business logic.
- **SQL Server (MSSQL)**: For managing database and storing all related data for books, customers, and transactions.

### Database Schema

#### Core Entities:
- **Books**: Information about books, including title, ISBN, published year, availability, and price.
- **Customers**: Details of customers, including name, email, password, and profile information.
- **Borrowings**: Records of borrowed books, including borrow and return dates.
- **Sales**: Book sales records, including transaction amounts and customer details.
- **Authors**: Authors of books, with birth dates and other details.
- **Genres**: Categories for organizing books based on their genres.

## Installation

### Prerequisites:
- Node.js (for the frontend)
- .NET 8 (for the backend)
- SQL Server (for the database)

### Clone the Repository:

```bash
git clone https://github.com/your-username/BookHaven.git
cd BookHaven
