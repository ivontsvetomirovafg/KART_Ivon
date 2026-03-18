using System.Collections.Generic;
using UnityEngine;

public class LocalizacionManager : MonoBehaviour
{
    [SerializeField]
    private Lenguajes spanish;
    [SerializeField]
    private Lenguajes english;

    private Dictionary<string, string> spanishDictionary;
    private Dictionary<string, string> englishDictionary;
    public static LocalizacionManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
        spanishDictionary = new Dictionary<string, string>();
        englishDictionary = new Dictionary<string, string>();
        
        for(int i = 0;  i < spanishDictionary.Count; i++)
        {
            spanishDictionary.Add(spanish.keys[i], spanish.values[i]);
        }
    }

    void Start()
    {

    }

    public string GetTextValue(string _key)
    {
        string dictionaryValue = "";
        if (Application.systemLanguage == SystemLanguage.Spanish)
        {
            spanishDictionary.TryGetValue(_key, out dictionaryValue);
        }
        else if (Application.systemLanguage == SystemLanguage.English)
        {
            englishDictionary.TryGetValue(_key, out dictionaryValue);
        }
        return dictionaryValue;
    }
}
