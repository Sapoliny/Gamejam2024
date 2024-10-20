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

    public AudioClip damageSound; // The sound to play when the player takes damage
    private AudioSource audioSource; // The AudioSource component
    GameObject bg;

    [Header("nao mexer")]
    public float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
        healthUI.text = playerHealth.ToString();
        audioSource = GetComponent<AudioSource>();
        bg = GameObject.Find("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHurt(float damage) 
    {
        DamageSound();

        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            healthBar.GetComponent<Slider>().value = playerHealth / maxHealth;
            healthUI.text = playerHealth.ToString();
            StartCoroutine(waitToDie());
            GetComponent<Animator>().SetBool("isDead", true);
        }
        healthBar.GetComponent<Slider>().value = playerHealth / maxHealth;
        healthUI.text = playerHealth.ToString(); 
        
    }

    void DamageSound()
    {
        audioSource.volume = bg.GetComponent<AudioSource>().volume;
        audioSource.PlayOneShot(damageSound);
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
