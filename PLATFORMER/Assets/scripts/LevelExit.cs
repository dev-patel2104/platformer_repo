using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float ExitSlowMo = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextSceneWithDelay());
    }

    IEnumerator LoadNextSceneWithDelay()
    {
        Time.timeScale = ExitSlowMo;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
