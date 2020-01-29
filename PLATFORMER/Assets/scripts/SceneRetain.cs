using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRetain : MonoBehaviour
{
    int startingIndex;
    private void Awake()
    {
        int numSceneRetain = FindObjectsOfType<SceneRetain>().Length;
        if(numSceneRetain > 1)
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
        startingIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentIndex != startingIndex)
        {
            Destroy(this.gameObject);
        }
    }
}
