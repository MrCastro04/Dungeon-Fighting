using System;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(AnimationCurve))]
        public class TorchLight : MonoBehaviour
        {
            [SerializeField] private AnimationCurve _animationCurve;

            private float _currentTime;
            private float _finalTime;
            private Light _light;

            private void Awake()
            {
                _light = GetComponentInChildren<Light>();

                _finalTime = _animationCurve.keys[_animationCurve.keys.Length - 1].time;
            }

            private void Update()
            {
                _light.intensity = _animationCurve.Evaluate(_currentTime);

                _currentTime += Time.deltaTime;

                if (_currentTime >= _finalTime)
                    _currentTime = 0f;
            }
        }
}