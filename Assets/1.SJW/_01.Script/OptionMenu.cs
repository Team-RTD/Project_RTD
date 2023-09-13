using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider bgmslider;
    public Slider effectSlider;
    public Slider narSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Data_Manager.instance.isPause = true;
        Time.timeScale = 0;
        bgmslider.value = Sound_Manager.instance.bgmPlayer.volume;
        effectSlider.value = Sound_Manager.instance.effectSoundPlayer.volume;
        narSlider.value = Sound_Manager.instance.narSoundPlayer.volume;
    }

    private void OnDisable()
    {
        Data_Manager.instance.isPause = false;
        Time.timeScale = 1;
    }


    public void BgmVolChanger()
    {
        Sound_Manager.instance.bgmPlayer.volume = bgmslider.value;
    }
    public void EffectVolChanger()
    {
        Sound_Manager.instance.effectSoundPlayer.volume = effectSlider.value;
    }
    public void NarVolChanger()
    {
        Sound_Manager.instance.narSoundPlayer.volume = narSlider.value;
    }

}
