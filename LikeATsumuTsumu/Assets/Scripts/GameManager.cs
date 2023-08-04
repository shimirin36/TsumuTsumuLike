using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームシステムを実装するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();
    [SerializeField] Score score;
    [SerializeField] GameObject pointEffectPrefab;
    [SerializeField] Text timerText;
    [SerializeField] GameObject resultPanel;
    
    float timeCount;
    bool isDragging;
    bool gameOver;
    Ball currentDraggingBall;


    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Main);
        timeCount = ParamsSO.Entity.limitTime;
        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.initBallCount));
        StartCoroutine(CountDown());
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
        else if (isDragging)
        {
            OnDragging();
        }
    }

    /// <summary>
    /// ドラッグの始まり
    /// </summary>
    void OnDragBegin()
    {
        //マウスによるオブジェクトの判定
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Ball ball = hit.collider.GetComponent<Ball>();
            AddRemoveBall(ball);
            isDragging = true;
        }
    }

    /// <summary>
    /// ドラッグ中
    /// </summary>
    void OnDragging()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit && hit.collider.GetComponent<Ball>())
        {
            //現在選択しているオブジェクトと同じ種類且つ距離が近ければリストに追加
            Ball ball = hit.collider.GetComponent<Ball>();

            //同じ種類
            if(ball.id == currentDraggingBall.id)
            {
                //距離の計算
                float distance = Vector2.Distance(ball.transform.position, currentDraggingBall.transform.position);
                if(distance < ParamsSO.Entity.ballDistance)
                {
                    AddRemoveBall(ball);
                }
            }
        }
    }

    /// <summary>
    /// ドラッグの終わり
    /// </summary>
    void OnDragEnd()
    {
        int removeCount = removeBalls.Count;
        float point = 0;
        //3つ以上で消せる
        if (removeCount >= 3)
        {
            //ボールを選択した分だけ消す
            for (int i = 0; i < removeCount; i++)
            {
                removeBalls[i].Explosion();
            }
            
            if(removeCount < 4)
            {
                point = removeCount * ParamsSO.Entity.scorePoint * ParamsSO.Entity.scoreRatio[0];
            }
            else if(3 < removeCount && removeCount < 7)
            {
                point = removeCount * ParamsSO.Entity.scorePoint * ParamsSO.Entity.scoreRatio[1];
            }
            else if(8 < removeCount)
            {
                point = removeCount * ParamsSO.Entity.scorePoint * ParamsSO.Entity.scoreRatio[2];
            }

            //減った分だけ増やす
            StartCoroutine(ballGenerator.Spawns(removeCount));
            
            score.ShowScore(point);
            
            SpawnPointEffect(removeBalls[removeCount - 1].transform.position, point);
        }

        //選択されたボールの大きさと色変更
        for(int i= 0; i < removeCount; i++)
        {
            removeBalls[i].transform.localScale = Vector3.one;
            removeBalls[i].GetComponent<SpriteRenderer>().color = Color.white;
        }

        removeBalls.Clear();
        isDragging = false;
    }

    /// <summary>
    /// ドラッグされたボールを削除リストに追加
    /// </summary>
    /// <param name="ball"></param>
    void AddRemoveBall(Ball ball)
    {
        //ドラッグ開始地点のボール
        currentDraggingBall = ball;
        //同じボールインスタンス以外のものはリストに入れる
        if (removeBalls.Contains(ball) == false)
        {
            SoundManager.instance.PlaySE(SoundManager.SE.Touch);
            ball.transform.localScale = Vector3.one * 1.5f;
            ball.GetComponent<SpriteRenderer>().color = Color.yellow;
            removeBalls.Add(ball);
        }
    }

    /// <summary>
    /// 消したボールの最後の座標で取得したポイントを表示するエフェクト
    /// </summary>
    /// <param name="position"></param>
    /// <param name="point"></param>
    void SpawnPointEffect(Vector2 position, float point)
    {
        SoundManager.instance.PlaySE(SoundManager.SE.Destroy);
        GameObject effectObj = Instantiate(pointEffectPrefab, position, Quaternion.identity);
        PointEffect pointEffect = effectObj.GetComponent<PointEffect>();
        pointEffect.ShowEffect(point);
    }

    //カウントダウン
    IEnumerator CountDown()
    {
        while (timeCount > 0)
        {
            yield return new WaitForSeconds(0.1f);
            timeCount -= 0.1f;
            timerText.text = timeCount.ToString("n1");
        }
        timerText.text = "0.0";
        gameOver = true;
        resultPanel.SetActive(true);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene("Main");
    }
}
