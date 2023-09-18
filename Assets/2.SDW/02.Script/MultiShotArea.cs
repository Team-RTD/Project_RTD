using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class MultiShotArea : MonoBehaviour
{
    public GameObject fireParticle;
    ParticleSystem fireParticleSystem;

    float damage;
    float duration;
    float damageDelay;
    LayerMask enemyLayer;
    Collider[] areaObjColliders;
    MultiShotTower multiShotTower;
    private bool isActive = false;
    private void Awake()
    {
        areaObjColliders = GetComponents<Collider>();
        multiShotTower = GetComponentInParent<MultiShotTower>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(AttackArea());
        StartCoroutine(DurationArea());
    }
    public void SetParticleSystem(ParticleSystem _particleSystem)
    {
        fireParticleSystem = _particleSystem;
    }
    public void GetAreaObjectInfo(float _damage, float _duration, float _damageDelay, LayerMask _enemyLayer)
    {
        damage = _damage;
        duration = _duration;
        damageDelay = _damageDelay;
        enemyLayer = _enemyLayer;
        isActive = true;
        fireParticleSystem = fireParticle.GetComponent<ParticleSystem>();
        fireParticleSystem.Play();

        gameObject.SetActive(true);
    }
    IEnumerator DurationArea()
    {
        yield return new WaitForSeconds(duration);
        isActive = false;

        gameObject.SetActive(false);
    }

    IEnumerator AttackArea()
    {

        for (int i = 0; i < areaObjColliders.Length; i++)
        {
            areaObjColliders[i].enabled = true;
        }

        yield return new WaitForSeconds(damageDelay);

        for (int i = 0; i < areaObjColliders.Length; i++)
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
