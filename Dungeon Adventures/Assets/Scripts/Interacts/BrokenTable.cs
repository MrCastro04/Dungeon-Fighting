using Core;
using ScriptableObjects;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interacts
{
    public class BrokenTable : MonoBehaviour
    {
        [SerializeField] private ItemSO _item;

        private Canvas _canvasCmp;

        private void Awake()
        {
            _canvasCmp = GetComponentInChildren<Canvas>();
        }

        public void HandlerInteract(InputAction.CallbackContext context)
        {
            if(context.performed == false && _canvasCmp.enabled) return;

            EventManager.RaisePlayerGetItem(_item);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(Constants.TAG_ENEMY)) return;

            _canvasCmp.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag(Constants.TAG_ENEMY)) return;

            _canvasCmp.enabled = false;
        }
    }
}
