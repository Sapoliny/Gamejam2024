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
    public TextMeshProUGUI healthUI;

    [Header("nao mexer")]
    public float bossHealth;

    bool isDead;

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
        healthUI.text = Mathf.Round(bossHealth).ToString();
        if (bossHealth <= 0)
        {
            bossHealth = 0;
            isDead = true;
            //SceneManager.LoadScene("xD");
        }

    }
}
