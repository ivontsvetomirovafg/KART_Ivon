using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float damping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, damping);
    }
}
