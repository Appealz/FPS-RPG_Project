using UnityEngine;

public abstract class DestroySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static bool _applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if(_applicationIsQuitting)
            {
                Debug.Log($"Instance Callback When QuitedApplication_{typeof(T)}");
                return null;
            }

            lock(_lock)
            {
                if(_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if(_instance == null)
                    {
                        GameObject singleObj = new GameObject(typeof(T).Name);
                        _instance = singleObj.AddComponent<T>();
                        singleObj.name = typeof(T).Name;
                    }
                }
                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        DoAwake();
    }

    protected virtual void DoAwake() { }

    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if(_instance == this)
        {
            _applicationIsQuitting = true;
        }
    }
}

public abstract class DontDestroySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static bool _applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.Log($"Instance Callback When QuitedApplication_{typeof(T)}");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject singleObj = new GameObject(typeof(T).Name);
                        _instance = singleObj.AddComponent<T>();
                        DontDestroyOnLoad(singleObj);
                        singleObj.name = typeof(T).Name;
                    }
                }
                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DoAwake();
    }

    protected virtual void DoAwake() { }

    protected virtual void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _applicationIsQuitting = true;
        }
    }
}