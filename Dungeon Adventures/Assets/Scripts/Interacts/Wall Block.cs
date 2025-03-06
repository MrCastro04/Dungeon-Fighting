using Core;
using UnityEngine;

namespace Interacts
{
    [RequireComponent(typeof(BoxCollider))]
    public class WallBlock : MonoBehaviour
    {
        private BoxCollider _boxColliderCmp;

        private void Awake()
        {
            _boxColliderCmp = GetComponent<BoxCollider>();
        }

        private void OnEnable()
        {
            EventManager.OnPlayerEnterTheAreaWithBoss += HandlerOnPlayerEnterTheAreaWithBoss;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerEnterTheAreaWithBoss -= HandlerOnPlayerEnterTheAreaWithBoss;
        }

        private void HandlerOnPlayerEnterTheAreaWithBoss()
        {
            _boxColliderCmp.isTrigger = false;
        }
    }
}
