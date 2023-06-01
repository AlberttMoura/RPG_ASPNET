namespace RPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character{Id = 0, Name = "Joao", Class = RpgClass.Mage},
            new Character{ Id = 1, Name = "James"}
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }

        public async Task<ServiceResponse<List<OutputCharacterDTO>>> AddCharacter(InputCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<OutputCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<OutputCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OutputCharacterDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<OutputCharacterDTO>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<OutputCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<OutputCharacterDTO>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<OutputCharacterDTO>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<OutputCharacterDTO>(character);
            return serviceResponse;
        }
    }
}