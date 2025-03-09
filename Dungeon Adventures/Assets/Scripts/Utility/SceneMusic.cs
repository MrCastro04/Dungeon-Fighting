using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
   [RequireComponent(typeof(AudioSource))]

   public class SceneMusic : MonoBehaviour
   {
      [SerializeField] private AudioSource _audioSource;
      [SerializeField] private AudioClip _sceneMusic;
      [SerializeField] private AudioClip _enterSceneSound;

      private void Start()
      {
         if (SceneManager.GetActiveScene().buildIndex == 2)
         {
            _audioSource.PlayOneShot(_enterSceneSound);
         }

         _audioSource.clip = _sceneMusic;

         _audioSource.Play();
      }
   }
}
