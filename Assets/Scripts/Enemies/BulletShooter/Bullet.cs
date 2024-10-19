using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum attackDeflected
{
    Attack1,
    Attack2,
}

public class Bullet : MonoBehaviour
{

    public float damage;

    [Tooltip("Bullet till bullet disapears")]
    public float bulletTime;
    private float bulletLivingTime;
    attackDeflected attackDeflected;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletLivingTime += Time.deltaTime;
        if (bulletLivingTime >= bulletTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHealth>().getHurt(damage);
            Destroy(this.gameObject);
        }
    }

    void Sword1()
    {

    }
}
