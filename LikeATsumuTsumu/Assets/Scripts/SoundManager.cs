using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceBGM;
    [SerializeField] AudioClip[] bgmClips;

    [SerializeField] AudioSource audioSourceSE;
    [SerializeField] AudioClip[] seClips;

    //BGM‚Ì—ñ‹“
    public enum BGM 
    {
        Title,
        Main
    }

    public enum SE
    {
        Touch,
        Destroy
    }

    //ƒVƒ“ƒOƒ‹ƒgƒ“‰»
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGM bgm)
    {
        audioSourceBGM.clip = bgmClips[(int)bgm];
        audioSourceBGM.Play();
    }
    
    public void PlaySE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }
}
