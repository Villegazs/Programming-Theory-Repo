using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float score = 0;

    [SerializeField]
    private UIGameHandler UIGameHandler;
    
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
        if (UIGameHandler != null)
            UIGameHandler.UpdateScore(score);
    }

    private void Start()
    {
        UIGameHandler = FindObjectOfType<UIGameHandler>();
        AddScore(0);
    }

    public void EndGame()
    {
        UIGameHandler.GameOverUI();
    }
}
