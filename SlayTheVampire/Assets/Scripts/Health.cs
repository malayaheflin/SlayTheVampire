using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthPoints = 3;
    // Start is called before the first frame update
    public void takeHits(float damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0){
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }
}
