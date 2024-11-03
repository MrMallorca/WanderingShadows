using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    [SerializeField] InputActionReference ability;

    private Vector3 dashDirection;
    float dashForce = 30f;
    bool isDashing;
    bool canDash = true;
    float dashingTime = 0.1f;
    float dashingCooldown = 1f;

    int abilityCounter = 2;

    private TrailRenderer tr;

    Animator anim;


    CharacterController controller;

    PlayerScript playerScript;

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
        controller = GetComponent<CharacterController>();

        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            controller.Move(dashDirection * dashForce * Time.fixedDeltaTime);
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
        if (!playerScript.isDead && canDash)
        {
            if (abilityCounter > 0)
            {
                StartCoroutine(Dash());
            }
        }

    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashDirection = transform.forward;
        Physics.IgnoreLayerCollision(6, 7, true);
        
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        Physics.IgnoreLayerCollision(6, 7, false);
        canDash = true;
    }

}
