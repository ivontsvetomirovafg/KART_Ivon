
using System.Collections.Generic;
using UnityEngine;

public class PathCars : MonoBehaviour
{
    public List<Transform> nodes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        nodes = new List<Transform>();
        Transform[] tempNodes = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tempNodes.Length; i++)
        {
            if (i != 0)
            {
                nodes.Add(tempNodes[i]);
                Gizmos.DrawWireSphere(tempNodes[i].position, 1);
            }

        }
        for (int i = 0; i < nodes.Count; i++)
        {
            if (i + 1 >= nodes.Count)
            {
               Gizmos.DrawLine(nodes[i].position, nodes[0].position); 
            }
            else
            {
                Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
            }
            

        }

    }
}
