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
    [HideInInspector]
    public float speed;

    [Tooltip("Bullet till bullet disapears")]
    public float bulletTime;
    private float bulletLivingTime;
    public attackDeflected attackToDeflected;
    [Tooltip("If delflected go to boss")]
    public bool goToBoss;

    private Transform bossTransform;
    public GameObject indicator;

    public AudioClip bulletBlockSound;
    AudioSource audioSource;

    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        bossTransform = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
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
        else if (coll.gameObject.tag == "Boss")
        {
            coll.gameObject.GetComponent<BossHealth>().getHurt(damage);
            GameObject e = Instantiate(indicator);
            e.GetComponent<DamageIndicator>().Show(damage, this.transform);
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
        audioSource.PlayOneShot(bulletBlockSound);
        if (goToBoss)
        {
            Vector2 direction = bossTransform.position - this.transform.position;
            float angle = Vector2.Angle(direction, this.transform.position);
            if (direction.y > 0)
            {
                angle *= -1;
            }
            rb2D.velocity = direction.normalized * speed;
            transform.rotation = Quaternion.Euler(0, 0, angle);
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
            transform.rotation = Quaternion.Euler(0, 0, angle);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Background")
        {
            Destroy(gameObject);
        }
    }
}
