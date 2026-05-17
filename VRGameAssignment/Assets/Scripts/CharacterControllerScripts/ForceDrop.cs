using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class ForceDrop : MonoBehaviour
{
    public InputActionReference dropAction;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor;

    void OnEnable()
    {
        dropAction.action.performed += Drop;
    }

    void OnDisable()
    {
        dropAction.action.performed -= Drop;
    }

    void Drop(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (interactor.hasSelection)
        {
            interactor.interactionManager.SelectExit(
                interactor,
                interactor.firstInteractableSelected
            );
        }
    }
}