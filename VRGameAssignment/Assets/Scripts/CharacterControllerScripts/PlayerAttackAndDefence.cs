using UnityEngine;

public class PlayerAttackAndDefence : MonoBehaviour
{
    GameObject obj;
    HealthManager healthManager;
    public GameObject Player;
    public GameObject Enemy;
    public void PlayerFightControls()
    {
        if(this.gameObject.tag == "PlayerWeapon")
        {
            healthManager = Enemy.GetComponent<HealthManager>();
            healthManager.DamageManager(Enemy, Player);
        }
    }
    public void PlayerDefence()
    {
        if(this.gameObject.tag == "PlayerShield" || this.gameObject.tag == "PlayerWeapon")
        {
            this.GetComponent<AudioSource>().Play();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        obj = collision.gameObject;

        if(obj.tag == "Enemy")
        {
            PlayerFightControls();
        }
        if(obj.tag == "EnemyWeapon")
        {
            PlayerDefence();
        }
        if(obj.tag == "Player" && this.gameObject.tag == "EnemyWeapon")
        {
            healthManager = Player.GetComponent<HealthManager>();
            healthManager.DamageManager(Player, Enemy);
        }
    }
}
