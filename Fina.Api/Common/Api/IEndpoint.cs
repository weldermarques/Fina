namespace Fina.Api.Common.Api;

public interface IEndpoint
{
    abstract static void Map(IEndpointRouteBuilder app);
}
