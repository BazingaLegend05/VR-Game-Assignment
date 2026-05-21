using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ChargedAttack : MonoBehaviour
{
    public int hitsRequired = 5;
    public GameObject fireParticles;
    public AudioSource chargeSound;
    public GameObject TriggerPanel;
    GameObject Player;
    HealthManager healthManager;

    private int currentHits = 0;
    private bool isCharged = false;
    private bool isAbilityActive = false;

    [SerializeField] private InputActionProperty controllerButton;

    private void OnEnable()
    {
        if (controllerButton.action != null)
        {
            controllerButton.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (controllerButton.action != null)
        {
            controllerButton.action.Disable();
        }
    }

    private void Start()
    {
        if (TriggerPanel != null) TriggerPanel.SetActive(false);
        if (fireParticles != null) fireParticles.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("PlayerParent");
        healthManager = Player.GetComponent<HealthManager>();
    }

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

    private void Update()
    {
        if (isCharged)
        {
            if (controllerButton.action != null && controllerButton.action.WasPressedThisFrame())
            {
                ActivateGladiatorRage();
            }
        }
    }

    private void TriggerCharge()
    {
        isCharged = true;
        if (TriggerPanel != null) TriggerPanel.SetActive(true);
        if (chargeSound != null) chargeSound.Play();
    }

    public void ActivateGladiatorRage()
    {
        isAbilityActive = true;
        isCharged = false;
        currentHits = 0;

        healthManager.damage = healthManager.damage * 2;
        if (TriggerPanel != null) TriggerPanel.SetActive(false);
        if (fireParticles != null) fireParticles.SetActive(true);
        if (chargeSound != null) chargeSound.Play();

        Invoke(nameof(ResetAbility), 5f);
    }

    void ResetAbility()
    {
        healthManager.damage = healthManager.damage / 2;
        isAbilityActive = false;
        if (fireParticles != null) fireParticles.SetActive(false);
    }

    public bool IsHeavyAttackActive() => isAbilityActive;
}
