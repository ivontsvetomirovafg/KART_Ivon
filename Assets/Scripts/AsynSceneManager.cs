using UnityEngine;
using UnityEngine.SceneManagement;

public class AsynSceneManager : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Cargar nivel de forma aditiva
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //unload escena de forma aditiva
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
