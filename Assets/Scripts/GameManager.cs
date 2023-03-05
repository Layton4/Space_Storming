using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> bullets = new List<GameObject>();
    public Transform shotPoint;

    private int numOfBullets = 5;
    private int currentBullet;

    private void Awake()
    {
       
    }
    private void Start()
    {
        for (int i = 0; i < numOfBullets; i++)
        {

            bullets.Add(Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation));
        }

        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }
    }

    public void Prepareshot(Transform p)
    {
        bullets[currentBullet].transform.position = shotPoint.position;
        bullets[currentBullet].SetActive(true);
        bullets[currentBullet].transform.rotation = p.rotation;
        currentBullet++;
        if(currentBullet >= numOfBullets)
        {
            currentBullet = 0;
        }
    }

}
