using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    public float Score;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") is not null)
        {
            Score += 1 * Time.deltaTime;
            _scoreText.text = "Score: " + (int)Score;
        }
    }
}
