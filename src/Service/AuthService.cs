using Domain.Request;
using Domain.Responses;
using Infra.Repositories;

namespace Service
{
    public interface IAuthService
    {
        Task<AuthResponse> SignIn(AuthRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IHashingService _hashingService;
        private readonly IUserRepository _userRepository;

        private const string InvalidLoginMessage = "Login is invalid!";

        public AuthService(IUserRepository userRepository, IHashingService hashingService, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _hashingService = hashingService;
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> SignIn(AuthRequest request)
        {
            var user = await _userRepository.FindByUsernameAsync(request.Email!);

            if (user is null)
                throw new UnauthorizedAccessException(InvalidLoginMessage);

            var isPasswordValid = _hashingService.Verify(request.Password!, user.Password!);

            if (!isPasswordValid)
                throw new UnauthorizedAccessException(InvalidLoginMessage);

            var token = _jwtService.CreateToken(user);
            return new AuthResponse
            {
                Jwt = token,
            };
        }
    }
}
