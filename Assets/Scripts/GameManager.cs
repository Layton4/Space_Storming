using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> bullets = new List<GameObject>();
    public Transform shotPoint;

    private int numOfBullets = 5;
    private int currentBullet;

    public Transform respawnPoint;

    public GameObject gameOverPanel;

    public Transform spawnPoint;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
        player.SetActive(true);
        player.transform.position = spawnPoint.position;

    }
    private void Start()
    {
        DataPersistance.hasPlayed = 1;
        DataPersistance.SaveForFutureGames();

        for (int i = 0; i < numOfBullets; i++)
        {

            bullets.Add(Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation));
        }

        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }

    }

    private void Update()
    {

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

    public void ActivateGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void SafeLastPosition()
    {
        DataPersistance.playerXPos = player.transform.position.x;
        DataPersistance.playerYPos = player.transform.position.y;
    }

}
