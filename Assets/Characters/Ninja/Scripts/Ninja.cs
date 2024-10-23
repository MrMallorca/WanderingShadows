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

    float dashForce = 11f;
    bool isDashing;
    bool canDash = true;
    float dashingTime = 0.1f;
    float dashingCooldown = 1f;

    private TrailRenderer tr;

    Animator anim;

    Rigidbody rb;


    // Start is called before the first frame update

    private void OnEnable()
    {

        ability.action.Enable();

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

    private void OnDisable()
    {


        ability.action.performed -= OnAbility;
        ability.action.canceled -= OnAbility;

        ability.action.Disable();

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
        rb.useGravity = false;
        Physics.IgnoreLayerCollision(6, 7, true);
        anim.SetBool("isDashing", true);
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
