using Character;
using Core;
using Factories.Interfaces;
using Inputs.Interfaces;
using Utils;
using Object = UnityEngine.Object;

namespace Factories
{
    public class MainCharacterFactory: IFactory<MainCharacter>
    {
        private readonly CharacterConfig _config;
        private readonly Updater _updater;
        private readonly IMouseService _mouseService;
        private readonly CameraFollow _cameraFollow;
        
        public MainCharacterFactory(CharacterConfig config, Updater updater, IMouseService mouseService, CameraFollow cameraFollow)
        {
            _config = config;
            _updater = updater;
            _mouseService = mouseService;
            _cameraFollow = cameraFollow;
        }
        
        public MainCharacter Create()
        {
            CharacterView characterView = Object.Instantiate(_config.characterPrefab);

            MainCharacter character = new MainCharacter(characterView, _config.characterModel, _updater, _mouseService, _cameraFollow);
            return character;
        }
    }
}