using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackMark : MonoBehaviour
{
    float defaultScale;
    bool halfwayFlag = false;
    public float n; //numero de vezes que cresce
    public float baseDamage = 10;
    float time = -1;
    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
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
            float damage = baseDamage * (transform.localScale.x / (defaultScale * n));
            boss.GetComponent<BossHealth>().getHurt(damage);
            Destroy(gameObject);
        }
    }

    
}
