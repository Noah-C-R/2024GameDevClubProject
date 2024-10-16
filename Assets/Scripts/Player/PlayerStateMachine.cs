using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))] //forces us to make sure we have an animator, inputreader, and charactercontroller
public class PlayerStateMachine : StateMachine
{
    public Vector3 Velocity;
    [field: SerializeField]
    public float MovementSpeed { get; private set; } = 5f;
    [field: SerializeField]
    public float JumpForce { get; private set; } = 5f;
    [field: SerializeField]
    public float Gravity { get; private set; } = -16f;
    public float LookRotationDampFactor { get; private set; } = 30f;
    public Transform MainCamera { get; private set; }
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main.transform;

        InputReader = GetComponent<InputReader>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();

        SwitchState(new PlayerMoveState(this));
    }
}