using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Samurai : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] InputActionReference jump;

    Rigidbody rb;

    Animator anim;

    int jumpCount;
    public float jumpForce;
    public bool isOnGround;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    private void OnEnable()
    {
        jump.action.Enable();

        jump.action.performed += OnJump;
        jump.action.canceled += OnJump;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(jumpCount);

    }
    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && PlayerScript.startGame)
        {
            
            if(jumpCount < 2)
            {
                
                    rb.velocity = Vector3.up * jumpForce;
                    anim.SetBool("isJumping", true);
                    jumpCount++;
                    isOnGround = false;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            jumpCount = 0;
            isOnGround = true;
        }
    }

    private void OnDisable()
    {


        jump.action.performed -= OnJump;
        jump.action.canceled -= OnJump;


        jump.action.Disable();
    }
}
