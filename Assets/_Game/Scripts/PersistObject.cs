using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
