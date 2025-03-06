using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class PlayerPrefsUtility
    {
        public static List<string> GetString (string key)
        {
            string unformattedValue = PlayerPrefs.GetString(key);

            List<string> formattedValue = new List<string>(unformattedValue.Split(","));

            if (formattedValue.Count == 1 && unformattedValue.Length == 0)
            {
                formattedValue.RemoveAt(0);
            }

            return formattedValue;
        }

        public static void SetString(string key , List<string> value)
        {
            string formattedValue = String.Join("," , value);

            PlayerPrefs.SetString(key, formattedValue);
        }
    }
}