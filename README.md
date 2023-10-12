# Discussions - A Discussion Forums Application 🗣️💬

Discussions is a web-based discussion forums application built using .NET Core 7, C#, and Microsoft Entity Framework. It allows users to create, update, and delete discussions and comments. Users can also log in and store login information in sessions. 🌐🔒

<img width="1366" alt="Screenshot 2023-09-06 at 16 53 25" src="https://github.com/MathiasCK/Discussions/assets/26365473/9a7d6470-ed33-465a-991b-11ce0d935bc3">

## Features 🚀

- User Registration and Login: Users can create accounts, log in, and stay authenticated using sessions. 👤🔐
- Discussion Creation: Registered users can create new discussion topics. 📝
- Discussion Management: Users can edit and delete their own discussions. ✏️🗑️
- Comment System: Users can leave comments on discussions. 💬
- Comment Management: Users can edit and delete their own comments. ✏️🗑️
- Responsive Design: The application is designed to work well on various screen sizes. 📱💻

## Technologies Used 💻

- .NET Core 7
- C#
- Entity Framework Core
- ASP.NET Core MVC
- HTML/CSS
- Bootstrap

## Getting Started 🏁

Follow these instructions to set up and run the Discussions application locally on your development machine.

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet) (v7.0.307) - Verify by running dotnet --version ✔️

### Installation 💽

1. Clone the repository:

   ```shell
   git clone https://github.com/MathiasCK/Discussions.git
   ```

2. Navigate to project folder:

   ```shell
   cd Discussions
   ```

3. Install dependencies:

   ```shell
   dotnet restore
   ```

4. Build the application:

   ```shell
   dotnet build
   ```
   
## Running the Project 🚀

### Development Mode 🔧

1. **Start the application**:

   ```bash
   dotnet run
   ```

The application will be accessible at [http://localhost:5124](http://localhost:5124)

### Production Mode 🌐

1. **Publish the application**:

   ```bash
   dotnet publish -c Release
   ```

2. **Run the production ready application**:

   ```bash
   cd bin/Release/net7.0/publish && dotnet Discussions.dll
   ```

The production ready application will be accessible at [http://localhost:5000](http://localhost:5000)

## Usage 📝

Visit the application in your web browser.
Create an account or log in.
Start creating discussions and participating in conversations.

## Contributing 🤝

If you'd like to contribute to this project, please follow these guidelines:

- Fork the repository.
- Create a new branch for your feature or bug fix.
- Commit your changes with clear messages.
- Push your changes to your fork.
- Create a pull request to the main branch of the original repository.

## License 📄

This project is licensed under the MIT License.
