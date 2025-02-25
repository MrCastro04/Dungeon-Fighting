using Core;
using ScriptableObjects;
using TMPro;
using Uitility;
using UnityEngine;

namespace UI
{
    public class KeyViewer : MonoBehaviour
    {
        private GameObject _keyImage;
        private TextMeshProUGUI _succesMesage;

        private void Awake()
        {
           _keyImage = GameObject.FindGameObjectWithTag(Constants.TAG_KEY_IMAGE);

           _succesMesage = _keyImage.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            EventManager.OnPlayerGetItem += HandlerPlayerGetItem;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerGetItem -= HandlerPlayerGetItem;
        }

        private void HandlerPlayerGetItem(ItemSO item)
        {
            _succesMesage.text = $"You have got a {item.Name} .";
        }
    }
}
