using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Twr_0Base;

public class MultiShotTower : Twr_0Base
{
    List<MultiShotArea> multiObjects = new List<MultiShotArea>();
    MultiShotArea[] multiObjArray;

    ParticleSystem fireParticleSystem;
    public GameObject fireParticle;
    public override void TowerInfo()
    {
        towerAttackType = TowerAttackType.Area;
        towerType = TowerType.Archer;

        towerName = "¿¬»ç±Ã¼ö";
        towerPrice = 100;
        towerAttackDamage = new float[6] { 25f, 60f, 140f, 320f, 800f, 1600f };
        towerAttackSpeed = new float[6] { 5f, 5f, 5f, 5f, 5f, 2f };
        towerAttackRange = new float[6] { 7f, 7f, 7f, 7f, 7f, 7f };

        towerUpgradeLevel = 1;
        towerUpgradeTier = 1;

        towerMaxTarget = new int[6] { 1, 1, 1, 1, 1, 1 };

        areaDuration = 2f;
        areaAttDelay = 0.251f;

        towerRank = 0;

        fireParticleSystem = fireParticle.GetComponent<ParticleSystem>();
    }
    protected override void AfterAwake()
    {
        multiObjArray = GetComponentsInChildren<MultiShotArea>(true);
        multiObjects.AddRange(multiObjArray);
    }

    protected override void Detecting()
    {
        StopCoroutine(ActingAnimator());
        Collider[] colliders = Physics.OverlapSphere(towerPos, towerAttackRange[towerRank], enemyLayer);
        enemyTargets.Clear();

        if (colliders.Length != 0)
        {
            if (fireSound != null)
            { Sound_Manager.instance.EffectPlay(fireSound); }

            foreach (Collider collider in colliders)
            {
                if (targetsCount >= towerMaxTarget[towerRank]) { break; }

                MonsterMove _enemy = collider.GetComponent<MonsterMove>();

                switch (towerAttackType)
                {
                    case TowerAttackType.Area:

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(MultiAreaEnemy(multiObjArray[targetsCount], collider));
                            targetsCount++;
                        }
                        break;
                }
            }
            targetsCount = 0;
        }
    }

    IEnumerator MultiAreaEnemy(MultiShotArea multiObject, Collider collider)
    {
        isCoolTime = true;
        CoolTimeTrueDetectOff();

        
        gameObject.transform.LookAt(collider.transform.position);
        StartCoroutine(ActingAnimator());
        DamageSetting();
        multiObject.GetComponent<MultiShotArea>().GetAreaObjectInfo(damage, areaDuration, areaAttDelay, collider.gameObject.layer);
        yield return new WaitForSeconds(towerAttackSpeed[towerRank]);
        isCoolTime = false;
        CoolTimeFalseDetectOn();
    }

    IEnumerator ActingAnimator()
    {
        for (int i = 0; i < 3; i++)
        {
            animator.SetTrigger("IdleToAttack");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
