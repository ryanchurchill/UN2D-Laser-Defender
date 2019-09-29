using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadGameOverWithDelay()
    {
        StartCoroutine(LoadGameOverWithDelayCR());
    }

    public IEnumerator LoadGameOverWithDelayCR()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        GameSession session = FindObjectOfType<GameSession>();
        if (session)
        {
            session.ResetGame();
        }

        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
