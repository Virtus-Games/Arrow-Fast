using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance;

        private void Awake()
        {
            if (instance == null)
            {
                T[] ins = FindObjectsOfType(typeof(T)) as T[];

                if (ins.Length > 0)
                {
                    instance = ins[0];
                    DontDestroyOnLoad(instance);
                }

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
            }
        }
        public static T GetInstance
        {
            get
            { 
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);

                }
                return instance;
            }
        }
    }
}
