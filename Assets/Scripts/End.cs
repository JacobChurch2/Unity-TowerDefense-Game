using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField]
    public float health;

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PlayerTakeDamage(other.gameObject.GetComponent<Enemy>().damage);
            ObjectPooler.Instance.Despawn(other.gameObject.GetComponent<Enemy>().type,other.gameObject);
        }
    }

    private void PlayerTakeDamage(int damage)
    {
        health -= damage;
    }
}
