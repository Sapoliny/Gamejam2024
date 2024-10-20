using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour
{
    public float maxHealth;
    public GameObject healthBar;
    public GameObject player;
    public GameObject gameWin;
    public TextMeshProUGUI healthUI;

    [Header("nao mexer")]
    public float bossHealth;


    // Start is called before the first frame update
    void Start()
    {
       bossHealth = maxHealth;
       healthUI.text = bossHealth.ToString();
    }
    public void getHurt(float damage)
    {
        bossHealth -= damage;
        healthBar.GetComponent<Slider>().value = bossHealth / maxHealth;
        if (bossHealth <= 0)
        {
            bossHealth = 0;
            StartCoroutine(waitToWin());
        }
        healthUI.text = Mathf.Round(bossHealth).ToString();
    }

    IEnumerator waitToWin()
    {
        gameWin.SetActive(true);
        Destroy(player.GetComponent<PlayerAttack>());
        Destroy(player.GetComponent<PlayerControl>());

        gameObject.SendMessage("SelfStop"); //O SCRIPT DO BOSS DE AUTODESTRUIR-SE AO RECEBER ISTO

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}

