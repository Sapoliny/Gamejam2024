using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public enum attackDeflected
{
    Attack1,
    Attack2,
    None,
}
    
public class Bullet : MonoBehaviour
{


    public float damage;

    [Tooltip("Bullet till bullet disapears")]
    public float bulletTime;
    private float bulletLivingTime;
    public attackDeflected attackToDeflected;
    [Tooltip("If delflected go to boss")]
    public bool goToBoss;

    public Transform bossTransform;

    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
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
        Debug.Log("Defltected attack 1");
        if (attackToDeflected != attackDeflected.Attack1)
        {
            return;
        }
        if (goToBoss)
        {
            Vector2 direction = bossTransform.position - this.transform.position;
            float angle = Vector2.Angle(direction, this.transform.position);
            if (direction.y > 0)
            {
                angle *= -1;
            }
            rb2D.velocity = direction.normalized;
            transform.rotation = Quaternion.Euler(0,0, angle);
        }
        else
        {
            Vector2 direction = new Vector2(rb2D.velocity.x * -1, rb2D.velocity.y);
            float angle = Vector2.Angle(rb2D.velocity, direction) / 2;
            if (direction.y > 0)
            {
                angle *= -1;
            }
            rb2D.velocity = direction;
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }

    void Sword2()
    {
        Debug.Log("Defltected attack 2f");
        if (attackToDeflected != attackDeflected.Attack2)
        {
            return;
        }
        Vector2 direction = new Vector2(rb2D.velocity.x * -1, rb2D.velocity.y);
        float angle = Vector2.Angle(rb2D.velocity, direction) / 2;
        if (direction.y > 0)
        {
            angle *= -1;
        }
        rb2D.velocity = direction;
        this.transform.Rotate(new Vector3(0, 0, angle));

    }
}
