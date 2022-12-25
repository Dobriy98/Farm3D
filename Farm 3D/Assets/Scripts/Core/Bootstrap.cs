using Character;
using Counters;
using Utils;
using Crops;
using Factories;
using Factories.Interfaces;
using Farm;
using Inputs;
using Tiles;
using UI;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private FarmGenerationConfig farmGenerationConfig;
        [SerializeField] private CharacterConfig characterConfig;
        [SerializeField] private CropsConfigs cropsConfigs;
        [SerializeField] private TileConfig tileConfig;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private InputPanel inputPanel;
        [SerializeField] private Updater updater;
        [SerializeField] private GameplayCanvas gameplayCanvas;

        private CameraFollow _cameraFollow;
        
        private void Awake()
        {
            if (!mainCamera.TryGetComponent(out _cameraFollow))
            {
                Debug.LogWarning("There is no CameraFollow script on camera");
            }
            
            ExperienceCounter experienceCounter = new ExperienceCounter();
            CropCounter cropCounter = new CropCounter();
            
            ICropFactory cropFactory = new CropFactory(cropsConfigs, updater, cropCounter, experienceCounter);
            
            IFactory<MainCharacter> characterFactory = new MainCharacterFactory(characterConfig, updater, inputPanel, _cameraFollow);
            MainCharacter mainCharacter = characterFactory.Create();
            
            inputPanel.Initialize(mainCamera);
            gameplayCanvas.Initialize(cropCounter, experienceCounter);
            
            ITileFactory tileFactory = new TileFactory(tileConfig, mainCharacter, cropFactory, _cameraFollow);
            
            TileRaycastHandler tileRaycastHandler = new TileRaycastHandler(inputPanel);
            
            IFarmGeneration farmGenerator = new FarmGenerator(farmGenerationConfig, tileFactory);

            Level level = new Level(farmGenerator, mainCharacter, _cameraFollow);
            level.Start();
        }
    }
}
