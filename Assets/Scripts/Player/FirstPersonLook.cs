using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    private void Start()
    {
        SetMouseLockState(true);
    }

    public void RotateTowards(Vector2 dir)
    {
        transform.Rotate(Vector3.up, dir.x * mouseSensitivity);
        Camera.main.transform.Rotate(Vector3.right, -dir.y * mouseSensitivity);
    }
    public void SetMouseLockState(bool shouldBeLocked)
    {
        if (shouldBeLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
