using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] Button back;
    [SerializeField] GameObject mainMenu;

    [SerializeField] AudioMixer audioMixer;
    // Start is called before the first frame update

    private void OnEnable()
    {
        back.onClick.AddListener(BackMainMenu);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }
}
