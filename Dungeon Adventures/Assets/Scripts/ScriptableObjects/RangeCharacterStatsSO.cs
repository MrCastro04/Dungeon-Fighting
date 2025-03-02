using Uitility;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(
        order = 1,
        fileName = Constants.SO_MAGE_CHARACTER_STATS_FILE_NAME,
        menuName = Constants.SO_MAGE_CHARACTER_STATS_MENU_NAME
    )]
    public class RangeCharacterStatsSO : CharacterStatsSO
    {
        [Min(5f)] public float ProjectileSpeed;
        [Min(5f)] public float ProjectileDamage;
    }
}