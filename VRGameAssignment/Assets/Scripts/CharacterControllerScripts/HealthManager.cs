using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int fullHealth;
    private int currentHealth;
    public int damage;
    private int enemyDamage;
    private string ActiveBody;
    private int healthPercentage;
    private GameObject obj;
    public GameObject damagePannel1;
    public GameObject damagePannel2;
    public GameObject damagePannel3;
    public GameObject deathScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void DamageVisuals(){
        healthPercentage = (currentHealth / fullHealth) * 10;
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
    private void DamageManager(string ActiveBody, GameObject obj)
    {
        obj.GetComponent<HealthManager>().damage = enemyDamage;
        currentHealth = currentHealth - enemyDamage;

        if(ActiveBody == "Player")
        {
            Debug.Log("Player Health: " + currentHealth);
            DamageVisuals();
            if (currentHealth <= 0)
            {
                Debug.Log("Player has died");///make a death screen
            }
        }
        if(ActiveBody == "Enemy")
        {
            Debug.Log("Enemy Health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("Enemy has died");///make a death animation
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        obj = collision.gameObject;
        if (collision.gameObject.tag == "Enemy")
        {
            ActiveBody = "Player";
            DamageManager(ActiveBody, obj);
        }
        if(collision.gameObject.tag == "Player")
        {
            ActiveBody = "Enemy";
            DamageManager(ActiveBody, obj);
        }
    }
    void Start()
    {
        currentHealth = fullHealth;
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
