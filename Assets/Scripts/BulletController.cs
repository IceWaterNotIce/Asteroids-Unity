using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeTime; // seconds
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        // if bullet touches a stone, destroy both
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            if (Vector3.Distance(transform.position, asteroid.transform.position) < 0.5)
            {
                Destroy(asteroid);
                Destroy(gameObject);
                levelManager.AddScore(1);
            }
        }
        

    }

    public void IncreaseSpeed(float increment)
    {
        speed += increment; // Increase speed safely

    }
}
