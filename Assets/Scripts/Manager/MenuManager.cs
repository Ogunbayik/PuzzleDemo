using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public event Action OnClickNewGame;
    public event Action OnPanelShowRequest;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }
    public void ClickNewGame()
    {
        OnClickNewGame?.Invoke();
    }

    private void OnEnable()
    {
        OnClickNewGame += MenuManager_OnClickNewGame;
    }
    private void OnDisable()
    {
        OnClickNewGame -= MenuManager_OnClickNewGame;
    }

    private void MenuManager_OnClickNewGame()
    {
        StartCoroutine(nameof(HandleStartSequence));
    }

    private IEnumerator HandleStartSequence()
    {
        Debug.Log("Game is starting now");
        yield return new WaitForSeconds(0.5f);
        OnPanelShowRequest?.Invoke();
        yield return new WaitForSeconds(3.3f);
        Debug.Log("Animasyon is finished");
        yield return new WaitForSeconds(0.5f);
        LoadGameScene();
    }

    public void LoadGameScene()
    {
        const int gameSceneIndex = 1;
        SceneManager.LoadScene(gameSceneIndex);
    }
}
