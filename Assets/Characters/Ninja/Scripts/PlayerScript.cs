using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    float speed = 4.0f;

    [SerializeField] InputActionReference jump;
    private float jumpForce = 6f;
    bool isOnGround;

    int vidas = 2;
    public Sprite[] healthSprites;
    public Image healthBar;

    public float knockBackForce = 10f;
    public float knockBackUp = 2f;

    bool isInvunerable;
    public static bool isDead;
    bool startGame = false;
    float knockbackDuration = 0.2f;

    Animator anim;
    // Start is called before the first frame update

    private void OnEnable()
    {
        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isDead = false;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInvunerable && startGame == true )
        {
            if(isDead == false)
            {
                Vector3 movement = Vector3.right * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
            }
            
        }
        if(vidas == 0) 
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
        if (isOnGround)
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
        else if(collision.gameObject.CompareTag("Obstacle"))
        {

            StartCoroutine(KnockBack());
            vidas--;
            healthBar.sprite = healthSprites[vidas];

        }

    }

    IEnumerator  KnockBack()
    {
        isInvunerable = true;
        StartCoroutine(Hitted());
        Vector2 knockBackDirection = new Vector2(transform.position.y + 3, 0);
        rb.velocity = new Vector2(knockBackDirection.x, knockBackUp) * knockBackForce;

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
        anim.SetBool("Hitted", true);
        yield return new WaitForSeconds(2);
        anim.SetBool("Hitted", false);

    }





}
