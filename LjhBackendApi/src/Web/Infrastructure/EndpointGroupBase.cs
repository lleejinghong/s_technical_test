using Microsoft.AspNetCore.Mvc;

namespace LjhBackendApi.Web.Infrastructure;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
