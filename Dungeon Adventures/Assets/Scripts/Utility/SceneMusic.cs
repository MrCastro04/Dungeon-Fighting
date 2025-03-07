using UnityEngine;

namespace Utility
{
   public class SceneMusic : MonoBehaviour
   {
      [SerializeField] private AudioSource _audioSource;
      [SerializeField] private AudioClip _clip;

      private void Start()
      {
         _audioSource.clip = _clip;

         _audioSource.Play();
      }
   }
}
