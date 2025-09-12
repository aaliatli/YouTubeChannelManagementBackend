using Mapster;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Channel, GetAllChannelsQueryResult>.NewConfig();
        TypeAdapterConfig<Channel, GetChannelByIdQueryResult>.NewConfig();
        TypeAdapterConfig<Channel, GetChannelDetailQueryResult>.NewConfig();
    }
}
