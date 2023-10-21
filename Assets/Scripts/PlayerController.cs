using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;



public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody_;
    public float max_velocity = 10f;
    public float acceleration = 5f;
    public float jump_impulse = 9.8f;
    public float jump_hold_force = 9.81f;

    public float air_velocity_multiplier = .3f;
    private bool is_jumping;
    private bool just_landed;
    private bool holding_jump;
    private float hold_timer;
    public float max_hold_jump_timer;
    
    private Animator anim;
    private SpriteRenderer sprite_renderer;
    PlayerActions player_actions;
    void Awake ()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        player_actions = new PlayerActions();
        player_actions.General.Enable();

        anim = GetComponentInChildren<Animator>();
        sprite_renderer = GetComponentInChildren<SpriteRenderer>();

        player_actions.General.Jump.started += Jump;
        player_actions.General.Jump.performed += ContinueJump;
        player_actions.General.Jump.canceled += context => {
            holding_jump = false;
        };
    }

    public void ContinueJump (InputAction.CallbackContext context) 
    {
        if (context.interaction is HoldInteraction) {
            holding_jump = true;
            hold_timer = 0;
        }
    }
    
    public void Jump (InputAction.CallbackContext context)
    {
        if (!is_jumping) {
            rigidbody_.AddForce(Vector2.up * jump_impulse, ForceMode2D.Impulse);
            is_jumping = true;
        }
    }

    void HandleAnim()
    {   
        if (is_jumping) {
            anim.SetBool("IsJumping", true);
            anim.SetFloat("JumpState", 1);
        }
        if (just_landed) {
            anim.SetFloat("JumpState", 0);
            anim.SetBool("IsJumping", false);
        }

        if (Mathf.Abs(rigidbody_.velocity.x) > 0) {
            anim.SetFloat("Move", 1);
        } else {
            anim.SetFloat("Move", 0);
        }
    }

    void FixedUpdate()
    {
        float input_ = player_actions.General.Move.ReadValue<float>();

        if (input_ < 0) {
            sprite_renderer.flipX = true;
        } else if (input_ > 0) {
            sprite_renderer.flipX = false;
        }

        if (Mathf.Abs(rigidbody_.velocity.x) <= max_velocity) {
            float mult = is_jumping ? air_velocity_multiplier : 1;
            rigidbody_.AddForce(input_ * mult * acceleration * Vector2.right);
        } else {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x > 0 ? max_velocity : -1 * max_velocity, rigidbody_.velocity.y);
        }

        bool is_jumping_before = is_jumping;
        is_jumping = Mathf.Abs(rigidbody_.velocity.y) > 0;
        just_landed = is_jumping ^ is_jumping_before;

        if (is_jumping && hold_timer < max_hold_jump_timer && holding_jump) {
            hold_timer += Time.fixedDeltaTime;
            rigidbody_.AddForce(Vector2.up * jump_hold_force, ForceMode2D.Force);
        } else {
            holding_jump = false;
            hold_timer = 0;
        }

        HandleAnim();

        
        
    }
}
