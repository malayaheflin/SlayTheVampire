using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;
    public Camera cam; // look into getting vars automatically instead of public

    bool attacking = false;
    bool readyToAttack = true;

    public Animator animator;
    private const string IDLESTATE = "Idle";
    private const string PUNCHSTATE = "Attack";
    string currentAnimationState;
    
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
        ChangeAnimationState(PUNCHSTATE);
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
        ChangeAnimationState(IDLESTATE);
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            Debug.Log("attacked");
            // if (hit.transform.TryGetComponent<Enemy>(out Enemy E))
            // {
            //     E.TakeDamage(attackDamage);
            // }
        }
    }

    // animation
    public void ChangeAnimationState(string newState)
    {
        if (newState == currentAnimationState)
            return;
        
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

}
