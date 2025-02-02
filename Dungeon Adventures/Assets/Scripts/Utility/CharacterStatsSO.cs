using Uitility;
using UnityEngine;

namespace Character_Stats
{
    [CreateAssetMenu(
        order = 0,
        fileName = Constants.CHARACTER_SO_FILE_NAME,
        menuName = Constants.CHARACTER_SO_MENU_NAME)]

    public class CharacterStatsSO : ScriptableObject
    {
       [Min(5)] public float healthPoints;
       [Min(1)] public float damage;
       [Min(0)]public float speed;
    }
}
