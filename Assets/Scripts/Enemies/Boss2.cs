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

    public Transform[] swipePositionsLeft;
    public Transform[] swipePostionsRight;

    public Transform exitScreen;
    public float movementSpeedExit;
    public float movementSpeed;

    public float swipeDamage;
    public float rotationSpeed;


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
        int choice = UnityEngine.Random.Range(0, 2);
        switch (choice)
        {
            case 0:
                StartCoroutine(attack1());  
                break;
            case 1:
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
        yield return new WaitForSeconds(2);
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

            for (int i = 0; i < 3; i++)
            {
                Vector3 swipePositionLeft = swipePositionsLeft[UnityEngine.Random.Range(0, swipePositionsLeft.Length)].position;
                Vector3 swipePositionRight = swipePostionsRight[UnityEngine.Random.Range(0, swipePostionsRight.Length)].position;
                int choice = UnityEngine.Random.Range(0, 2);

                Vector2 firstSwipePostion = Vector2.zero;
                Vector2 lastSwipePosition = Vector2.zero;

                switch (choice)
                {
                    case 0:
                        firstSwipePostion = swipePositionLeft;
                        lastSwipePosition = swipePositionRight;
                        Debug.Log(firstSwipePostion);
                        break;
                    case 1:
                        firstSwipePostion = swipePositionRight;
                        lastSwipePosition = swipePositionLeft;
                        Debug.Log(firstSwipePostion);
                        break;
                    default:
                        firstSwipePostion = swipePositionLeft;
                        Debug.Log(firstSwipePostion);
                        break;
                }
                this.transform.position = firstSwipePostion;

                while (MovePos(lastSwipePosition, movementSpeed * Time.deltaTime)) 
                {
                    yield return null;
                } 
            }

            this.transform.position = defaultPosition;
            waitingToEnd = true;
        }

    bool MovePos(Vector3 target, float moveSpeed)
    {
        this.transform.position =  Vector2.MoveTowards(this.transform.position, target, moveSpeed);
        this.transform.Rotate(new Vector3(0,0, rotationSpeed * Time.deltaTime));
        if (Vector2.Distance(this.transform.position, target) < 0.1f)
        {       
            this.transform.rotation = Quaternion.identity;
            return false;
        }
        return true;
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
