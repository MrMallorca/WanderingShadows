using System.Collections;
using System.Collections.Generic;
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

    int abilityCounter = 2;

    Animator anim;
    CharacterController controller;

    DeflectionZone deflectionZone;

    PlayerScript playerScript;

    [SerializeField] GameObject deflectParticle;

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        playerScript = GetComponent<PlayerScript>();

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
        if (!playerScript.isDead && canDeflect)
        {
            if(abilityCounter > 0)
            {
                StartCoroutine(Deflect());
            }

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
        audio.clip = playerScript.clips[3];
        audio.PlayOneShot(playerScript.clips[3]);
        canDeflect = false;
        isDeflecting = true;
        abilityCounter -= 1;

        deflectionZone.gameObject.SetActive(true);
        deflectParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(deflectTime);

        isDeflecting = false;

        yield return new WaitForSeconds(deflectCooldown);
        canDeflect = true;
        deflectionZone.gameObject.SetActive(false);
        deflectParticle.gameObject.SetActive(false);


    }
}
