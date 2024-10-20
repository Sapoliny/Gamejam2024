using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword1 : MonoBehaviour
{
    GameObject bg;
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected a object");
        collision.gameObject.SendMessage("Sword1",SendMessageOptions.DontRequireReceiver);
        bg = GameObject.Find("BackgroundMusic");
        bg.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
}
