using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObject : MonoBehaviour
{
    Vector3 dir;
    float damage;
    float duration;
    float damageDelay;
    LayerMask enemyLayer;
    private void OnEnable()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.transform.position = dir;
        StartCoroutine(AttackArea());
        StartCoroutine(DurationArea());
    }
    public void GetAreaObjectInfo(Vector3 _dir, float _damage, float _duration, float _damageDelay,LayerMask _enemyLayer)
    {
        dir = _dir;
        damage = _damage;
        duration = _duration;
        damageDelay = _damageDelay;
        enemyLayer = _enemyLayer;
        gameObject.SetActive(true);
    }
    IEnumerator DurationArea()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    IEnumerator AttackArea()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        
        yield return new WaitForSeconds(damageDelay);
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(AttackArea());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == enemyLayer)
        {
            print(other.gameObject.name);
            other.GetComponent<MonsterMove>().DamagedAction(damage);
        }

        else
        {

        }
    }


}
