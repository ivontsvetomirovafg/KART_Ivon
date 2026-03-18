using System;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    public int lap;
    public int checkpoint;
    private LevelManager levelManager;
    private TimeSpan [] tiempoVueltas;
    private TimeSpan tiempoCarrera;
    private TimeSpan [] recordSector;
    private DateTime inicioCarrera;
    


    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        tiempoVueltas = new TimeSpan[levelManager.totalLaps];
    }

    public float FactorOrden()
    {
        float distance = (transform.position - levelManager.checkpoints[checkpoint].position).magnitude;
        return lap * 1000 + checkpoint * 100 + distance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Meta")
        {
            if (lap == 0)
            {
                inicioCarrera = DateTime.Now;
            }
            else
            {
                TimeSpan tiempoTotalDeVueltas = new TimeSpan();
                for(int i = 0; i< tiempoVueltas.Length; i++)
                {
                    tiempoTotalDeVueltas += tiempoVueltas[i];
                }
                TimeSpan tiempoVuelta = DateTime.Now - (inicioCarrera + tiempoTotalDeVueltas);
                for (int i = 0; i< tiempoVueltas.Length; i ++)
                {
                    if (tiempoVueltas[i].TotalSeconds > 0)
                    {

                    }
                    else
                    {
                        tiempoVueltas[i] = tiempoVuelta;
                        break;
                    }
                }
                if(gameObject.tag == "Player")
                {
                    levelManager.ShowLapTime(tiempoVuelta);
                }
            }
        }
    }
}
