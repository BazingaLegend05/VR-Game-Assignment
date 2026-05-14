using UnityEngine;
using UnityEngine.InputSystem;

public class RayVisibilityController : MonoBehaviour
{
    public string handSide = "left";
    private InputAction triggerAction;
    private LineRenderer laserLine;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        triggerAction = new InputAction(binding: $"<XRController>{{{handSide}Hand}}/triggerPressed");
        triggerAction.Enable();
    }

    void Update()
    {
        //Turn laser visual on when trigger is pressed down and off when let go
        laserLine.enabled = triggerAction.IsPressed();
    }
}
