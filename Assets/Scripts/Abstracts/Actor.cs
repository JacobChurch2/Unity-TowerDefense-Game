using UnityEngine;

[RequireComponent(typeof(Health))]
public class Actor : MonoBehaviour
{
    public virtual void Die()
    {
        if (gameObject.GetComponent<IPooledObject>()!=null)
        {
           gameObject.GetComponent<IPooledObject>().Despawn();
        }
        else
        {
            Destroy(gameObject);
            System.GC.Collect();
        }

    }
}
