using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour
{
    public Toggle autoSaveToggle;
    // Start is called before the first frame update

    //void Awake()
    //{
    //    autoSaveToggle = GetComponent<Toggle>();

    //    autoSaveToggle.onValueChanged.AddListener();

    //}

    //void Start()
    //{
    //    InternalValueChanged(autoSaveToggle.)
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InternalValueChanged(bool value)
    {

    }
}
