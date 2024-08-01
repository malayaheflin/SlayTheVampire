using UnityEngine;

public class Combat : MonoBehaviour
{
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;
    public Camera cam; // get automatically ?
    bool attacking = false;
    bool readyToAttack = true;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            Debug.Log("attacked");
            if (hit.transform.TryGetComponent<Health>(out Health E))
            {
                 E.takeHits(attackDamage);
            }
        }
    }
}
