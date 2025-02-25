using Uitility;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(
        order = 2,
        fileName = Constants.SO_ITEM_FILE_NAME,
        menuName = Constants.SO_ITEM_MENU_NAME
        )]

    public class ItemSO : ScriptableObject
    {
        public string Name;
        public Sprite Image;
    }
}
