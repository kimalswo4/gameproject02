using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find(typeof(T).ToString()).GetComponent<T>();
            }
            return _instance;
        }
    }

    private static T _instance = null;
}
