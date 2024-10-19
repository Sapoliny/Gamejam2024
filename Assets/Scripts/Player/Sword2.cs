using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected a object");
        collision.gameObject.SendMessage("Sword2", SendMessageOptions.DontRequireReceiver);
    }
}
