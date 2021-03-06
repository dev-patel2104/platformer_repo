﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;


    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();

    }
    public void AddToScore(int pointToAdd)
    {
        score = score + pointToAdd;
        scoreText.text = score.ToString();
    }
    public void ProcessDeath()
    {
        if(lives > 1)
        {
            TakeLife();
        }

        else
        {
            RestartGame();
        }
    }

    private void TakeLife()
    {
        lives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = lives.ToString();
    }

    private void RestartGame()
    {
        
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        
    }

    public void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentIndex == 0)
        {
            Destroy(this.gameObject);
        }
    }
}

    
