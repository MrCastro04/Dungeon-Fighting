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
        [Min(2f)] public float FireRate;
        [Min(2f)] public float NextFireTime;
        public float ProjectileSpeed;
        public float ProjectileDamage;
    }
}