using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs;  // Array of candy prefabs
    public Transform shootPoint;        // Reference to the point where candies will be shot from
    public float shootForce = 10f;     // The force with which candies are shot
    public AudioSource shootSound;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Assuming Fire1 is your fire button (left mouse click by default)
        {
            ShootCandy();
        }
    }

    void ShootCandy()
    {
        // Randomly select a candy prefab from the array
        int randomIndex = Random.Range(0, candyPrefabs.Length);
        GameObject selectedCandyPrefab = candyPrefabs[randomIndex];

        GameObject candy = Instantiate(selectedCandyPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody candyRigidbody = candy.GetComponent<Rigidbody>();
        candyRigidbody.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        shootSound.PlayOneShot(shootSound.clip);

        // Rotate the candy randomly
        Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        candy.transform.Rotate(randomRotation);

        Destroy(candy, 7f); // Destroy the candy after 7 seconds
    }
}
