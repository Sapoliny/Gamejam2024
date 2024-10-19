using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public GameObject healthBar;
    public TextMeshProUGUI healthUI;
    bool isDead;

    [Header("nao mexer")]
    public float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHurt(float damage) 
    {
        playerHealth -= damage;
        healthBar.GetComponent<Slider>().value = playerHealth / maxHealth;
        healthUI.text = playerHealth.ToString(); 
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            isDead = true;
            //SceneManager.LoadScene("xD");
        }
        
    }
}
