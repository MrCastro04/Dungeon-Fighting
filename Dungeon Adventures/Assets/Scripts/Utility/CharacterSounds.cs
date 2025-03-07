using System;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct CharacterSounds
    {
        [SerializeField] private Actions _actionType;
        [SerializeField] private AudioClip _actionClip;

        public Actions ActionType => _actionType;
        public AudioClip ActionClip => _actionClip;
    }
}
