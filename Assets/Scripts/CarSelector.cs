using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public int carIndex;

    public MonoBehaviour playerScript;
    public MonoBehaviour aiScript;

    void Start()
    {
        if (carIndex == GameManager.selectedCar)
        {
            playerScript.enabled = true;
            aiScript.enabled = false;
            gameObject.tag = "Player";
        }
        else
        {
            playerScript.enabled = false;
            aiScript.enabled = true;
        }
    }
}
