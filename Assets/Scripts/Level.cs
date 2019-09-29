using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public IEnumerator LoadGameOverWithDelay()
    {
        yield return new WaitForSeconds(1);
        LoadGameOver();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
