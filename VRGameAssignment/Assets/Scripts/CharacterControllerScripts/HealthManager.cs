using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int fullHealth;
    private int currentHealth;
    public int damage;
    private int enemyDamage;
    private float healthPercentage;
    public GameObject damagePannel1;
    public GameObject damagePannel2;
    public GameObject damagePannel3;
    public GameObject deathScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void DamageVisuals(){
        healthPercentage = ((float)currentHealth / fullHealth) * 100f;
        if (healthPercentage <= 30 && healthPercentage > 20)
        {
            damagePannel1.SetActive(true);
            damagePannel2.SetActive(false);
            damagePannel3.SetActive(false);
        }
        else if(healthPercentage <= 20 && healthPercentage > 10)
        {
            damagePannel1.SetActive(false);
            damagePannel2.SetActive(true);
            damagePannel3.SetActive(false);
        }
        else if(healthPercentage <= 10 && healthPercentage > 0)
        {
            damagePannel1.SetActive(false);
            damagePannel2.SetActive(false);
            damagePannel3.SetActive(true);
        }
        else if (healthPercentage <= 0)
        {
            damagePannel1.SetActive(false);
            damagePannel2.SetActive(false);
            damagePannel3.SetActive(false);
            deathScreen.SetActive(true);
        }

    }
    public void DamageManager(GameObject ActiveBody, GameObject obj)
    {
        enemyDamage = obj.GetComponent<HealthManager>().damage;

        if (ActiveBody.tag == "Player")
        {
            this.currentHealth -= enemyDamage;
            Debug.Log("Player Health: " + this.currentHealth);
            DamageVisuals();
            if (this.currentHealth <= 0)
            {
                Debug.Log("Player has died");///make a death screen
            }
        }
        if(ActiveBody.tag == "Enemy")
        {
            this.currentHealth -= enemyDamage;
            Debug.Log("Enemy Health: " + this.currentHealth);
            if (this.currentHealth <= 0)
            {
                Debug.Log("Enemy has died");///make a death animation
            }
        }

    }
    void Start()
    {
        currentHealth = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
