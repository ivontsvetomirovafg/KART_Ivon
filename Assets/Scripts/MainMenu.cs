using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panelSlots;
    
    public void StartButton()
    {
        panelSlots.SetActive(true);
    }

    public void SlotButton(int coche)
    {
        GameManager.instance.coche = coche;
        SceneManager.LoadScene("Race");
    }
}
