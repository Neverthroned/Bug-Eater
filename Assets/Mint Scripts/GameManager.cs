using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool saidNoToSnail = false;

    public Vector3 returnPosition;
    public string returnScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSnailScene()
    {
        SceneManager.LoadScene("SnailScene");
    }

    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(returnScene);
    }
}