using UnityEngine;

// Tracks an enemy's health and whether it's dead.
public class PlayerHealth : MonoBehaviour
{
    // How much damage the enemy can take before it dies.
    [SerializeField] float hitPoints = 100f;

    // True once the enemy has died. Other scripts check it through IsDead().

    // The enemy's collider, which gets turned off when the enemy dies.


    // Runs once when the object loads.

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= damage)
        {
            print("player died");
        }
    }

    // Students add TakeDamage() in Week 15.
    // Students add Die() in Week 16.
}
