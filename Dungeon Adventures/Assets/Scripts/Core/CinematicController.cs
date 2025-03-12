using Uitility;
using UnityEngine;
using UnityEngine.Playables;

namespace Core
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(PlayableDirector))]

    public class CinematicController : MonoBehaviour
    {
        private BoxCollider _boxColliderCmp;
        private PlayableDirector _playableDirector;

        private void Awake()
        {
            _boxColliderCmp = GetComponent<BoxCollider>();

            _playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            _playableDirector.played += HandlePlayed;
            _playableDirector.stopped += HandleStopped;
        }

        private void OnDisable()
        {
            _playableDirector.played -= HandlePlayed;
            _playableDirector.stopped -= HandleStopped;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(Constants.TAG_PLAYER) == false) return;

            _boxColliderCmp.enabled = false;

            _playableDirector.Play();
        }

        private void HandlePlayed(PlayableDirector pd)
        {
            EventManager.RaiseOnCutsceneUpdated(false);
        }

        private void HandleStopped(PlayableDirector pd)
        {
           EventManager.RaiseOnCutsceneUpdated(true);
        }
    }
}
