using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score;

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
