using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour, ICharacterStatus
{
    public static PlayerScript currentPlayer;

    public float speed;

   
    public float knockBackForce = 10f;
    public float knockBackUp = 2f;
    float knockbackDuration = 0.2f;


    bool isInvunerable;
    public bool isDead;
    public bool startGame;


    Animator anim;

    CharacterController controller;

    float verticalVelocity;
    const float gravity = -9.81f;
    public float jumpForce;

    int jumpCount;

    [SerializeField] InputActionReference jump;

    public bool isHitted;

    public bool Hit => isHitted;
    public bool Dead => isDead;
    public bool GameBegin => startGame;



    private void OnEnable()
    {

        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;

    }

  

    void Start()
    {
        currentPlayer = this;


        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();

        isDead = false;
        startGame = false;


        StartCoroutine(StartGame());


        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (startGame == true )
        {
            if(!isInvunerable && isDead == false )
            {
                Vector3 movement = Vector3.right * speed * Time.fixedDeltaTime + 
                    Vector3.up * verticalVelocity * Time.deltaTime;
                controller.Move(movement);


                
            }
            
        }


        if (controller.isGrounded)
        {
            jumpCount = 0;
            verticalVelocity = 0;
        }

        if (GameLogic.vidas <= 0 && isDead == false) 
        {
            isDead = true;
            anim.SetTrigger("isDead");
        }

       


        verticalVelocity += gravity * Time.deltaTime;
        anim.SetBool("isJumping", !controller.isGrounded);

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Damageable"))
        {
            StartCoroutine(KnockBack());
            

        }
    }

    IEnumerator  KnockBack()
    {
        isHitted = true;

        StartCoroutine(Hitted());

        yield return new WaitForSeconds(knockbackDuration);

        isInvunerable = false;
        isHitted = false;


    }

    IEnumerator StartGame()
    {
        if(gameObject.name == "Ninja")
        {
            yield return new WaitForSeconds(7.5f);
        }
       
        startGame = true;
        anim.SetBool("startGame", true);
    }

    IEnumerator Hitted()
    {
        isInvunerable = true;
        anim.SetBool("Hitted", true);

        Physics.IgnoreLayerCollision(6, 7, true);

        yield return new WaitForSeconds(4);
        anim.SetBool("Hitted", false);
        Physics.IgnoreLayerCollision(6, 7, false);

    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (startGame)
        {
            if (controller.isGrounded)
            {

                anim.SetBool("isJumping", true);

                verticalVelocity = jumpForce;

            }
        }

        if (ctx.performed && startGame)
        {
            if (gameObject.name == "Samurai(Clone)")
            {
                if (jumpCount < 2)
                {

                    verticalVelocity = jumpForce;

                    jumpCount++;

                }
            }

           
        }

    }

    private void OnDisable()
    {

        jump.action.performed -= OnJump;
        jump.action.canceled -= OnJump;


        jump.action.Disable();

    }


}
