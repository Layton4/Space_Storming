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
    public GameObject DialoguePanel;
    public GameObject TabletPanel;

    public Transform spawnPoint;
    private GameObject player;

    private AudioSource musicAudioSource;
    public AudioClip gameOverMusic;

    public GameObject[] doors;
    private List<int> doorsPersistance = new List<int>();


    private void Awake()
    {
        DialoguePanel.SetActive(true);
        TabletPanel.SetActive(true);

        musicAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        player.SetActive(true);
        player.transform.position = spawnPoint.position;
    }
    private void Start()
    {
        DataPersistance.hasPlayed = 1;
        DataPersistance.SaveForFutureGames();

        LoadOpenDoors();

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

    public void ActivateGameOver()
    {
        musicAudioSource.Stop();
        musicAudioSource.PlayOneShot(gameOverMusic);

        gameOverPanel.SetActive(true);
    }

    public void SafeLastPosition()
    {
        DataPersistance.playerXPos = player.transform.position.x;
        DataPersistance.playerYPos = player.transform.position.y;
    }

    public void SafeOpenDoors()
    {
        DataPersistance.door1 = doors[0].GetComponent<Animator>().GetBool("isOpen") ? 1 : 0;
        DataPersistance.door2 = doors[1].GetComponent<Animator>().GetBool("isOpen") ? 1 : 0;
        DataPersistance.door3 = doors[2].GetComponent<Animator>().GetBool("isOpen") ? 1 : 0;
        DataPersistance.door4 = doors[3].GetComponent<Animator>().GetBool("isOpen") ? 1 : 0;
        DataPersistance.door5 = doors[4].GetComponent<Animator>().GetBool("isOpen") ? 1 : 0;
        DataPersistance.door6 = doors[5].GetComponent<Animator>().GetBool("isOpen") ? 1 : 0;
    }

    public void LoadOpenDoors()
    {
        doorsPersistance.Add(DataPersistance.door1);
        doorsPersistance.Add(DataPersistance.door2);
        doorsPersistance.Add(DataPersistance.door3);
        doorsPersistance.Add(DataPersistance.door4);
        doorsPersistance.Add(DataPersistance.door5);
        doorsPersistance.Add(DataPersistance.door6);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetBool("isOpen",(doorsPersistance[i] == 1));
        }
    }

}
