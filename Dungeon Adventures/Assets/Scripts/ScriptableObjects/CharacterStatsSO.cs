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
       [Min(5)] public float healthPoints;
       [Min(0)] public float speed;
       public float damage;
    }
}
