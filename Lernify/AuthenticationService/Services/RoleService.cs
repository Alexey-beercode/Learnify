using AuthenticationService.Models.DTOs.User.Responses;
using AuthenticationService.Repositories.Interfaces;
using AutoMapper;

namespace AuthenticationService.Services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<RoleResponseDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<RoleResponseDTO>>(roles);
    }

    public async Task SetRoleToUser(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        await _roleRepository.SetRoleToUser(userId, roleId, cancellationToken);
    }

    public async Task RemoveRoleFromUser(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        await _roleRepository.RemoveRoleFromUser(userId, roleId, cancellationToken);
    }

    public async Task<List<RoleResponseDTO>> GetRolesByUser(Guid userId, CancellationToken cancellationToken)
    {
        var rolesByUser = await _roleRepository.GetRolesByUserId(userId, cancellationToken);
        return _mapper.Map<List<RoleResponseDTO>>(rolesByUser);
    }

    public async Task<RoleResponseDTO> GetById(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
        if (role is null)
        {
            throw new InvalidOperationException($"Role with id : {id} are not found");
        }

        return _mapper.Map<RoleResponseDTO>(role);
    }
}