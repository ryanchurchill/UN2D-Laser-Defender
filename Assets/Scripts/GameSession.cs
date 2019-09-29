using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        updateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore(int points)
    {
        Debug.Log(points);
        score += points;
        updateScoreText();
    }

    public int getScore()
    {
        return score;
    }

    private void updateScoreText()
    {
        FindObjectOfType<ScoreText>().GetComponent<Text>().text = score.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
