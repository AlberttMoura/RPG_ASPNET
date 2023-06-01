namespace RPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, OutputCharacterDTO>();
            CreateMap<InputCharacterDTO, Character>();
            CreateMap<UpdateCharacterDTO, Character>();
        }
    }
}