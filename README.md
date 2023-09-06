# Discussions - A Discussion Forums Application

Discussions is a web-based discussion forums application built using .NET Core 7, C#, and Microsoft Entity Framework. It allows users to create, update, and delete discussions and comments. Users can also log in and store login information via cookies.

<img width="1366" alt="Screenshot 2023-09-06 at 16 53 25" src="https://github.com/MathiasCK/Discussions/assets/26365473/9a7d6470-ed33-465a-991b-11ce0d935bc3">


## Features

- User Registration and Login: Users can create accounts, log in, and stay authenticated using cookies.
- Discussion Creation: Registered users can create new discussion topics.
- Discussion Management: Users can edit and delete their own discussions.
- Comment System: Users can leave comments on discussions.
- Comment Management: Users can edit and delete their own comments.
- Responsive Design: The application is designed to work well on various screen sizes.

## Technologies Used

- .NET Core 7
- C#
- Entity Framework Core
- ASP.NET Core MVC
- HTML/CSS
- Bootstrap

## Getting Started

Follow these instructions to set up and run the Discussions application locally on your development machine.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:

   ```shell
   git clone https://github.com/MathiasCK/Discussions.git
   cd Discussions
   ```
   
2. Configure the database connection in appsettings.json:

   ```json
   "ConnectionStrings": {
      "DbConnection": "your-connection-string",
   }
   ```

3. Run database migrations:

   ```shell
   dotnet ef database update
   ```

4. Build and run the application:

   ```shell
   dotnet build
   dotnet run
   ```



## Usage

Visit the application in your web browser.
Create an account or log in.
Start creating discussions and participating in conversations.

## License

This project is licensed under the MIT License.
