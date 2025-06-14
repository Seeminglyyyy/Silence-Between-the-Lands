using UnityEngine;
using TMPro;

public class Player_Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text  healthText;

    private void Start()
    {
        healthText.text = "HP : " + currentHealth + " / " + maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthText.text = "HP : " + currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
