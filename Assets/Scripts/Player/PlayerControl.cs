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
    private Rigidbody2D rb2d;

    public float attackCooldown = 0.3f;
    public GameObject sword1;
    public GameObject sword2;
    public GameObject swordObject;
    Animator swordAnim;

    Attacks currentAttack;
    bool minCharge = false;
    bool triedToRelease = false;

    Animator anim;

    bool isAllowed = false; //BOOL GLOBAL DE TODO O SCRIPT
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        swordAnim = swordObject.GetComponent<Animator>();
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

    void StartDefenseState() //CHAMADA PELO SENDMESSAGE DO GAMEMANAGER
    {
        isAllowed = true;
    }

    void StopDefenseState()  //CHAMADA PELO SENDMESSAGE DO GAMEMANAGER
    { //HARD RESET
        isAllowed = false;
        StopAllCoroutines();
        swordObject.SetActive(false);
        anim.SetBool("isAttacking1", false);
        anim.SetBool("isAttacking2", false);
        anim.SetBool("isChargingAttack2", false);
        swordAnim.SetBool("isAttacking1", false);
        swordAnim.SetBool("isAttacking2", false);
        swordAnim.SetBool("isChargingAttack2", false);
        sword1.SetActive(false);
        sword2.SetActive(false);
        minCharge = false;
        triedToRelease = false;
        currentAttack = Attacks.NotAttacking;
    }


    void Move()
    {
        Xmovement = Input.GetAxisRaw("Horizontal") * Xvelocity;
        Ymovement = Input.GetAxisRaw("Vertical") * Yvelocity;

        rb2d.velocity = new Vector2(Xmovement, Ymovement);
        
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
                swordObject.SetActive(true);
                anim.SetBool("isChargingAttack2", true);
                swordAnim.SetBool("isChargingAttack2", true);
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
        swordObject.SetActive(true);
        anim.SetBool("isAttacking1", true);
        swordAnim.SetBool("isAttacking1", true);
        sword1.SetActive(true);
        currentAttack = Attacks.Attack1;
        yield return new WaitForSeconds(0.15f);
        sword1.SetActive(false);
        StartCoroutine(AttackCooldown());
        anim.SetBool("isAttacking1", false);
        swordAnim.SetBool("isAttacking1", false);
        swordObject.SetActive(false);
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
        anim.SetBool("isChargingAttack2", false);
        swordAnim.SetBool("isChargingAttack2", false);
        if (currentAttack == Attacks.Attack2Charge && minCharge == true)
        {
           
            StartCoroutine(Attack2());
        }
    }

    IEnumerator Attack2()
    {
        anim.SetBool("isAttacking2", true);
        swordAnim.SetBool("isAttacking2", true);
        sword2.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        sword2.SetActive(false);
        StartCoroutine(AttackCooldown());
        anim.SetBool("isAttacking2", false);
        swordAnim.SetBool("isAttacking2", false);
        swordObject.SetActive(false);
    }

    void Strike1() //CHAMADO POR SENDMESSAGE DA ATTACKMARK
    {
        StartCoroutine(StrikeAnim());
    }

    IEnumerator StrikeAnim()
    {
        swordObject.SetActive(true);
        anim.SetBool("isAttacking1", true);
        swordAnim.SetBool("isAttacking1", true);
        yield return new WaitForSeconds(0.15f);
        anim.SetBool("isAttacking1", false);
        swordAnim.SetBool("isAttacking1", false);
        swordObject.SetActive(false);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        currentAttack = Attacks.NotAttacking;
    }
}
