using System;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct CharacterSounds
    {
        [SerializeField] private SoundActionType _actionType;
        [SerializeField] private AudioClip _actionClip;

        public SoundActionType ActionType => _actionType;
        public AudioClip ActionClip => _actionClip;
    }
}
