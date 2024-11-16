using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float bulletDamage = 1;


    void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Gun"))
        {
            collision.gameObject.GetComponent<Health>()?.takeHits(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
