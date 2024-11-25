using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSlideOfTutorial : MonoBehaviour
{
    [SerializeField] Button leftClickBtn;
    [SerializeField] Button rightClickBtn;

    [SerializeField] Button back;
    [SerializeField] GameObject mainMenu;

    Animator[] btnAnimator;

    [SerializeField] GameObject ninjaCanvas;
    [SerializeField] GameObject samuraiCanvas;


    CanvasGroup interactableCanvas;
    // Start is called before the first frame update

    private void OnEnable()
    {
        back.onClick.AddListener(BackMainMenu);
        leftClickBtn.onClick.AddListener(ChangeLeftSlide);
        rightClickBtn.onClick.AddListener(ChangeRightSlide);

    }
    void Start()
    {
        interactableCanvas = mainMenu.GetComponent<CanvasGroup>();
        btnAnimator = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BackMainMenu()
    {
        interactableCanvas.interactable = true;
        gameObject.SetActive(false);
    }

    public void ChangeLeftSlide()
    {
        btnAnimator[1].SetTrigger("Clicked");
        if (ninjaCanvas.activeSelf)
        {
            ninjaCanvas.SetActive(false);
            samuraiCanvas.SetActive(true);
        }
        else
        {
            ninjaCanvas.SetActive(true);
            samuraiCanvas.SetActive(false);
        }


    }
    public void ChangeRightSlide()
    {
        btnAnimator[0].SetTrigger("Clicked");

        if (samuraiCanvas.activeSelf)
        {
            samuraiCanvas.SetActive(false);
            ninjaCanvas.SetActive(true);
        }
        else
        {
            samuraiCanvas.SetActive(true);
            ninjaCanvas.SetActive(false);
        }


    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(BackMainMenu);
        leftClickBtn.onClick.RemoveListener(ChangeLeftSlide);
        rightClickBtn.onClick.RemoveListener(ChangeRightSlide);

    }
}
