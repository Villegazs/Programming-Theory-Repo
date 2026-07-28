using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject gameOverUI;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        gameOverUI.SetActive(false);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void UpdateScore(float score)
    {
        scoreText.text = $"Score: {score}";
    }
}
