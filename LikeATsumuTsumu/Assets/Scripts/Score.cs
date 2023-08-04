using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアの表示を実装するクラス
/// </summary>
public class Score : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text resultScore;

    //初期値
    float score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void ShowScore(float getPoint)
    {
        score += getPoint;
        scoreText.text = score.ToString();
        resultScore.text = score.ToString();
    }
}
