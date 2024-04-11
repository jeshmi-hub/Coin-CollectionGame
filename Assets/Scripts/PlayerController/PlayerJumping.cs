
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJumping : MonoBehaviour
{
    Player player;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpPressureBufferTime = .05f;
    [SerializeField] float jumpGroundGraceTime = .2f;


    bool tryingToJump;
    float lastJumpPressTime;
    float lastGroundedTime;
    void Awake()
    {
        player = GetComponent<Player>();    
        
    }

     void OnEnable()
    {
        player.OnBeforeMove += OnBeforeMove;
        player.OnGroundStateChange += OnGroundStateChange;
        
    }

  

    void OnDisable()
    {
        player.OnBeforeMove -= OnBeforeMove;
        player.OnGroundStateChange -= OnGroundStateChange;
    }

    void OnJump()
    {
        tryingToJump = true;
        lastJumpPressTime = Time.time;
    }


    void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressureBufferTime;
        bool wasGrounded = Time.time - lastGroundedTime < jumpGroundGraceTime;
        bool isOrWasTryingToJump = tryingToJump || (wasTryingToJump && player.IsGrounded);
        bool isOrWasGrounded = player.IsGrounded || wasGrounded;

        if(isOrWasTryingToJump && isOrWasGrounded)
        {
            player.velocity.y += jumpSpeed;

        }
        tryingToJump = false;
    }

    void OnGroundStateChange(bool isGround)
    {
        if (!isGround) lastGroundedTime = Time.time;
    }
}
