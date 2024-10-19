using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Attacks
{
    NotAttacking,
    Attack1,
    Attack2Charge,
    Attack2Release,
}

public class PlayerControl : MonoBehaviour
{

    
    public float Xvelocity;
    public float Yvelocity;
    float Xmovement;
    float Ymovement;

    public GameObject sword1;
    public GameObject sword2;

    Attacks currentAttack;
    bool minCharge = false;
    bool triedToRelease = false;

    public bool isAllowed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAllowed)
        {
            Move();
            Attack();
        }    
    }

    void StartDefense() //CHAMADA PELO SENDMESSAGE DO GAMEMANAGER
    {
        isAllowed = true;
    }

    void StopDefense()  //CHAMADA PELO SENDMESSAGE DO GAMEMANAGER
    { //HARD RESET
        isAllowed = false;
        StopAllCoroutines();
        sword1.SetActive(false);
        sword2.SetActive(false);
        minCharge = false;
        triedToRelease = false;
        currentAttack = Attacks.NotAttacking;
    }


    void Move()
    {
        Xmovement = Input.GetAxisRaw("Horizontal") * Xvelocity * Time.deltaTime;
        Ymovement = Input.GetAxisRaw("Vertical") * Yvelocity * Time.deltaTime; ;

        transform.position = new Vector2(transform.position.x + Xmovement, transform.position.y + Ymovement);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentAttack == Attacks.NotAttacking)
            {
                StartCoroutine(Attack1());
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (currentAttack == Attacks.NotAttacking)
            {
                triedToRelease = false;
                minCharge = false;
                currentAttack = Attacks.Attack2Charge;
                StartCoroutine(ReleaseAttack2timer());
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            triedToRelease = true;
            ReleaseAttack2();
        }
    }

    IEnumerator Attack1()
    {
        sword1.SetActive(true);
        currentAttack = Attacks.Attack1;
        yield return new WaitForSeconds(0.1f);
        sword1.SetActive(false);
        currentAttack = Attacks.NotAttacking;
    }

    IEnumerator ReleaseAttack2timer()
    {
        yield return new WaitForSeconds(0.3f);
        minCharge = true;
        if(triedToRelease == true)
        {
            ReleaseAttack2();
        }
        else
        {
            yield return new WaitForSeconds(1.7f);
            ReleaseAttack2();
        }
        
        
    }

    void ReleaseAttack2()
    {
        if (currentAttack == Attacks.Attack2Charge && minCharge == true)
        {
           
            StartCoroutine(Attack2());
        }
    }

    IEnumerator Attack2()
    {
        sword2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        sword2.SetActive(false);
        currentAttack = Attacks.NotAttacking;
    }
}
