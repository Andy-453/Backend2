using Backend2.DTO;
using Backend2.Model;
using Backend2.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend2.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
        
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> GetUsersByIdAsync(int id);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IPasswordHasher<Users> passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }


        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            return await _userRepository.GetUsersByIdAsync(id);
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByEmailAsync(loginDTO.Email);
            if (user == null) throw new Exception("Usuario no encontrado");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);
            if (result == PasswordVerificationResult.Failed) throw new Exception("Contraseña incorrecta");

            //Generar token
            var token = GenerateJwtToken(user);
            return token;
        }

        private string GenerateJwtToken(Users users)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDTO.Email);
            if (existingUser != null) throw new Exception("Usuario ya existe");

            var user = new Users
            {
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                Password = registerDTO.Password,
            };
            user.Password = _passwordHasher.HashPassword(user, registerDTO.Password);

            await _userRepository.CreateUsersAsync(user);
            return "Usuario registrado con exito";
        }
    }
}
