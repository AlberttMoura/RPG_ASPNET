namespace RPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character{Id = 0, Name = "Joao", Class = RpgClass.Mage},
            new Character{ Id = 1, Name = "James"}
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<List<OutputCharacterDTO>>> AddCharacter(InputCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<OutputCharacterDTO>>();
            var dbCharacters = _context.Characters;
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = dbCharacters.Max(c => c.Id) + 1;
            dbCharacters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<OutputCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OutputCharacterDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<OutputCharacterDTO>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<OutputCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<OutputCharacterDTO>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<OutputCharacterDTO>();
            try
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (dbCharacter is null)
                {
                    throw new Exception($"Character with Id {id} not found!");
                }
                serviceResponse.Data = _mapper.Map<OutputCharacterDTO>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<OutputCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<OutputCharacterDTO>();
            try
            {
                var dbCharacters = await _context.Characters.ToListAsync();
                var dbCharacter = dbCharacters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (dbCharacter is null)
                {
                    throw new Exception($"Character with Id {updatedCharacter.Id} not found!");
                }
                _mapper.Map(updatedCharacter, dbCharacter);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<OutputCharacterDTO>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OutputCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<OutputCharacterDTO>>();

            try
            {
                var dbCharacters = _context.Characters;
                var dbCharacter = await dbCharacters.FirstOrDefaultAsync(c => c.Id == id);
                if (dbCharacter is null)
                {
                    throw new Exception($"Character with Id {id} not found!");
                }

                dbCharacters.Remove(dbCharacter);
                await _context.SaveChangesAsync();
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<OutputCharacterDTO>(c)).ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}