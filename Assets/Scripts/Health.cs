using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;

    private float _currentHealth = 100;

    private UnityAction damageTakeAction;
    public UnityEvent OnDamageTaken;

    private void Start()
    {
        damageTakeAction += CheckStatus;
        OnDamageTaken.AddListener(damageTakeAction);
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        OnDamageTaken.Invoke();
    }

    private void CheckStatus()
    {
        if (_currentHealth<=0)
        {
            gameObject.GetComponent<Actor>().Die();
        }
    }


}
