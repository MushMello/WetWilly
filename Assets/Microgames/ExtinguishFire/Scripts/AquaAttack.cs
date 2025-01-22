using System.Collections;
using UnityEngine;

public class AquaAttack : MonoBehaviour
{
    public Collider2D aquaCollider;
    public float damage = 3;

    Vector2 rightAttackOffset;
    

    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void AttackRight()
    {
        aquaCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        aquaCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x *-1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        aquaCollider.enabled = false;
    }

    private void OnParticleCollision(GameObject collider) {
        if (collider.CompareTag("Flame"))
        {
            Debug.Log("Touched Flame");
        }
    }
}
