using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public GameObject healthBar;
   //bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHurt(float damage) 
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            playerHealth = 0;
            //isDead = true;
            SceneManager.LoadScene("xD");
        }
    }
}
