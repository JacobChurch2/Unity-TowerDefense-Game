using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGERED");
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Enemy !");
            ObjectPooler.Instance.Despawn(other.gameObject.GetComponent<Enemy>().type,other.gameObject);
        }
    }
}
