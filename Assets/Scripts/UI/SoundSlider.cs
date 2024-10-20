using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    Slider soundSlider;
    GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        soundSlider = GetComponent<Slider>();
        soundSlider.value = PlayerPrefs.GetInt("Volume", 100) / 100f;
    }

    public void ValueChanged()
    {
        GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>().SetVolume(soundSlider.value);
    }
}
