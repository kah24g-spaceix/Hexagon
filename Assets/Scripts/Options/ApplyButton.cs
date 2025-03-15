using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyButton : MonoBehaviour
{
    [SerializeField] private Button applyButton;
    ResolutionOption resolution;
    LanguageOption language;
    private void Awake()
    {
        resolution = GetComponent<ResolutionOption>();
        language = GetComponent<LanguageOption>();
    }
    private void Start()
    {
        applyButton.onClick.AddListener(Apply);
    }
    public void Apply()
    {
        resolution.Apply();
        language.ApplyLanguage();
    }
}
