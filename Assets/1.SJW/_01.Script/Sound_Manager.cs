using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager instance { get; private set; }


    AudioClip[] bgms;

    public AudioSource bgmPlayer;
    public AudioSource effectSoundPlayer;
    public AudioSource narSoundPlayer;

    public AudioClip[] nars;
    public AudioClip[] BGMS;
    public AudioClip[] EffectiveClip;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartVolumeUp()); //시작 브금 키기
    }

    public void BgmPlay(AudioClip bgm)
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }

      public void BgmPlay(int bgm)
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = BGMS[bgm];
        bgmPlayer.Play();
    }

    public void EffectPlay(AudioClip esm)
    {
        effectSoundPlayer.PlayOneShot(esm);

    }

    public void EffectPlay(int num)
    {
        effectSoundPlayer.PlayOneShot(EffectiveClip[num]);

    }

    public void NarPlay(int nar)
    {
        //if (!narSoundPlayer.isPlaying)
        {
            narSoundPlayer.clip = nars[nar];
            narSoundPlayer.Play();
        }
    }

    


    IEnumerator StartVolumeUp() 
    {
        while (bgmPlayer.volume < 0.45f)
        {
            bgmPlayer.volume += 0.1f * Time.deltaTime;

            yield return null;
        }

    }

}
