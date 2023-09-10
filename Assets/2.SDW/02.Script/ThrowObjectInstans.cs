using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectInstans : MonoBehaviour
{
    Vector3 dir;
    float objSpeed;
    float damage;

    public float destroyTime = 15f;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
        gameObject.transform.LookAt(dir);
    }

    public void GetThrowObjectInfo(Vector3 _dir, float _objSpeed,float _damage)
    {
        dir = _dir;
        objSpeed = _objSpeed;
        damage = _damage;
        gameObject.transform.LookAt(_dir);
    }
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (dir * objSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<MonsterMove>().DamagedAction(damage);
            Destroy(gameObject);
        }

        else
        {
            
        }
    }
}
