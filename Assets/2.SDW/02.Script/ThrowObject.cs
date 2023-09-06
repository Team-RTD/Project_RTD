using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    Twr_0Base twrBase;


    Vector3 dir;
    public float damage;
    public float objSpeed;
    public bool isFollowing = false;

    private void OnEnable()
    {
        print("Enable ThrowObject");
        
    }
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (dir * objSpeed * Time.deltaTime);
    }

    public void GetThrowObjectInfo(Vector3 _dir, float _damage, float _objSpeed)
    {
        dir = _dir;
        damage = _damage;
        objSpeed = _objSpeed;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponent<MonsterMove>().DamagedAction(damage);
            gameObject.SetActive(false);
        }

        else
        {
            StartCoroutine(ObjectDisable());
        }
    }

    IEnumerator ObjectDisable()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
