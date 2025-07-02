using MediatR;
using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealership.Application.Features.Authentication
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterUserCommand, RegisterResultDto>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IJwtTokenService _jwt;

        public RegisterUserHandler(
            IUserRepository repo,
            IPasswordHasher<User> hasher,
            IJwtTokenService jwt)
        {
            _repo = repo;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<RegisterResultDto> Handle(
            RegisterUserCommand cmd,
            CancellationToken ct)
        {
            if (await _repo.ExistsByEmailAsync(cmd.Email))
                return new RegisterResultDto
                {
                    Success = false,
                    Errors = new[] { "Email already in use." }
                };

            // 1) Create a temporary user just so the hasher has a TUser instance.
            var tempUser = new User(
                cmd.FirstName, cmd.LastName, cmd.Email,
                cmd.Mobile, cmd.Address, cmd.Postcode,
                cmd.City, passwordHash: string.Empty);

            // 2) Hash the password
            var hashed = _hasher.HashPassword(tempUser, cmd.Password);

            // 3) Create the real user with the hashed password via constructor
            var user = new User(
                cmd.FirstName, cmd.LastName, cmd.Email,
                cmd.Mobile, cmd.Address, cmd.Postcode,
                cmd.City, passwordHash: hashed);

            // 4) Persist to the database
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync(ct);

            // 5) Generate a JWT for the newly‐created user
            var token = _jwt.GenerateToken(user);

            return new RegisterResultDto
            {
                Success = true,
                Token = token
            };
        }
    }
}