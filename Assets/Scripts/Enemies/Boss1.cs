using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject gameManager;

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
        int choice = Random.Range(1, 1 + 1); // 3 +1 depois
        switch (choice)
        {
            case 1:
                StartCoroutine(Attack1());
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
        for (int y = 0; y<3; y++)
        {
            hole = Random.Range(-5, 6);
            bullets = new List<GameObject>();
            
            for (int i = -5; i < 6; i++)
            {
                if (i != hole)
                {
                    newBullet = Instantiate(bullet1);
                    newBullet.transform.position = transform.position;
                    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, i);
                    newBullet.transform.SetParent(transform);
                    bullets.Add(newBullet);
                }
            }
            yield return new WaitForSeconds(1);
            foreach(GameObject i in bullets)
            {
                i.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }

            yield return new WaitForSeconds(2.5f);
        }

        waitingToEnd = true;
    }
}
