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

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

   




}
