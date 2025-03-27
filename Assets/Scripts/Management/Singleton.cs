using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    private bool shouldDestroyOnScene0 = true;

    protected virtual void Awake()
    {
        DestroyOnScene0(true); 
    }

    public void DestroyOnScene0(bool isDestroy)
    {
        shouldDestroyOnScene0 = isDestroy;

        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = (T)this;

        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (shouldDestroyOnScene0 && scene.name == "Scene0")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
