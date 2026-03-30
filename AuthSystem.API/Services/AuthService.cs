using AuthSystem.API.Data;
using AuthSystem.API.DTOs;
using Microsoft.EntityFrameworkCore;
using AuthSystem.API.Models;

namespace AuthSystem.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _PasswordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            AppDbContext context, 
            IPasswordHasher passwordHasher, 
            ITokenService tokenService)
        {
            _context = context;
            _PasswordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // check if username already exists
            var existingUser = await _context.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email); 

            if (existingUser)
            {
                throw new Exception("Username or email already exists.");
            }

            // create new user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _PasswordHasher.HashPassword(request.Password)
            };

            // save user to database

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // generate token for the new user and return response
            var token = _tokenService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                ExpiresAt = DateTime.UtcNow.AddHours(1) // token expiration time
            };
        }

        public  async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
           // find user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            //if user not found or password, throw unathorized
            if (user == null || !_PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }

            //verify password agaínst stored hash
            var isPasswordValid = _PasswordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid username or password.");
            }

            //Generate JWT token 
            var token = _tokenService.GenerateToken(user);

            //return auth response with token and user info
            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                ExpiresAt = DateTime.UtcNow.AddHours(1) // token expiration time
            };

        }

    }
}
