using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f;

    public bool isDead = false;
    public void DamagedAction(float _damage)
    {
        if (isDead == true) { return; }

        hp = hp - _damage;

        print(gameObject.name + " has damaged " + _damage + "FinalHP" + hp);

        if (hp <= 0 )
        {
            DeadAction();
        }
    }

    public void DeadAction()
    {
        isDead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log(gameObject.name + "has dead");
    }
}
