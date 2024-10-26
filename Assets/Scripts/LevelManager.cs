using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    //prefabs
    public GameObject stonePrefab;
    public Camera cam;
    public int Score;
    public Text txtScore;

    // Start is called before the first frame update
    void Start()
    {
        // spawn a stone every 0.1 seconds
        InvokeRepeating("SpawnStone", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // destroy stone if it goes out the screen
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            // get the screen position of the stone
            Vector3 screenPos = cam.WorldToScreenPoint(asteroid.transform.position);
            // if the stone is out of the screen 100 pixels, destroy it
            int distance = 500;
            if (screenPos.x < -distance || screenPos.x > Screen.width + distance || screenPos.y < -distance || screenPos.y > Screen.height + distance)
            {
                Destroy(asteroid);
            }

        }
    }

    public void SpawnStone()
    {
        //spawn a stone at a random position outside the screen 50 pixels
        Vector3 screenPos = new Vector3();
        int side = UnityEngine.Random.Range(0, 4);
        switch (side)
        {
            case 0:
                screenPos.x = -50;
                screenPos.y = UnityEngine.Random.Range(0, Screen.height);
                break;
            case 1:
                screenPos.x = Screen.width + 50;
                screenPos.y = UnityEngine.Random.Range(0, Screen.height);
                break;
            case 2:
                screenPos.x = UnityEngine.Random.Range(0, Screen.width);
                screenPos.y = -50;
                break;
            case 3:
                screenPos.x = UnityEngine.Random.Range(0, Screen.width);
                screenPos.y = Screen.height + 50;
                break;
        }
        //Debug.Log(screenPos);
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        // z = 0
        worldPos.z = 0;
        //Debug.Log(worldPos);  
        // Instantiate the stone
        GameObject stone = Instantiate(stonePrefab, worldPos, Quaternion.identity);
        // Generate a random angle for the Z-axis
        float randomZ = UnityEngine.Random.Range(0f, 360f);

        // Create a Quaternion with the random Z rotation
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomZ);

        // Apply the random rotation to the stone
        stone.transform.rotation = randomRotation;
        stone.GetComponent<AsteroidController>().speed = UnityEngine.Random.Range(0, 4);


    }

    public void AddScore(int num)
    {
        Score += num;
        txtScore.text = "Score : " + Score;
    }
}
