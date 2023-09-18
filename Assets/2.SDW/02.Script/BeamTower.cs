using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Twr_0Base;

public class BeamTower : Twr_0Base
{
    public ParticleSystem beamEffect;
    public GameObject lazerParticle;

    private Coroutine _stopParticle;
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.InstanceThorwer;
        towerType = TowerType.Mage;

        towerName = "화염법사";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 2.5f, 5.5f, 12.5f, 30f, 80f, 150f };
        towerAttackSpeed = new float[6] { 0.1f, 0.1f, 0.1f, 0.09f, 0.08f, 0.07f };
        towerAttackRange = new float[6] { 15f, 15f, 15f, 15f, 15f, 50f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = new int[6] { 1, 1, 1, 1, 1, 1 };

        throwObjSpeed = 90f;

        towerRank = 0;
    }

    public override void Start()
    {
        lazerParticle = GetComponentInChildren<LazerBeam>().gameObject;
        beamEffect = lazerParticle.GetComponent<ParticleSystem>();
    }

    public bool stopWaiting = false;
    public override void CoolTimeFalseDetectOn() //공격쿨타임을 모두 기다린 뒤
    {
        base.CoolTimeFalseDetectOn();

        if (stopWaiting == true)
        {
            StopCoroutine(_stopParticle);
            _stopParticle = null;
        }
        _stopParticle = StartCoroutine(StopParticle());    
    }

    public override void CoolTimeTrueDetectOff() //공격시
    {
        base.CoolTimeTrueDetectOff();

        if (stopWaiting == true)
        {
            StopCoroutine(_stopParticle);
            _stopParticle = null;
            stopWaiting = false;
        }
        beamEffect.Play();
    }

    IEnumerator StopParticle()
    {
        stopWaiting = true;

        yield return 0.5f;

        beamEffect.Stop();
        stopWaiting = false;
    }
}
