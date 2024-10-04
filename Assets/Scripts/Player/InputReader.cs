using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Player_Controls.IPlayerControlsActions
{
    public Vector2 moveComposite;
    [SerializeField]
    private bool isGrounded = true;

    [SerializeField]
    private float CHARGETIME = 1.0f, CHARGECOOLDOWN = 5.0f;
    [SerializeField]
    private bool isEmpowered = false;


    public Action OnJumpPerformed, OnAttackPerformed, OnBlockPerformed, OnChargeHeld, OnChargeCompleted;

    private Player_Controls controls;

    private void OnEnable()
    {
        if (controls != null)
            return;

        controls = new Player_Controls();
        controls.PlayerControls.SetCallbacks(this);
        controls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.Disable();
    }

    //MoveComposite is an xy vector that reads values from wasd or the left thumbstick
    public void OnMove(InputAction.CallbackContext context)
    {
        moveComposite = context.ReadValue<Vector2>();
    }

    //breaking this down: the if statement is to prevent us from calling context.performed twice
    //...(once when button is pressed, then again when button is released)
    //we invoke an action (and also check if it exists using the "?.")
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnJumpPerformed?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnAttackPerformed?.Invoke();
    }

    public void OnBlockParry(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnBlockPerformed?.Invoke();
    }


    //if we've pressed the button for charging then invoke the coroutine of ChargeCountdown
    public void OnCharge(InputAction.CallbackContext context)
    {
        if (!isEmpowered && isGrounded) //if we are grounded and not empowered already...
        {
            if (context.started) //...and the button is pressed
            {
                StartCoroutine(ChargeCountdown()); //start charging
            }
            else //... or when the button is released
            {
                StopCoroutine(ChargeCountdown()); //stop the charge early
            }
                
        } //...if we are already empowered...
        else
        {
            return; //then do nothing
        }

    }

    public IEnumerator ChargeCountdown()
    {
        OnChargeHeld?.Invoke(); //invoke our charging UnityEvent
        yield return new WaitForSeconds(CHARGETIME);

        OnChargeCompleted?.Invoke(); //invoke this when our charge is done
        isEmpowered = true; //set us to empowered
        print("YOU ARE NOW FULLY CHARGED");

        StartCoroutine(ChargeCooldown()); //begin our cooldown timer

    }

    public IEnumerator ChargeCooldown()
    {
        yield return new WaitForSeconds(CHARGECOOLDOWN);

        isEmpowered = false;
    }
}
