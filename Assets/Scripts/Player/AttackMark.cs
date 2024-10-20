using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackMark : MonoBehaviour
{
    float defaultScale;
    public float n; //numero de vezes que cresce
    public float baseDamage = 10;
    float time = -1;
    GameObject boss;
    GameObject player;

    public GameObject badIndicator;
    public GameObject goodIndicator;
    public GameObject perfectIndicator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        defaultScale = transform.localScale.x; //scale: x = y
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        time += 2* Time.deltaTime;
        if (time > 1f)
        {
            Destroy(gameObject);
        }

        transform.localScale = new Vector2(defaultScale + defaultScale*(1-Math.Abs(time)) * (n-1) , defaultScale + defaultScale * (1 - Math.Abs(time)) * (n - 1));

        if(Input.GetKey(KeyCode.Mouse0))
        {
            player.SendMessage("Strike1");
            float damage = baseDamage * (0.2f + 0.8f * ((transform.localScale.x - defaultScale) / (defaultScale * (n-1))));
            GameObject indicator;
            if (damage / baseDamage > 0.85f)
            {
                indicator = Instantiate(perfectIndicator);
            }
            else if (damage / baseDamage > 0.45f)
            {
                indicator = Instantiate(goodIndicator);
            }
            else
            {
                indicator = Instantiate(badIndicator);
            }
            indicator.GetComponent<DamageIndicator>().Show(damage,transform);

            boss.GetComponent<BossHealth>().getHurt(damage);
            Destroy(gameObject);
        }
    }

    
}
