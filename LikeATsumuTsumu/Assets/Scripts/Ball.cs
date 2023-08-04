using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ballの識別、変更を実装するクラス
/// </summary>
public class Ball : MonoBehaviour
{
    //Ballの識別
    public int id;

    [SerializeField] GameObject explosionPrefab;
    public void Explosion()
    {
        //エフェクトを生成してオブジェクトを破壊
        GameObject explosion = Instantiate(explosionPrefab,transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 0.27f);
    }
}
