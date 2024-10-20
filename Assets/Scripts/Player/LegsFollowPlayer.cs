using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsFollowPlayer : MonoBehaviour
{
    public GameObject player;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x + 0.075f, player.transform.position.y - 0.75f);

        if (player.GetComponent<Rigidbody2D>().velocity != new Vector2(0,0))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
