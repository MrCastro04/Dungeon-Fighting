using Character.Boss;
using Core;
using Uitility;
using UnityEngine;

namespace Interacts
{
    public class BossCaller : MonoBehaviour
    {
        [SerializeField] private BossController _bossController;

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
            if (_bossController.gameObject.activeSelf == false)
            {
                _bossController.gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(Constants.TAG_BOSS)) return;

            if(_bossController.gameObject.activeSelf) return;

            EventManager.RaiseOnPlayerEnterTheAreaWithBoss();
        }
    }
}
