using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boss1 : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject markOfAttack2;
    public GameObject gameManager;
    public GameObject player;

    bool waitingToEnd = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waitingToEnd == true && transform.childCount == 0)
        {
            gameManager.SendMessage("bossAttackEnd");
            waitingToEnd = false;
        }
    }

    void StartAttackState() //CHAMADO POR UM SENDMESSAGE DO GAMEMANAGER
    {
        int choice = Random.Range(1, 3 + 1);
        switch (choice)
        {
            case 1:
                StartCoroutine(Attack1());
                break;
            case 2:
                StartCoroutine(Attack2());
                break;
            case 3:
                StartCoroutine(Attack3());
                break;
            default:
                break;
        }
    }

    IEnumerator Attack1()
    {
        int hole;
        List<GameObject> bullets;
        GameObject newBullet;
        for (int y = 0; y < 3; y++)
        {
            hole = Random.Range(-3, 4); //Perto do meio
            bullets = new List<GameObject>();

            for (int i = -5; i < 6; i++)
            {
                if (i != hole)
                {
                    newBullet = Instantiate(bullet1);
                    newBullet.transform.position = transform.position;
                    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, i);
                    newBullet.transform.SetParent(transform);
                    //newBullet.GetComponent<Bullet>().attackToDeflected = attackDeflected.None;
                    bullets.Add(newBullet);
                }
            }
            yield return new WaitForSeconds(1);
            foreach (GameObject i in bullets)
            {
                i.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            }

            yield return new WaitForSeconds(2.5f);
        }

        waitingToEnd = true;
    }

    IEnumerator Attack2()
    {
        List<GameObject> bullets;
        GameObject newBullet;

        bullets = new List<GameObject>();

        for (int i = -5; i < 6; i++)
        {
            newBullet = Instantiate(bullet1);
            newBullet.transform.position = transform.position;
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, i);
            newBullet.transform.SetParent(transform);
            //newBullet.GetComponent<Bullet>().attackToDeflected = attackDeflected.None;
            bullets.Add(newBullet);

        }

        yield return new WaitForSeconds(0.5f);
        GameObject bulletMark = Instantiate(markOfAttack2);
        bulletMark.transform.position = new Vector2(player.transform.position.x + Random.Range(-2f, 2f), player.transform.position.y + Random.Range(-1f, 1f));

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject i in bullets)
        {
            Vector3 direction = (bulletMark.transform.position - i.transform.position).normalized;

            // Rotate the bullet to face the mark
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            i.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Set the bullet's velocity towards the mark
            i.GetComponent<Rigidbody2D>().velocity = direction * 10f;
        }

        Destroy(bulletMark);
        waitingToEnd = true;
    }

    IEnumerator Attack3()
    {
        List<GameObject> bullets;
        GameObject newBullet;

        bullets = new List<GameObject>();

        for (int i = -5; i < 6; i++)
        {
            newBullet = Instantiate(bullet1);
            newBullet.transform.position = transform.position;
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, i);
            newBullet.transform.SetParent(transform);
            //newBullet.GetComponent<Bullet>().attackToDeflected = attackDeflected.None;
            bullets.Add(newBullet);

        }

        yield return new WaitForSeconds(1f);

        foreach (GameObject i in bullets) //PAUSE THEM
        {
            i.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }


        Vector3 nearPlayer = new Vector3(player.transform.position.x, player.transform.position.y + Random.Range(-1f, 1f),0);

        bullets = bullets.OrderBy(b => Random.value).ToList(); //Shuffle à lista

        foreach (GameObject i in bullets)
        {
            Vector3 direction = (nearPlayer - i.transform.position).normalized;

            // Rotate the bullet to face the mark
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            i.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Set the bullet's velocity towards the mark
            i.GetComponent<Rigidbody2D>().velocity = direction * 10f;

            yield return new WaitForSeconds(0.25f);
        }

        waitingToEnd = true;
    }
}
