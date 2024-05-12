using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private CharacterController characterController;
    private Vector3 localMovementDir;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (localMovementDir == Vector3.zero)
            return;

        Vector3 movementDir = transform.right * localMovementDir.x + transform.forward * localMovementDir.z;
        characterController.Move(movementDir * speed * Time.deltaTime);
    }
    public void SetDir(Vector2 dir)
    {
        localMovementDir = new Vector3(dir.x, 0, dir.y);
    }
}
