using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField]
    private int checkpoint;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Meta")
        {
            try
            {
                CarInfo car = other.gameObject.GetComponent<CarInfo>();
                if (car.checkpoint == levelManager.checkpoints.Length - 1)
                {
                    car.lap += 1;
                    car.checkpoint = 0;
                }
            }
            catch { }                      
        }
        else
        {
            Debug.Log("Paso checkpoint");
            try
            {
                Debug.Log("Intento try");
                CarInfo car = other.gameObject.GetComponent<CarInfo>();
                if (car.checkpoint == checkpoint -1)
                {
                    car.checkpoint += 1;
                }
            }
            catch { Debug.Log("Hago catch"); }           
        }
    }
}
