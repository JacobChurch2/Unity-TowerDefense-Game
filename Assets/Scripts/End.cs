using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PlayerTakeDamage();
            ObjectPooler.Instance.Despawn(other.gameObject.GetComponent<Enemy>().type,other.gameObject);
        }
    }

    private void PlayerTakeDamage()
    {

    }
}
