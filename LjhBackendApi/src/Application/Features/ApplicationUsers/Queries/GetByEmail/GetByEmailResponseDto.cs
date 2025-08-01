using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands;
using LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetApplicationUser;

namespace LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetByEmail;
public class GetByEmailResponseDto : ApplicationUserDto
{
#pragma warning disable 8632
    //public string Password { get; set; }
    //assume will not have ppl able to see password value
}
