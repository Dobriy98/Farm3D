using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerUI : MonoBehaviour
    {
        public event Action OnTimerIsReady;
        
        private const float MinTimerValue = 0;
        
        private TextMeshProUGUI _textMesh;
        private float _timerValue;

        private bool _activeTimer;
        private bool _timerIsReady;

        private const string ReadyWord = "Ready!";

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        public void StopTimer() => _activeTimer = false;

        public void StartTimer(float seconds)
        {
            _timerValue = seconds;
            _activeTimer = true;
            _timerIsReady = false;
            _textMesh.text = _timerValue.ToString("0.0");
        }

        private void Update()
        {
            if (_timerValue >= MinTimerValue && _activeTimer)
            {
                _timerValue -= Time.deltaTime;
                _textMesh.text = _timerValue.ToString("0.0");
            } else if (_timerValue <= MinTimerValue && !_timerIsReady)
            {
                _timerIsReady = true;
                _textMesh.text = ReadyWord;
                StopTimer();
                OnTimerIsReady?.Invoke();
            }
        }
    }
}