using System.Collections.Generic;
using Core;
using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(AudioSource))]
  public class CharacterSoundController : MonoBehaviour
  {
     [SerializeField] private CharacterSounds[] _sounds;

      private Dictionary < SoundActionType, AudioClip > _soundsDictionary;
      private AudioSource _soundPlayPosition;

      private void Awake()
      {
          _soundPlayPosition = GetComponent<AudioSource>();

          _soundsDictionary = new();

          foreach (var sound in _sounds)
          {
              if (_soundsDictionary.ContainsKey(sound.ActionType) == false)
              {
                  _soundsDictionary.Add(sound.ActionType, sound.ActionClip);
              }
          }
      }

      private void OnEnable()
      {
          EventManager.OnSoundAbilityWindCut += HandlerPlayActionTypeSound;
          EventManager.OnPlayerGetItem += HandlerPlayActionTypeSound;
          EventManager.OnSoundHit += HandlerPlayActionTypeSound;
          EventManager.OnSoundUsePotion += HandlerPlayActionTypeSound;
          EventManager.OnSoundDefeat += HandlerPlayActionTypeSound;
          EventManager.OnSoundMissHit += HandlerPlayActionTypeSound;
      }

      private void OnDisable()
      {
          EventManager.OnSoundAbilityWindCut -= HandlerPlayActionTypeSound;
          EventManager.OnPlayerGetItem -= HandlerPlayActionTypeSound;
          EventManager.OnSoundHit -= HandlerPlayActionTypeSound;
          EventManager.OnSoundUsePotion -= HandlerPlayActionTypeSound;
          EventManager.OnSoundDefeat -= HandlerPlayActionTypeSound;
          EventManager.OnSoundMissHit -= HandlerPlayActionTypeSound;
      }

      public void HandlerPlayActionTypeSound(ItemSO item)
      {
          _soundPlayPosition.PlayOneShot(item.CollectSound);
      }

      public void HandlerPlayActionTypeSound(SoundActionType actionType, IControllerType controllerType)
      {
          if (controllerType != this.GetComponent<IControllerType>())
          {
              return;
          }

          if (_soundsDictionary.TryGetValue(actionType, out AudioClip anyClip))
          {
              _soundPlayPosition.PlayOneShot(anyClip);
          }
      }
  }
}
