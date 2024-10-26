using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject bulletPrefab;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        2d movement
        w - accelerate
        a - rotate left
        s - slow down
        d - rotate right
        */
        float accelerate = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        speed += accelerate * Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, speed * Time.deltaTime, 0);
        transform.Rotate(0, 0, -rotation);

        // space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<BulletController>().IncreaseSpeed(speed);
            Debug.Log(bullet);
        }

    }

    void LateUpdate()
    {
        // Reset the cam's rotation to its initial rotation
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
