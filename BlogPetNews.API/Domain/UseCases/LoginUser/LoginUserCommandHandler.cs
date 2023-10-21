using AutoMapper;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.LoginUser
{
  public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
  {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;


    public LoginUserCommandHandler(IUserService userService, TokenService tokenService, IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
      _tokenService = tokenService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
      var token = Login(request.Email, request.Password);
      return new LoginUserCommandResponse { Token = token, Success = true };
    }

    private string Login(string email, string password)
    {
      var user = _userService.Login(email, password);
      if (user is not null)
      {
        var token = _tokenService.GenerateToken(_mapper.Map<User>(user));
        return token;
      }
      return "Unauthorized Access";
    }
  }
}
