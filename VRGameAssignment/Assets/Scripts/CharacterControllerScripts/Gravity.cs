using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Gravity : MonoBehaviour
{
    public float gravity = -9.81f;
    public float groundedForce = -2f;

    private CharacterController controller;
    private float velocityY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && velocityY < 0)
        {
            velocityY = groundedForce;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 move = new Vector3(0, velocityY, 0);

        controller.Move(move * Time.deltaTime);
    }
}
