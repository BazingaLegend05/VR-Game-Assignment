using UnityEngine;

public class ChargedAttack : MonoBehaviour
{
    public int hitsRequired = 5;
    public ParticleSystem fireParticles;
    public AudioSource chargeSound;

    private int currentHits = 0;
    private bool isCharged = false;
    private bool isAbilityActive = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isCharged)
        {
            currentHits++;
            if (currentHits >= hitsRequired)
            {
                TriggerCharge();
            }

        }
        if (chargeSound != null) chargeSound.Play();
    }
    private void TriggerCharge()
    {
        isCharged = true;
        if (fireParticles != null) fireParticles.Play();
        if (chargeSound != null) chargeSound.Play();
    }
    public void ActivateGladiatorRage()
    {
        if (isCharged)
        {
            isAbilityActive = true;
            isCharged = false;
            currentHits = 0;

            Invoke(nameof(ResetAbility), 5f);
        }
    }
    void ResetAbility()
    {
        isAbilityActive = false;
        if (fireParticles != null) fireParticles.Stop();
    }
    public bool IsHeavyAttackActive() => isAbilityActive;
}
