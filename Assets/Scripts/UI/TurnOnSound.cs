using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSound : MonoBehaviour
{
    GameObject backgroundMusic;
    public GameObject turnOffButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Muted", 1) == 0)
        {
            gameObject.SetActive(false);
        }
        backgroundMusic = GameObject.Find("BackgroundMusic");
    }

    public void press()
    {
        backgroundMusic.GetComponent<BackgroundMusic>().UnMute();
        turnOffButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
