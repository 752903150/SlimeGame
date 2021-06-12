using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    private static T instance = null;
    public static T Instance => instance;

    protected void Awake()
    {
        instance = this as T;
    }
}