using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(FirstPersonLook))]
public class InputReader : MonoBehaviour
{
    private PlayerInput input;
    private CharacterMovement playerMovement;
    private FirstPersonLook playerLook;

    void Start()
    {
        playerMovement = GetComponent<CharacterMovement>();
        playerLook = GetComponent<FirstPersonLook>();
        input = new PlayerInput();
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.Look.performed += OnLookPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(context.ReadValue<Vector2>());
    }
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        playerMovement.SetDir(Vector2.zero);
    }
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        playerLook.RotateTowards(context.ReadValue<Vector2>());
    }
}
