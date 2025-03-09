using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    [SerializeField] private TextAsset jsonFile;
    private Dictionary<string, Dictionary<string, string>> translations;
    private Dictionary<string, string> fonts;
    private string currentLanguage = "en";
    private Font currentFont;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalizationData();
            LoadLanguage(PlayerPrefs.GetString("Language", "en"));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLocalizationData()
    {
        if (jsonFile == null)
        {
            Debug.LogError("Not Found Localization.json");
            return;
        }

        LocalizationData data = JsonConvert.DeserializeObject<LocalizationData>(jsonFile.text);
        translations = data.translations;
        fonts = new Dictionary<string, string>();

        foreach (var locale in data.locales)
        {
            fonts[locale.languageCode] = locale.font;
        }
    }

    public void LoadLanguage(string languageCode)
    {
        if (!translations.ContainsKey(languageCode)) return;

        currentLanguage = languageCode;
        PlayerPrefs.SetString("Language", languageCode);
        PlayerPrefs.Save();
        UpdateAllLocalizedTexts();
    }

    public string GetLocalizedText(string key)
    {
        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            return translations[currentLanguage][key];
        }
        return key;
    }

    private void UpdateAllLocalizedTexts()
    {
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (var localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
    }
}

[System.Serializable]
public class LocalizationData
{
    public List<Locale> locales;
    public Dictionary<string, Dictionary<string, string>> translations;
}

[System.Serializable]
public class Locale
{
    public string languageCode;
    public string displayName;
    public string font;
}
