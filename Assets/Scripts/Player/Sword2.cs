using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessage("Sword2", SendMessageOptions.DontRequireReceiver);
    }
}
