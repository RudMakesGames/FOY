using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGround : MonoBehaviour
{
    public GameObject Chandelier;
    public float Force = 3f;
    public Transform firePoint;
    private void Start()
    {
        ShootProjectile();
    }
    public void ShootProjectile()
    {
        // Instantiate a new projectile at the firePoint position and rotation
        GameObject projectile = Instantiate(Chandelier, firePoint.position, firePoint.rotation);

        // Get the rigidbody of the projectile and apply force forward
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * Force, ForceMode2D.Impulse);

        }
        else
        {
            Debug.LogError("Projectile is missing Rigidbody2D component");
        }

    }
}
