using System.Linq;
using Core;
using UnityEngine;

namespace Utility
{
  public class CharacterSoundController : MonoBehaviour
  {
      [SerializeField] private CharacterSounds[] _sounds;

      private void OnEnable()
      {
          EventManager.OnSoundHit += HandlerPlayActionTypeSound;
          EventManager.OnSoundUsePotion += HandlerPlayActionTypeSound;
          EventManager.OnSoundDefeat += HandlerPlayActionTypeSound;
      }

      private void OnDisable()
      {
          EventManager.OnSoundHit -= HandlerPlayActionTypeSound;
          EventManager.OnSoundUsePotion += HandlerPlayActionTypeSound;
          EventManager.OnSoundDefeat -= HandlerPlayActionTypeSound;
      }

      private void HandlerPlayActionTypeSound(Actions actionType)
      {
          var foundSound = _sounds.FirstOrDefault
                  (sound => sound.ActionType == actionType);

          AudioSource.PlayClipAtPoint(foundSound.ActionClip, transform.position);
      }
  }
}
