using Uitility;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class Portal : MonoBehaviour
    {
         [SerializeField] private Transform _spawnPoint;
         [SerializeField] private int _nextScene;

         private Collider _colliderCmp;

         private void Awake()
         {
             _colliderCmp = GetComponent<Collider>();
         }

         private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag(Constants.TAG_PLAYER)) return;

            _colliderCmp.enabled = false;

            EventManager.RaiseOnPortalEnter(other, _nextScene);

            SceneTransition.Initiate(_nextScene);
        }
    }
}
