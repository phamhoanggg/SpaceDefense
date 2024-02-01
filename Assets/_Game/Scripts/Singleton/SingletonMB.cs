using UnityEngine;

public abstract class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("Singleton");
                instance = obj.AddComponent<T>();
            }

            return instance;
        }
    }

    public static bool InstanceExisted()
    {
        return instance != null;
    }
}
