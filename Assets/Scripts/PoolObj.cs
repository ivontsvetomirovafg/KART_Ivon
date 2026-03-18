using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolObj : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int poolSize;
    private List<GameObject> bulletPool;

    public static PoolObj instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletTemp = Instantiate(bulletPrefab, Vector2.zero, Quaternion.identity);
            bulletPool.Add(bulletTemp);
            bulletTemp.SetActive(false);
        }

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPoolBullet()
    {
        GameObject bulletToReturn = null;
        for(int i = 0; i <bulletPool.Count; i++)
        {
            if (bulletPool[i].activeInHierarchy == false)
            {
                bulletToReturn = bulletPool[i];
                break;
            }
        }
        if(bulletToReturn != null)
        {
            bulletToReturn.SetActive(true);
            return bulletToReturn;
        }
        else
        {
            GameObject bulletTemp = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bulletPool.Add(bulletTemp);
            return bulletTemp;
        }
    }
    public void ReturnBulletToPool(GameObject _bulletToPool)
    {
        _bulletToPool.SetActive(false);
    }
}
