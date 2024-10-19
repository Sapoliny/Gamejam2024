using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public GameObject shooter;
    public Transform[] shooterPositions;
    public GameObject gameManager;

    Vector3 defaultPosition;
    public Transform[] swipePositions;
    public Transform exitScreen;
    public float movementSpeedExit;
    public float movementSpeed;

    public float swipeDamage;

    bool waitingToEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartAttackState() //CHAMADO POR UM SENDMESSAGE DO GAMEMANAGER
    {
        int choice = UnityEngine.Random.Range(1, 2 + 1);
        switch (choice)
        {
            case 1:
                StartCoroutine(attack1());
                break;
            case 2:
                StartCoroutine(attack2());
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        if (waitingToEnd == true)
        {
            gameManager.SendMessage("bossAttackEnd");
            waitingToEnd = false;
        }
    }


    IEnumerator attack1()
    {
        List<GameObject> shooters = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            shooters.Add(GameObject.Instantiate(shooter, shooterPositions[i].position, quaternion.Euler(0, 0, 0)));
        }
        yield return new WaitForSeconds(4);
        for (int i = 0; i < 5; i++)
        {
            Destroy(shooters[i]);
        }
        shooters.Clear();
        waitingToEnd = true;
    }

    IEnumerator attack2()
    {
        while (true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, exitScreen.position, movementSpeedExit * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, exitScreen.position) < 0.1f)
            {

                yield return new WaitForSeconds(2);
                break;
            }
            yield return null;
        }

        this.transform.position = swipePositions[0].position;

        while (true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, swipePositions[1].position, movementSpeed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, swipePositions[1].position) < 0.1f)
            {
                yield return new WaitForSeconds(2);
                break;
            }

            yield return null;
        }
        this.transform.position = swipePositions[2].position;

        while (true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, swipePositions[3].position, movementSpeed * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, swipePositions[3].position) < 0.1f)
            {
                this.transform.position = defaultPosition;
                yield return new WaitForSeconds(2);
                break;
            }
            yield return null;
        }

        this.transform.position = defaultPosition;
        waitingToEnd = true;

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            collider2D.gameObject.GetComponent<PlayerHealth>().getHurt(swipeDamage);
        }
        else if (collider2D.gameObject.GetComponent<Bullet>() != null)
        {
            this.gameObject.GetComponent<BossHealth>().getHurt(collider2D.gameObject.GetComponent<Bullet>().damage);
        }
    }
}
