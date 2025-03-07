using System.Linq;
using UnityEngine;

namespace Utility
{
  public class CharacterSoundController
  {
      private CharacterSounds[] _sounds;
      private Vector3 _soundPlayPosition;

      public CharacterSoundController(CharacterSounds[] sounds , Vector3 soundPlayPosition)
      {
          _sounds = sounds;

          _soundPlayPosition = soundPlayPosition;
      }

      public void HandlerPlayActionTypeSound(SoundActionType actionType)
      {
          var foundSound = _sounds.FirstOrDefault
                  (sound => sound.ActionType == actionType);

              AudioSource.PlayClipAtPoint(foundSound.ActionClip, _soundPlayPosition);
      }
  }
}
