using AutoMapper;

public class MappingProile : Profile
{
    public MappingProile()
    {
        CreateMap<Channel, GetAllChannelsQueryResult>();
        CreateMap<Channel, GetChannelByIdQueryResult>();
        CreateMap<Channel, GetChannelDetailQueryResult>();
    }

}