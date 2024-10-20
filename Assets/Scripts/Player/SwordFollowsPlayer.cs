using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollowsPlayer : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); //Just in case
    }
    
    void OnSetActive()
    {
        transform.position = new Vector2(player.transform.position.x - 0.1f, player.transform.position.y - 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x -0.1f, player.transform.position.y - 0.4f);
    }
}
