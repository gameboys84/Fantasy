using System;
using UnityEngine;

namespace GameLogic
{
    public class Utils : MonoBehaviour
    {
        // private static Utils _instance;

        // public static Utils Instance
        // {
        //     get
        //     {
        //         if (_instance == null)
        //         {
        //             GameObject go = new GameObject("Utils");
        //             _instance = go.AddComponent<Utils>();
        //         }
        //         return _instance;
        //     }
        // }
        
        [SerializeField] private UIEntry uiEntry;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        
        public static void Log(string message, Color color = default)
        {
            if (message.Length >= 1000)
            {
                message = message.Substring(0, 1000) + " ...";
            }
            
            if (color == default)
            {
                Debug.Log(message);
            }
            else
            {
                Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + message + "</color>");
            }
        }
    }
}