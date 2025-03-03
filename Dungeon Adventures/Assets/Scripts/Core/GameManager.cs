using System;
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
            if(player == null) return;

            PlayerController playerController = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_HEALTH, playerController.HealthCmp.HealthPoints);


        }
    }
}
