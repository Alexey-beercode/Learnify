using AuthenticationService.Models.DTOs.User.Requests;
using AuthenticationService.Models.DTOs.User.Responses;
using AuthenticationService.Models.Entities;
using AuthenticationService.Repositories.Interfaces;
using AuthenticationService.Utils;
using AutoMapper;

namespace AuthenticationService.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken=default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            throw new InvalidOperationException($"User with id : {id} are not found");
        }
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task CreateAsync(RegisterDTO registerDto, CancellationToken cancellationToken=default)
    {
        var userFromDb = await _userRepository.GetByLoginAsync(registerDto.Login);
        if (userFromDb is not null)
        {
            throw new InvalidOperationException($"User with login : {registerDto.Login} is alredy exists");
        }
        var user = _mapper.Map<User>(registerDto);
        await _userRepository.CreateAsync(user, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userFromDb = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (userFromDb is null)
        {
            throw new InvalidOperationException($"User with id : {id} are not found");
        }

        await _userRepository.DeleteAsync(userFromDb, cancellationToken);
    }

    public async Task<List<UserResponseDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<UserResponseDTO>>(users);
    }

    public async Task ChangePasswordAsync(string newPassword, string oldPassword, Guid userId, CancellationToken cancellationToken)
    {
        var userFromDb = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (userFromDb is null)
        {
            throw new InvalidOperationException($"User with id : {userId} are not found");
        }

        var isPasswordConfirm = PasswordHasher.VerifyPassword(userFromDb.PasswordHash, oldPassword);
        if (!isPasswordConfirm)
        {
            throw new UnauthorizedAccessException("Incorrect password");
        }

        userFromDb.PasswordHash = PasswordHasher.HashPassword(newPassword);
        await _userRepository.UpdateAsync(userFromDb, cancellationToken);
    }

    public async Task<List<UserResponseDTO>> GetUsersByRole(Guid roleId, CancellationToken cancellationToken)
    {
        var usersByRole = await _userRepository.GetUsersByRoleId(roleId, cancellationToken);
        return _mapper.Map<List<UserResponseDTO>>(usersByRole);
    }
    
}