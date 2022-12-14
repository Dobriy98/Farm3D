namespace Character.Interfaces
{
    public interface ICharacter : IMoveable, IPlant, ICollect
    {
        public CharacterView CharacterView { get; set; }
        public CharacterModel CharacterModel { get; set; }

        public void Initialize();
    }
}