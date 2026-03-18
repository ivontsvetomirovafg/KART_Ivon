using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform agujaSpeedMeter;
    [SerializeField]
    private CarController player;
    [SerializeField]
    private TextMeshProUGUI speedmeterText;
    [SerializeField]
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private TextMeshProUGUI positionPlayertext;
    [SerializeField]
    private TextMeshProUGUI lapPlayerText;
    [SerializeField]
    private TextMeshProUGUI lapTimeText;
    [SerializeField]
    private GameObject time;
    [SerializeField]
    private List<CarInfo> cars;
    public Transform [] checkpoints;
    [SerializeField]
    private int lapPlayer;
    [SerializeField]
    public int totalLaps;

    private void Awake()
    {
        cars[GameManager.instance.coche].tag = "Player";
    }

    void Start()
    {
        StartCoroutine(CountDown());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();

    }
    IEnumerator CountDown()
    {
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1);
        //Aqu� que se puedan mover los coches
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].tag == "Player")
            {
                cars[i].GetComponent<CarController>().enabled = true;
            }
            else
            {
                cars[i].GetComponent<CarIA>().enabled = true;
            }
        } 
    }      

    void Update()
    {
        //36=0km/h y 165=220km/h
        float t = (player.rb.linearVelocity.magnitude * 3.6f) / player.maxSpeed;
        float z = Mathf.Lerp(36.0f, -195.0f, t);
        agujaSpeedMeter.localEulerAngles = new Vector3(0, 0, z);
        speedmeterText.text = (player.rb.linearVelocity.magnitude * 3.6f).ToString("000");

        /*Quaternion rotA= Quaternion.Euler(0, 0, 36);
        Quaternion rotB = Quaternion.Euler(0, 0, 160);
        Quaternion.Slerp(rotA, rotB, t);*/

        SortCars();
        int indicePlayer = 0;
        for (int i = 0; i < cars.Count; i++)
        {
            if(cars[i].tag == "Player")
            {
                indicePlayer = i; 
                break;
            }
        }
        positionPlayertext.text = (indicePlayer + 1).ToString() + "º";
    }
    void SortCars()
    {
        cars.Sort((a, b) => b.FactorOrden().CompareTo(a.FactorOrden()));
        
        /*for (int i = 0; i < cars.Count; i++)
        {
            for(int j = 0; j < cars[i].Count-1; j++)
            {
                if (cars[j].lap < cars[j+1].lap)
                {
                    CarInfo tempCar = cars[j];
                    cars[j] = cars[i + 1];
                    cars[i + 1] = tempCar;
                }
            }
        }*/
    }
    public void ShowLapTime(TimeSpan _tiempoVuelta)
    {
        lapTimeText.text = String.Format("{0}:{1}:{2}", _tiempoVuelta.Minutes.ToString("00"), _tiempoVuelta.Seconds.ToString("00"), 
            _tiempoVuelta.Milliseconds.ToString("00"));
    }
    //tiempo: time.SetActive = true;
}

