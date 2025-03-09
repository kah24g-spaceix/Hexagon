using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public void ChangeLanguage(string languageCode)
    {
        LocalizationManager.Instance.LoadLanguage(languageCode);
    }
}
