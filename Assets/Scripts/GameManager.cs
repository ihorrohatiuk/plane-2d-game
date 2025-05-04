using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _scoreText;

    void Start()
    {
        _gameOverPanel.SetActive(false);
        _scoreText.SetActive(true);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") is null)
        {
            _scoreText.SetActive(false);
            _gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowMainMenuClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
