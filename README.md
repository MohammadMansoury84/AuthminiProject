
<h1 align="center">🔐 AuthMiniProject</h1>
<p align="center"><b>ASP.NET Core 8 Authentication REST API</b></p>

<p>
<span class="tag">C#</span>
<span class="tag">ASP.NET Core 8</span>
<span class="tag">Entity Framework Core</span>
<span class="tag">MySQL</span>
<span class="tag">JWT</span>
</p>

<h2>📌 Project Overview</h2>
<p>
AuthMiniProject is a backend authentication system developed with ASP.NET Core 8.
The project implements a complete authentication workflow including user registration,
email verification, login, JWT token generation, forgot password and password reset.
</p>

<p>
The main purpose of this project is demonstrating secure backend development,
layered architecture, database communication using Entity Framework Core and
token based authentication.
</p>

<h2>✨ Main Features</h2>
<ul>
<li><b>User Registration</b> - Creating new accounts with validation.</li>
<li><b>Password Security</b> - Passwords are hashed before storing using BCrypt.</li>
<li><b>Email Verification System</b> - Users receive verification tokens before activating accounts.</li>
<li><b>JWT Authentication</b> - Secure access tokens for authenticated users.</li>
<li><b>Login System</b> - Credential validation and token generation.</li>
<li><b>Forgot Password</b> - Generate password recovery tokens.</li>
<li><b>Password Reset</b> - Secure password update process.</li>
<li><b>Token Management</b> - Store, validate and expire security tokens.</li>
<li><b>Database Migration</b> - Automatic database schema management with EF Core.</li>
<li><b>Dependency Injection</b> - Service registration and clean code separation.</li>
</ul>

<h2>🏗️ Architecture</h2>
<pre>
AuthMiniProject
│
├── Controllers
│   └── AuthController.cs
│       Handles HTTP requests and authentication endpoints
│
├── DTOs
│   ├── RegisterDto
│   ├── LoginDto
│   ├── VerifyEmailDto
│   ├── ForgotPasswordDto
│   └── ResetPasswordDto
│
├── Entity
│   ├── User.cs
│   └── UserToken.cs
│
├── Services
│   ├── JwtProvider
│   ├── PasswordHasher
│   └── TokenService
│
├── db
│   └── AppDbContext
│
└── Migrations
    Database migration history
</pre>

<h2>🔄 Authentication Workflow</h2>

<h3>1. Registration</h3>
<p>
The client sends registration information. Input data is validated through DTO models.
The password is converted into a secure hash and the user account is created.
</p>

<h3>2. Email Verification</h3>
<p>
After registration, a verification token is created.
The token has an expiration time and must be validated before account activation.
</p>

<h3>3. Login</h3>
<p>
The system checks email and password.
After successful authentication, JwtProvider generates an access token.
</p>

<h3>4. Password Recovery</h3>
<p>
Users can request a recovery token and reset their password through a secure flow.
</p>

<h2>🗄️ Database Structure</h2>

<table>
<tr><th>Entity</th><th>Description</th></tr>
<tr><td>User</td><td>Stores user information including email, password hash, role and verification status.</td></tr>
<tr><td>UserToken</td><td>Stores verification and password reset tokens with expiration information.</td></tr>
</table>

<h2>🔐 Security Features</h2>
<ul>
<li>Plain text passwords are never stored.</li>
<li>BCrypt password hashing.</li>
<li>JWT signed authentication tokens.</li>
<li>Token expiration control.</li>
<li>DTO based input validation.</li>
<li>Separated authentication logic using services.</li>
</ul>

<h2>🌐 API Endpoints</h2>

<table>
<tr><th>Method</th><th>Route</th><th>Purpose</th></tr>
<tr><td>POST</td><td>/Auth/register</td><td>Create a new account</td></tr>
<tr><td>POST</td><td>/Auth/verify-email</td><td>Activate account</td></tr>
<tr><td>POST</td><td>/Auth/login</td><td>Authenticate user and return JWT</td></tr>
<tr><td>POST</td><td>/Auth/forgot-password</td><td>Create reset token</td></tr>
<tr><td>POST</td><td>/Auth/reset-password</td><td>Change password</td></tr>
</table>

<h2>🛠 Technologies</h2>
<table>
<tr><th>Technology</th><th>Usage</th></tr>
<tr><td>C#</td><td>Backend programming language</td></tr>
<tr><td>ASP.NET Core 8</td><td>REST API framework</td></tr>
<tr><td>Entity Framework Core</td><td>ORM and database access</td></tr>
<tr><td>MySQL</td><td>Relational database</td></tr>
<tr><td>JWT</td><td>Authentication mechanism</td></tr>
<tr><td>BCrypt</td><td>Password protection</td></tr>
</table>

<h2>🚀 Running Project</h2>
<pre>
git clone repository-url

cd AuthMiniProject

dotnet restore

dotnet ef database update

dotnet run
</pre>

<h2>📚 Learning Objectives</h2>
<ul>
<li>Building REST APIs with ASP.NET Core</li>
<li>Implementing authentication systems</li>
<li>Working with JWT security</li>
<li>Database design with Entity Framework Core</li>
<li>Applying service-based architecture</li>
</ul>

<h2>👨‍💻 Conclusion</h2>
<p>
AuthMiniProject is a complete authentication backend example suitable for learning
and demonstrating modern ASP.NET Core security practices.
</p>

</body>
</html>
