using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackSelectionMenu;
    public GameObject gameManager;
    public GameObject boss;
    public GameObject attackMark;

    bool isAttacking;
    int currentAttackID = 0;
    Rigidbody2D rb;
    [Header("Attack1")]
    public float maxVelocity = 2;
    public float xOffSet = 1;
    public float goBackX = -1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartAttackState() //CHAMADA PELO SENDMESSAGE DO GAMEMANAGER
    {
        StartCoroutine(GetInPosition());
    }

    IEnumerator GetInPosition()
    {
        
        float xVelocity = Math.Min(maxVelocity, goBackX - transform.position.x);
        rb.velocity = new Vector2(xVelocity, xVelocity / (goBackX - transform.position.x) * (0 - transform.position.y));
        if (transform.position.x > goBackX)
        {
            while (goBackX - transform.position.x < 0)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (goBackX - transform.position.x > 0)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(goBackX, 0);
        attackSelectionMenu.SetActive(true);
    }
    void EndAttack()
    {     
        gameManager.SendMessage("playerAttackEnd");
    }

    public void UseThisAttack(int attackID)
    {
        currentAttackID = attackID;
        switch(attackID)
        {
            case 1:
                StartCoroutine(Attack1());
                break;
            default:
                currentAttackID = 0; //Se o ataque não existe na switch entao o player nao está a atacar
                break;
        }
        attackSelectionMenu.SetActive(false); //Nao posso selecionar outro ataque
    }

    IEnumerator Attack1()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //Ir para posição de ataque
        float xVelocity = Math.Min(maxVelocity, (boss.transform.position.x - xOffSet - transform.position.x));
        rb.velocity = new Vector2(xVelocity, xVelocity / (boss.transform.position.x - xOffSet - transform.position.x) * (boss.transform.position.y - transform.position.y));
        while (boss.transform.position.x - xOffSet - transform.position.x>0)
        {
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = new Vector2(0,0);
        transform.position = new Vector2(boss.transform.position.x - xOffSet, boss.transform.position.y);


        /// O ATAQUE EM SI
        GameObject mark;
        mark = Instantiate(attackMark);
        mark.transform.position = transform.position;
        yield return new WaitForSeconds(1f);
        mark = Instantiate(attackMark);
        mark.transform.position = transform.position;
        yield return new WaitForSeconds(1f);
        mark = Instantiate(attackMark);
        mark.transform.position = transform.position;
        yield return new WaitForSeconds(1f);



        //Voltar
        xVelocity = Math.Min(maxVelocity, goBackX - transform.position.x);
        rb.velocity = new Vector2(xVelocity, xVelocity / (goBackX - transform.position.x) * (0 - transform.position.y));
        while (goBackX - transform.position.x < 0)
        {
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(goBackX, 0);

        GetComponent<BoxCollider2D>().enabled = true;
        EndAttack();
    }
}
