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
          EventManager.OnHit += HandlerOnHit;
          EventManager.OnUsePotion += HandlerOnUsePotion;
      }

      private void OnDisable()
      {
          EventManager.OnHit -= HandlerOnHit;
          EventManager.OnUsePotion += HandlerOnUsePotion;
      }

      private void HandlerOnHit(Actions actionType)
      {
         PlayActionTypeSound(actionType);
      }

      private void HandlerOnUsePotion(Actions actionType)
      {
          PlayActionTypeSound(actionType);
      }

      private void PlayActionTypeSound(Actions actionType)
      {
          var foundSound = _sounds.FirstOrDefault
              (sound => sound.ActionType == actionType);

          if (foundSound.ActionType != null)
          {
              AudioSource.PlayClipAtPoint(foundSound.ActionClip, transform.position);
          }
      }
  }
}
