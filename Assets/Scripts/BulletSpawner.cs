using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject BulletPrefab;
    AudioSource bulletSound;
    public float BulletVelocity = 20f;
    public float BulletLifetime = 3f;

    void Start()
    {
        bulletSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(
                BulletPrefab, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().linearVelocity =
                transform.forward * BulletVelocity;

            bulletSound.Play();
            Destroy(newBullet, BulletLifetime);
        }
    }
}