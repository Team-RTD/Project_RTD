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
    Collider[] areaObjColliders;

    private bool isActive = false;

    private void Start()
    {
        areaObjColliders = GetComponents<Collider>();
        gameObject.SetActive(false);
    }
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
        Debug.LogWarning("11");
        gameObject.SetActive(true);
        Debug.LogWarning("22");
    }
    IEnumerator DurationArea()
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
        gameObject.SetActive(false);
    }

    IEnumerator AttackArea()
    {
        for (int i = 0; i >= areaObjColliders.Length; i++)
        {
            areaObjColliders[i].enabled = true;
        }

        yield return new WaitForSeconds(damageDelay);

        for (int i = 0; i >= areaObjColliders.Length; i++)
        {
            areaObjColliders[i].enabled = false;
        }
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
