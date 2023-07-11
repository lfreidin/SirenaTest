namespace SirenaTestAPI.Mappers;

public interface IRouteMapper<T>
{
    public DTO.Route FromProviderRoute(T sourceRoute);
}