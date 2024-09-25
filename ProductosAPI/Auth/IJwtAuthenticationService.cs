using System;

namespace ProductosAPI.Auth;

public interface IJwtAuthenticationService
{
    string Authenticate(string userName);
}
