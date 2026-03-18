using TMPro;
using UnityEngine;

public class TextLocalizacion : MonoBehaviour
{
    private TextMeshProUGUI text; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = LocalizacionManager.instance.GetTextValue(text.text);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
