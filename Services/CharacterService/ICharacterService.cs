namespace RPG.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<OutputCharacterDTO>>> GetAllCharacters();

        Task<ServiceResponse<OutputCharacterDTO>> GetCharacterById(int id);

        Task<ServiceResponse<List<OutputCharacterDTO>>> AddCharacter(InputCharacterDTO newCharacter);
    }
}