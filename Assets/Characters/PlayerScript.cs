using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    public Sprite[] healthSprites;
    Image healthBar;

    [SerializeField] GameObject healthBarObj;

    public float knockBackForce = 10f;
    public float knockBackUp = 2f;
    float knockbackDuration = 0.2f;


    bool isInvunerable;
    public static bool isDead;
    public static bool startGame = false;


    GameObject obstacle;

    Animator anim;

    string escenaACambiar = "Level1";

    GameLogic gameLogicScript;

    CharacterController controller;

    float verticalVelocity;
    const float gravity = -9.81f;
    public float jumpForce;

    int jumpCount;

    [SerializeField] InputActionReference jump;

    private void OnEnable()
    {

        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;

    }

    void Start()
    {
        GameObject gameLogicObject = GameObject.FindGameObjectWithTag("GameLogic");
        gameLogicScript = gameLogicObject.GetComponent<GameLogic>();

        healthBar = healthBarObj.GetComponentInChildren<Image>();

        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        controller = GetComponent<CharacterController>();

        isDead = false;

        
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

        if (gameLogicScript != null)
        {

                if (gameLogicScript.vidas <= 0 && isDead == false) 
                {
                      isDead = true;
                      anim.SetTrigger("isDead");
                }
        }

        verticalVelocity += gravity * Time.deltaTime;
        anim.SetBool("isJumping", !controller.isGrounded);

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Damageable"))
        {
            StartCoroutine(KnockBack());
            gameLogicScript.vidas = gameLogicScript.vidas - 1;
            healthBar.sprite = healthSprites[gameLogicScript.vidas];

        }
    }


    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfBounds"))
        {
        }
    }

    IEnumerator  KnockBack()
    {

        StartCoroutine(Hitted());


        //Vector2 knockBackDirection = new Vector2(transform.position.y + 3, 0);
        //rb.velocity = new Vector2(knockBackDirection.x, knockBackUp) * knockBackForce;


        yield return new WaitForSeconds(knockbackDuration);

        isInvunerable = false;

      
    }

    IEnumerator StartGame()
    {
        if(gameObject.name == "Ninja")
        {
            yield return new WaitForSeconds(4);
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
        if (PlayerScript.startGame)
        {
            if (controller.isGrounded)
            {

                anim.SetBool("isJumping", true);

                verticalVelocity = jumpForce;

            }
        }

        if (ctx.performed && PlayerScript.startGame)
        {
            if (gameObject.name == "Samurai")
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
