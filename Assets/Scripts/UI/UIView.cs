using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [Header("Option UI")]
    [SerializeField] private GameObject OptionUI;

    [Header("InGame UI")]
    [SerializeField] private GameObject TechTreeUI;
    // Start is called before the first frame update
    private void Start()
    {
        OptionUI.gameObject.SetActive(false);
        TechTreeUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
