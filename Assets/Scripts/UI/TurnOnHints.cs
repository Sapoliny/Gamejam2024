using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnHints : MonoBehaviour
{
    public GameObject turnOnButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Hints", 1) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void press()
    {
        PlayerPrefs.SetInt("Hints", 1);
        turnOnButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
