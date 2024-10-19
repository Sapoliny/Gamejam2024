using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackSelectionMenu;
    public GameObject gameManager;

    bool isAttacking;
    int currentAttackID = 0;
    Rigidbody2D rb;

    bool isAllowed; //BOOL GLOBAL DE TODO O SCRIPT

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
        isAllowed = true;
        attackSelectionMenu.SetActive(true);
    }

    void EndAttack()
    {     
        gameManager.SendMessage("playerAttackEnd");
        isAllowed = false;
    }

    public void UseThisAttack(int attackID)
    {
        currentAttackID = attackID;
        switch(attackID)
        {
            case 1:
                break;
            default:
                currentAttackID = 0; //Se o ataque não existe na switch entao o player nao está a atacar
                break;
        }
        attackSelectionMenu.SetActive(false); //Nao posso selecionar outro ataque
    }

    //IEnumerator Attack1()
    //{
       
    //}
}
