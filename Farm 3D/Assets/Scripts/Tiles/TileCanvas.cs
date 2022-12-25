using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Tiles
{
    [RequireComponent(typeof(Canvas))]
    public class TileCanvas: MonoBehaviour
    {
        [SerializeField] private GameObject parentButtons;

        public event Action OnTimerIsReady;
        
        private Canvas _canvas;
        private TimerUI _timerUI;
        
        private Tile _tile;
        private TileModel _tileModel;
        
        private readonly List<TileButton> _tileButtons = new List<TileButton>();

        public void Initialize(Tile tile, TileModel tileModel)
        {
            _tile = tile;
            _tileModel = tileModel;

            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;

            CreateTileButtons();
            
            CreateTimer();
            _timerUI.OnTimerIsReady += TimerReady;

            HideTimer();
            ShowCanvas(false);
        }

        public void ShowCanvas(bool show)
        {
            _canvas.enabled = show;
        }

        public void ShowButtons(bool show)
        {
            parentButtons.SetActive(show);
        }

        private void CreateTileButtons()
        {
            foreach (var cropConfig in _tileModel.cropsToSowing)
            {
                TileButton tileButton = Instantiate(_tileModel.tileUIConfig.tileButtonPrefab, parentButtons.transform);
                tileButton.Initialize(cropConfig.cropUI.cropSprite, cropConfig.cropModel.cropType);
                
                _tileButtons.Add(tileButton);
                tileButton.OnTileButtonClick += _tile.Plant;
            }
        }

        private void CreateTimer()
        {
            _timerUI = Instantiate(_tileModel.tileUIConfig.timerUIPrefab, transform);
        }

        public void StartTimer(float seconds)
        {
            _timerUI.gameObject.SetActive(true);
            _timerUI.StartTimer(seconds);
        }

        public void HideTimer()
        {
            _timerUI.gameObject.SetActive(false);
        }

        private void TimerReady()
        {
            OnTimerIsReady?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (var tileButton in _tileButtons)
            {
                tileButton.OnTileButtonClick -= _tile.Plant;
            }
            _timerUI.OnTimerIsReady -= TimerReady;
        }
    }
}