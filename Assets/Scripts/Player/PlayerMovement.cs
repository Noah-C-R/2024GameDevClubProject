using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 vel;

    public float movementSpeed = 5f;

    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float gravity = -16f;
    [SerializeField]
    private float lookRotationDampFactor = 30f;

    private Transform mainCamera;
    private InputReader inputReader;
    private Animator animator;
    private CharacterController controller;

    private void Start()
    {
        mainCamera = Camera.main.transform;

        inputReader = InputReader.Instance;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();

        //*********************should put this in an if check that depends on our gamestate
        CalculateMoveDirection(); //firgure out which way to face
        FaceMoveDirection(); //rotate that direction
        Move(); //move that way

    }

    private  void CalculateMoveDirection()
    {
        Vector3 cameraForward = new(mainCamera.forward.x, 0, mainCamera.forward.z);
        Vector3 cameraRight = new(mainCamera.right.x, 0, mainCamera.right.z);

        Vector3 moveDirection = cameraForward.normalized * inputReader.moveComposite.y + cameraRight.normalized * inputReader.moveComposite.x;

        vel.x = moveDirection.x * movementSpeed;
    }

    private void FaceMoveDirection()
    {
        Vector3 faceDirection = new(vel.x, 0f, vel.z);

        if (faceDirection == Vector3.zero)
            return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(faceDirection), lookRotationDampFactor * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (vel.y > gravity)
        {
            vel.y += gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        controller.Move(vel * Time.deltaTime);
    }
}
