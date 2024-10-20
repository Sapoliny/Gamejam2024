using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSound : MonoBehaviour
{
    GameObject backgroundMusic;
    public GameObject turnOnButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Muted",0) == 1)
        {
            gameObject.SetActive(false);
        }
        backgroundMusic = GameObject.Find("BackgroundMusic");
    }

    public void press()
    {
        backgroundMusic.GetComponent<BackgroundMusic>().Mute();
        turnOnButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
