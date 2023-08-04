using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

/// <summary>
/// Ball‚ğ¶¬‚·‚éƒNƒ‰ƒX
/// </summary>
public class BallGenerator : MonoBehaviour
{
    
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Sprite[] ballSprites;

    #region Ball‚Ì¶¬
    /// <summary>
    /// Ball‚ğˆê’èŠÔŠu‚Å¶¬‚·‚é
    /// </summary>
    /// <param name="count">¶¬‚·‚éŒÂ”</param>
    /// <returns></returns>
    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //Ball‚Ì¶¬À•W
            Vector2 position = new Vector2(Random.Range(-0.1f, 0.1f), 6f);
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);

            //‰æ‘œ‚Ìİ’è
            int ballID = Random.Range(0, ballSprites.Length);
            ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID];
            ball.GetComponent<Ball>().id = ballID;

            yield return new WaitForSeconds(0.04f);
        }
    }
    #endregion

}
