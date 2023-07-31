using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : BaseController
{
    public AuthenticationController(IMediator mediator) 
        : base(mediator)
    {
    }
}

