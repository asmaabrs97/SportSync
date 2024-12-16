# SportSync - Sports Management Application

SportSync is a modern web application built with ASP.NET Core that helps manage sports activities, registrations, and user sessions. It provides a seamless experience for both athletes and administrators.

## Features

- **User Authentication**
  - Secure login and registration system
  - User profile management
  - Role-based access control (Admin, Coach, User)

- **Sports Management**
  - Browse different sports disciplines
  - View detailed information about each sport
  - Filter sports by type, level, and skill requirements

- **Session Management**
  - Schedule and manage training sessions
  - Track attendance
  - Manage registrations

- **Payment System**
  - Secure payment processing
  - Multiple payment methods support
  - Payment history tracking

- **Document Management**
  - Upload and manage medical certificates
  - Store important documents
  - Track document status

## Technology Stack

- **Backend**
  - ASP.NET Core 7.0
  - Entity Framework Core
  - SQL Server Database
  - Identity Framework for Authentication

- **Frontend**
  - Razor Views
  - Bootstrap 5
  - jQuery
  - Modern responsive design

## Getting Started

1. **Prerequisites**
   - .NET 7.0 SDK
   - SQL Server
   - Visual Studio 2022 or VS Code

2. **Installation**
   ```bash
   # Clone the repository
   git clone https://github.com/asmaabrs97/SportSync.git

   # Navigate to project directory
   cd SportSync

   # Restore packages
   dotnet restore

   # Update database
   dotnet ef database update

   # Run the application
   dotnet run
   ```

3. **Configuration**
   - Update the connection string in `appsettings.json`
   - Configure any additional settings in `appsettings.json`

## Project Structure

- `Controllers/` - Application controllers
- `Models/` - Data models and view models
- `Views/` - Razor views and layouts
- `Services/` - Business logic and services
- `Data/` - Database context and repositories
- `wwwroot/` - Static files (CSS, JS, images)

## Features in Development

- Advanced search functionality
- Real-time notifications
- Mobile application
- Integration with external sports APIs
- Performance analytics dashboard

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact

Project Link: [https://github.com/asmaabrs97/SportSync](https://github.com/asmaabrs97/SportSync)
