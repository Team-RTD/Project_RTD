using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Twr_0Base;

public class FireTower : Twr_0Base
{
    public GameObject onFireParticle;

    public float fireDamage = 30f;
    public float fireDuration = 2f;
    public float fireDamageDelay = 1f;

    Vector3 upFire;
    

    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;
        towerType = TowerType.Archer;

        towerName = "È­¿°±Ã¼ö";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 5f, 10f, 20f, 40f, 80f, 160f };
        towerAttackSpeed = new float[6] { 2f, 1.8f, 1.6f, 1.4f, 1f, 0.5f };
        towerAttackRange = new float[6] { 8f, 10f, 12f, 16f, 20f, 30f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerRank = 5;
        towerMaxTarget = new int[6] { 1, 2, 2, 3, 3, 5 };

        upFire = new Vector3(0, 3, 0);

    }

    protected override void ShooterHitAction(MonsterMove _enemy)
    {
        GameObject particleInstance = Instantiate(shooterParticle, _enemy.transform.position, Quaternion.identity); //0907
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>(); //0907
        float x = particleSystem.main.duration;//0907
        Destroy(particleInstance, x); //0907

        //GameObject _onFireParticle = Instantiate(onFireParticle, _enemy.transform.position, Quaternion.identity);
        //_onFireParticle.transform.SetParent(_enemy.gameObject.transform);

        if (_enemy.GetComponent<MonsterMove>().isOnFire == false)
        {
            Instantiate(onFireParticle, _enemy.transform.position+ upFire, Quaternion.identity, _enemy.transform);
        }

        _enemy.gameObject.GetComponent<MonsterMove>().FireDamageAction(fireDamage, fireDuration, fireDamageDelay);
    }



}
