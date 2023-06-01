namespace RPG
{
    public class UpdateCharacterDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; } = "Knight";

        public int HitPoints { get; set; } = 10;

        public int Strength { get; set; } = 14;

        public int Defense { get; set; } = 12;

        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}