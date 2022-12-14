using Character.Interfaces;
using Utils;
using Farm;

namespace Core
{
    public class Level
    {
        private readonly IFarmGeneration _farmGeneration;
        private readonly ICharacter _character;
        private readonly CameraFollow _cameraFollow;
        
        public Level(IFarmGeneration farmGeneration, ICharacter character, CameraFollow cameraFollow)
        {
            _farmGeneration = farmGeneration;
            _character = character;
            _cameraFollow = cameraFollow;
        }

        public void Start()
        {
            _farmGeneration.Generate();
            
            _character.Initialize();

            _cameraFollow.State = _character.CharacterModel.characterCameraState;
        }
    }
}