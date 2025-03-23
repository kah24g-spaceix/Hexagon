using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    [SerializeField] private TextAsset jsonFile;
    private Dictionary<string, Dictionary<string, string>> translations;
    private List<Locale> locales;
    private string currentLanguage = "en";

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
            Debug.LogError("Localization.json 파일을 찾을 수 없습니다!");
            return;
        }

        LocalizationData data = JsonConvert.DeserializeObject<LocalizationData>(jsonFile.text);
        translations = data.translations;
        locales = data.locales;
    }

    public void LoadLanguage(string languageCode)
    {
        if (!translations.ContainsKey(languageCode)) return;

        currentLanguage = languageCode;
        PlayerPrefs.SetString("Language", languageCode);
        PlayerPrefs.Save();
    }

    public string GetLocalizedText(string key)
    {
        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            return translations[currentLanguage][key];
        }
        return key;
    }

    public List<Locale> GetAvailableLocales()
    {
        return locales;
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
}
