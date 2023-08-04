using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボールを消した際のスコア表示エフェクトの実装
/// </summary>
public class PointEffect : MonoBehaviour
{
    [SerializeField] Text text;

    /// <summary>
    /// スコアに応じて表示を変更
    /// </summary>
    /// <param name="point"></param>
    public void ShowEffect(float point)
    {
        text.text = point.ToString();
        StartCoroutine(MoveUp());
    }

    /// <summary>
    /// 表示を上に上げるエフェクト
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveUp()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
            //y軸方向に上げる
            transform.Translate(0, 0.1f, 0);
        }
        //ボールを破壊
        Destroy(gameObject, 0.2f);
    }
}
