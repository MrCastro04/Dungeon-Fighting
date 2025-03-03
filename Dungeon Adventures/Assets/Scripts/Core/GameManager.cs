using System.Collections.Generic;
using Character;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(PlayerInput))]

    public class GameManager : MonoBehaviour
    {
        private List<string> _allSceneEnemiesId = new ();
        private List<GameObject> _aliveEnemies = new ();

        private void OnEnable()
        {
            EventManager.OnPortalEnter += HandlerPortalEnter;
        }

        private void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlerPortalEnter;
        }

        private void Start()
        {
            List<GameObject> allEnemies = new();

            allEnemies.AddRange(GameObject.FindGameObjectsWithTag(Constants.TAG_ENEMY));

            allEnemies.ForEach((GameObject enemy) =>
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();

                if (enemyController != null)
                {
                    _allSceneEnemiesId.Add(enemyController.EnemyId);
                }
            });
        }

        private void HandlerPortalEnter(Collider player, int nextSceneIndex)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_HEALTH, playerController.HealthCmp.HealthPoints);

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_DAMAGE , playerController.CombatCmp.Damage);

            PlayerPrefs.SetFloat(Constants.PREF_PLAYER_SPEED , playerController.AgentCmp.speed);

            PlayerPrefs.SetInt(Constants.PREF_PLAYER_POTION_COUNT, playerController.HealthCmp.PotionCount);

            _aliveEnemies.AddRange(GameObject.FindGameObjectsWithTag(Constants.TAG_ENEMY));

            _allSceneEnemiesId.ForEach(SaveDefeatedEnemies);
        }

        private void SaveDefeatedEnemies(string savedEnemyId)
        {
            bool isAlive = false;

            _aliveEnemies.ForEach((aliveEnemy) =>
            {
                EnemyController enemyController = aliveEnemy.GetComponent<EnemyController>();

                if (enemyController.EnemyId == savedEnemyId)
                {
                    isAlive = true;
                }
            });

            if(isAlive) return;

            string key = "DefeatedEnemies";

            List<string> defeatedEnemies = PlayerPrefsUtility.GetString(key);

            defeatedEnemies.Add(savedEnemyId);

            PlayerPrefsUtility.SetString(key, defeatedEnemies);

            ///
            ///
            ///
            string s = PlayerPrefs.GetString(key);

            Debug.Log(s);
        }
    }
}
