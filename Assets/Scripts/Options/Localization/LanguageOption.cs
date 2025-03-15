using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageOption : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private string selectedLanguageCode;
    private List<Locale> locales;

    void Start()
    {
        locales = LocalizationManager.Instance.GetAvailableLocales();
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var locale in locales)
        {
            options.Add(locale.displayName);
        }
        dropdown.AddOptions(options);

        string savedLanguage = PlayerPrefs.GetString("Language", "en");
        selectedLanguageCode = savedLanguage;

        dropdown.value = locales.FindIndex(l => l.languageCode == savedLanguage);
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        selectedLanguageCode = locales[index].languageCode;
    }

    public void ApplyLanguage()
    {
        LocalizationManager.Instance.LoadLanguage(selectedLanguageCode);
    }
}
