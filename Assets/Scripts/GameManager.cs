using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> bullets = new List<GameObject>();
    public Transform shotPoint;

 
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            bullets.Add(Instantiate(bulletPrefab, shotPoint));
        }

        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }
    }
}
