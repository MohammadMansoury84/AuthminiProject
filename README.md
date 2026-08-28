🔐 AuthMiniProject

Overview

AuthMiniProject is a secure authentication REST API developed with
ASP.NET Core 8.
The project implements a complete user authentication workflow including
registration, email verification, login with JWT authentication,
password recovery, and password reset.

The main goal of this project is to provide a clean and modular
authentication backend using modern .NET technologies.

✨ Features

👤 User Registration

Create a new user account using email and password.

Validate email format.

Validate password complexity.

Prevent duplicate email registration.

Store passwords securely using BCrypt hashing.

📧 Email Verification

After registration:

A six-digit verification code is generated.

The code is stored in the database.

The user verifies their email using the code.

Expired or already-used codes are rejected.

🔑 Login System

The login process includes:

Email and password validation.

Checking email verification status.

Password hash verification.

Generating JWT access tokens after successful authentication.

🔄 Forgot Password

Users can request a password recovery code:

Generate a temporary reset token.

Store token information.

Validate expiration time.

Prevent token reuse.

🔐 Password Reset

Users can change their password after successful token verification.

🏗️ Architecture

The project follows a layered architecture:

AuthMiniProject
│
├── Controllers
│   └── AuthController
│
├── DTOs
│   ├── RegisterDto
│   ├── LoginDto
│   ├── VerifyEmailDto
│   ├── ForgotPasswordDto
│   └── ResetPasswordDto
│
├── Entity
│   ├── User
│   └── UserToken
│
├── Services
│   ├── JwtProvider
│   ├── TokenService
│   ├── PasswordHasher
│   └── Interfaces
│
├── db
│   └── AppDbContext
│
└── Migrations

🧩 Project Components

Controllers

AuthController

Responsible for handling authentication endpoints:

Register

Verify Email

Login

Forgot Password

Reset Password

DTO Layer

DTOs are used to transfer and validate incoming API data.

Available DTOs:

DTO                 Purpose

RegisterDto         User registration
LoginDto            User login
VerifyEmailDto      Email confirmation
ForgotPasswordDto   Password recovery request
ResetPasswordDto    Password change

Entity Layer

User

Stores user information:

Id

Email

Password hash

Role

Email verification status

Creation date

UserToken

Stores temporary security tokens:

Verification codes

Password reset codes

Expiration time

Usage status

🔒 Security Implementation

Password Security

Passwords are never stored as plain text.

The project uses:

BCrypt hashing algorithm

Secure password verification

JWT Authentication

After successful login, the server generates a JWT token containing:

User ID

Email

Role

Token expiration:

2 hours

Token Management

Temporary tokens include:

Token type

Expiration time

Used/Unused status

User relationship

Supported token types:

EmailVerification
ResetPassword

🗄️ Database

The project uses:

MySQL

Entity Framework Core

Pomelo MySQL Provider

Database entities:

Users
 |
 └── UserTokens

Relationship:

User (1) -------- (*) UserToken

🛠️ Technologies Used

Technology              Usage

C#                      Programming language
ASP.NET Core 8          Web API framework
Entity Framework Core   ORM
MySQL                   Database
JWT                     Authentication
BCrypt                  Password hashing
Swagger                 API documentation

⚙️ Installation

Requirements

Install:

.NET 8 SDK

MySQL Server

Clone Repository

git clone <repository-url>
cd AuthminiProject-master

Configure Database

Edit:

AuthMiniProject/appsettings.json

Update:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=AuthMiniProject;User=root;Password=your_password;"
}

Apply Migration

Run:

dotnet ef database update

Run Project

dotnet run

Swagger will be available at:

/swagger

🔌 API Endpoints

Register

POST /Auth/register

Request:

{
  "email": "user@example.com",
  "password": "Password@123",
  "confirmPassword": "Password@123"
}

Verify Email

POST /Auth/verify-email

Login

POST /Auth/login

Response:

{
  "token": "JWT_TOKEN"
}

Forgot Password

POST /Auth/forgot-password

Reset Password

POST /Auth/reset-password

📌 Future Improvements

Possible improvements:

Connect email provider for real verification emails.

Replace test debug codes with email delivery.

Add refresh tokens.

Add role-based authorization.

Add rate limiting.

Add unit tests.

Add Docker support.

👨‍💻 Author

AuthMiniProject - Authentication API built with ASP.NET Core 8.
