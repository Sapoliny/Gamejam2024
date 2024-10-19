using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] bulletPrefabs;

    private Transform playerTransform;

    public float bulletSpeed;

    [Tooltip("Fire in seconds")]
    public float fireRate;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            Shoot();
            canShoot = false;
            StartCoroutine(shootTiming(fireRate));
        }
    }

    void Shoot()
    {
        Vector3 direction = playerTransform.position - this.transform.position;
        float angle = Vector2.Angle(Vector2.left, direction);
        if (direction.y > 0)
        {
            angle *= -1;
        }
        //Debug.Log(angle);

        GameObject bullet = Instantiate(getRandomPrefab(), this.transform.position, Quaternion.Euler(0, 0, angle));


        direction.Normalize();

        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.velocity = direction * bulletSpeed;
    }

    IEnumerator shootTiming(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    GameObject getRandomPrefab() 
    { 
        int maxIndex = bulletPrefabs.Length;
        int index = Random.Range(0, maxIndex);
        return bulletPrefabs[index];
    }
}
