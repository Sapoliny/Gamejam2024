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
    public GameObject gameOver;
    public GameObject playerDamageIndicator;

    [Header("nao mexer")]
    public float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
        healthUI.text = playerHealth.ToString();
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

        GameObject indicator = Instantiate(playerDamageIndicator);
        indicator.GetComponent<DamageIndicator>().Show(damage,transform);

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            StartCoroutine(waitToDie());
            GetComponent<Animator>().SetBool("isDead", true);
        }
        
    }


    IEnumerator waitToDie()
    {
        gameOver.SetActive(true);
        Destroy(GetComponent<PlayerAttack>());
        Destroy(GetComponent<PlayerControl>());
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
