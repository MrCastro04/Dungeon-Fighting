using Character_Stats;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
       [SerializeField] private CharacterStatsSO _playerStats;

        private Health _healthCmp;
        private Combat _combatCmp;
        
    }
}
