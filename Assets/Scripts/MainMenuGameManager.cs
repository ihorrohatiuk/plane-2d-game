using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainManuGameObject;
    [SerializeField] private GameObject _howToPlayGameObject;

    private void Start()
    {
        _mainManuGameObject.SetActive(true);
        _howToPlayGameObject.SetActive(false);
    }

    public void PlayGameClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGameClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowHowToPlayClick()
    {
        _mainManuGameObject.SetActive(false);
        _howToPlayGameObject.SetActive(true);
    }

    public void ShowMainManuClick()
    {
        _mainManuGameObject.SetActive(true);
        _howToPlayGameObject.SetActive(false);
    }
}
