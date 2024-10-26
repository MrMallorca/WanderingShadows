using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;

public class Samurai : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] InputActionReference ability;

    bool isDeflecting;
    bool canDeflect = true;
    float deflectTime = 1f;
    float deflectCooldown = 2f;

    Animator anim;
    CharacterController controller;

    DeflectionZone deflectionZone;


    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        deflectionZone = GetComponentInChildren<DeflectionZone>(); 

        deflectionZone.gameObject.SetActive(false);
    }
    private void OnEnable()
    {

        ability.action.Enable();

        ability.action.performed += OnAbility;
        ability.action.canceled += OnAbility;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnAbility(InputAction.CallbackContext ctx)
    {
        if (!PlayerScript.isDead && canDeflect)
        {
            StartCoroutine(Deflect());

        }

    }

    private void OnDisable()
    {


        ability.action.performed -= OnAbility;
        ability.action.canceled -= OnAbility;


        ability.action.Disable();

    }

    private IEnumerator Deflect()
    {
        canDeflect = false;
        isDeflecting = true;

        deflectionZone.gameObject.SetActive(true);
        yield return new WaitForSeconds(deflectTime);

        isDeflecting = false;

        yield return new WaitForSeconds(deflectCooldown);
        canDeflect = true;
        deflectionZone.gameObject.SetActive(false);

    }
}
