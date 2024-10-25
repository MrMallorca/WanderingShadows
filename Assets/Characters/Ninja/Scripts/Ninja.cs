using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    [SerializeField] InputActionReference ability;

    float dashForce = 5f;
    bool isDashing;
    bool canDash = true;
    float dashingTime = 0.1f;
    float dashingCooldown = 1f;

    private TrailRenderer tr;

    Animator anim;

    Rigidbody rb;

    [SerializeField] InputActionReference jump;

    public float jumpForce;
    public bool isOnGround;



    // Start is called before the first frame update

    private void OnEnable()
    {

        ability.action.Enable();

        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;

        ability.action.performed += OnAbility;
        ability.action.canceled += OnAbility;

       
    }

  
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if(PlayerScript.startGame)
        {
            if (isOnGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                anim.SetBool("isJumping", true);

                isOnGround = false;

            }
        }
        
    }

    private void OnDisable()
    {


        ability.action.performed -= OnAbility;
        ability.action.canceled -= OnAbility;

      

            jump.action.performed -= OnJump;
            jump.action.canceled -= OnJump;


            jump.action.Disable();
        

        ability.action.Disable();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            isOnGround = true;
        }
    }

    void OnAbility(InputAction.CallbackContext ctx)
    {
        if (!PlayerScript.isDead && canDash)
        {
            StartCoroutine(Dash());

        }

    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Physics.IgnoreLayerCollision(6, 7, true);
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.AddForce(gameObject.transform.forward * dashForce, ForceMode.Impulse);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.useGravity = false;
        tr.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        Physics.IgnoreLayerCollision(6, 7, false);
        canDash = true;
    }

}
