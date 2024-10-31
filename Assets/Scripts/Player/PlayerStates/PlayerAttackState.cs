using UnityEngine;

//THIS HANDLES MOVEMENT AND BLENDING OF OUR STATES
public class PlayerAttackState : PlayerBaseState
{
    private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Velocity.y = Physics.gravity.y;

        stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);

        //stateMachine.InputReader.OnJumpPerformed += ;
    }

    //each tick (aka update frame) we perform movement logic here
    public override void Tick()
    {
        if (!stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.moveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        //stateMachine.InputReader.OnJumpPerformed -= ;
    }

    private void SwitchToMoveState()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}