using Character;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(PlayerInput))]

    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.OnPortalEnter += HandlerPortalEnter;
        }

        private void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlerPortalEnter;
        }

        private void HandlerPortalEnter(Collider player, int nextSceneIndex)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_HEALTH, playerController.HealthCmp.HealthPoints);

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_DAMAGE , playerController.CombatCmp.Damage);

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_SPEED , playerController.AgentCmp.speed);

            PlayerPrefs.SetInt(Constants.PREF_PLAYER_POTION_COUNT , playerController.HealthCmp.PotionCount);


        }
    }
}
