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
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartVolumeUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BgmPlay(AudioClip bgm)
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }

    public void EffectPlay(AudioClip esm)
    {
        effectSoundPlayer.PlayOneShot(esm);

    }

    public void NarPlay(AudioClip nar)
    {
        narSoundPlayer.clip = nar;
        narSoundPlayer.Play();
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
