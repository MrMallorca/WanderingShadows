using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    float speed = 5.0f;

    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference ability;

    float dashForce = 11f;
    bool isDashing;
    bool canDash = true;
    float dashingTime = 0.1f;
    float dashingCooldown = 1f;

    private TrailRenderer tr;

    float jumpForce = 6f;
    public bool isOnGround;

    int vidas = 2;
    public Sprite[] healthSprites;
    public Image healthBar;

    public float knockBackForce = 10f;
    public float knockBackUp = 2f;
    float knockbackDuration = 0.2f;


    bool isInvunerable;
    public static bool isDead;
    bool startGame = false;


    GameObject obstacle;

    Animator anim;

    string escenaACambiar = "Level1";

    private void OnEnable()
    {
        jump.action.Enable();

        ability.action.Enable();

        ability.action.performed += OnAbility;
        ability.action.canceled += OnAbility;

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        isDead = false;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        //Borrar comentarios luego
        if (startGame == true )
        {
            if(!isInvunerable && isDead == false )
            {
                

                Vector3 movement = Vector3.right * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
            }
            
        }
        if(vidas == 0 && isDead == false) 
        {
            isDead = true;
            anim.SetTrigger("isDead");
        }

    }

    private void OnDisable()
    {
       

        jump.action.performed -= OnJump;
        jump.action.canceled -= OnJump;

        ability.action.performed -= OnAbility;
        ability.action.canceled -= OnAbility;

        ability.action.Disable();

        jump.action.Disable();
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (isOnGround && startGame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("isJumping", true);

            isOnGround = false;

        }
    }
    void OnAbility(InputAction.CallbackContext ctx)
    {
        if(!isDead && canDash) 
        {
            StartCoroutine(Dash());

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag("Obstacle") 
            || collision.gameObject.CompareTag("Damageable"))
        {

            StartCoroutine(KnockBack());
            vidas--;
            healthBar.sprite = healthSprites[vidas];

        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfBounds"))
        {
            SceneManager.LoadScene(escenaACambiar);
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
        yield return new WaitForSeconds(4);
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        if(isOnGround)
        {
            rb.useGravity = false;
        }
        Physics.IgnoreLayerCollision(6, 7, true);
        anim.SetBool("isDashing",true);
        rb.velocity = new Vector3(transform.localScale.x * dashForce, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        anim.SetBool("isDashing", false);
        Physics.IgnoreLayerCollision(6, 7, false);
        canDash = true;
    }





}
