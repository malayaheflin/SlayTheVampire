using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public float force = 100;

    public void Shoot(InputAction.CallbackContext context)
    {
      if(context.started)
      {
        GameObject bulletObject = Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.transform.forward * force, ForceMode.Impulse);
      }
    }
}
