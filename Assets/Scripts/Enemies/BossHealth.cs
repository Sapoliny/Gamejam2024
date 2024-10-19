using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth;

    [Header("nao mexer")]
    public float bossHealth;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
       bossHealth = maxHealth;
    }
    public void getHurt(float damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
        {
            bossHealth = 0;
            isDead = true;
            //SceneManager.LoadScene("xD");
        }

    }
}
