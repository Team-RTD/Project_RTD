using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class AreaObject : MonoBehaviour
{
    Vector3 dir;
    Vector3 towerDir;
    float damage;
    float duration;
    float damageDelay;
    LayerMask enemyLayer;

    private bool isActive = false;
    private void OnEnable()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.transform.position = dir;
        gameObject.transform.LookAt(towerDir);
        StartCoroutine(AttackArea());
        StartCoroutine(DurationArea());
    }
    public void GetAreaObjectInfo(Vector3 _dir, Vector3 _towerDir, float _damage, float _duration, float _damageDelay,LayerMask _enemyLayer)
    {
        dir = _dir;
        towerDir = _towerDir;
        damage = _damage;
        duration = _duration;
        damageDelay = _damageDelay;
        enemyLayer = _enemyLayer;
        isActive = true;
        gameObject.SetActive(true);
        print(dir + " Area Dir");
    }
    IEnumerator DurationArea()
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
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
        if (other.gameObject.layer == enemyLayer && isActive == true && other.GetComponent<MonsterMove>() != null)
        {
            other.GetComponent<MonsterMove>().DamagedAction(damage);
        }

        else
        {

        }
    }


}
