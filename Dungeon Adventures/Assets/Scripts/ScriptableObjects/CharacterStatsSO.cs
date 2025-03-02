using Uitility;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(
        order = 0,
        fileName = Constants.SO_CHARACTER_STATS_FILE_NAME,
        menuName = Constants.SO_CHARACTER_STATS_MENU_NAME)]

    public class CharacterStatsSO : ScriptableObject
    {
       [Min(5f)] public float HealthPoints;
       [Min(2f)] public float AttackRange;
       [Min(0f)] public float Speed;
       public float MeeleDamage;
    }
}
