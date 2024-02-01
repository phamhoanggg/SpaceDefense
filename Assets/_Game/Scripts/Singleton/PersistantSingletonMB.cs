using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentSingletonMB<T> : SingletonMB<T> where T : MonoBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
