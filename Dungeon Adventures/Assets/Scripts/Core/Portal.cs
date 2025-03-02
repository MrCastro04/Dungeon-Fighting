using Uitility;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] protected Transform _spawnPoint;
        [SerializeField] protected int _nextScene;

        protected Collider _colliderCmp;

        private void Awake()
        {
            _colliderCmp = GetComponent<Collider>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.TAG_PLAYER)) return;

            _colliderCmp.enabled = false;

            EventManager.RaisePortalEnter(other, _nextScene);

            SceneTransition.Initiate(_nextScene);
        }
    }
}