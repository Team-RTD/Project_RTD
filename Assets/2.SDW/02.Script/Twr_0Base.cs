using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Twr_0Base : MonoBehaviour
{
    // Declare -----------------------------------------------

    // Must Input --
    protected TowerAttackType towerAttackType;

    protected string towerName;
    protected int towerPrice;
    protected float towerAttackDamage;
    protected float towerAttackSpeed;
    protected float towerAttackRange;

    protected int towerUpgradeLevel;
    protected int towerUpgradeTier;

    protected int towerMaxTarget;
    // Must Input --

    public int towerRank;
    GameObject tower;

    // Throw Input --
    // * if you create Throw Tower [(GameObject : Throw Object) Count == towerMaxTarget]
    protected float throwObjSpeed;
    // Throw Input --



    // Area Input --
    // * if you create Area Tower [(GameObject : Area Object) Count == towerMaxTarget]
    protected float areaDuration;
    protected float areaAttDelay;
    // Area Input --



    // Optional
    public float upgradePercent = 0;


    // Declare -----------------------------------------------






    // DO NOT EDIT THIS LIST *************************************************
    private Vector3 towerPos;
    private bool isCoolTime = false;
    private int targetsCount = 0;

    List<MonsterMove> enemyTargets = new List<MonsterMove>();
    private LayerMask enemyLayer;

    List<ThrowObject> throwObjects = new List<ThrowObject>();
    ThrowObject[] thorwObjArray;

    List<AreaObject> areaObjects = new List<AreaObject>();
    AreaObject[] areaObjArray;
    // DO NOT EDIT THIS LIST *************************************************






    public virtual void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;

        towerName = "Null";
        towerPrice = 0;
        towerAttackDamage = 0f;
        towerAttackSpeed = 0f;
        towerAttackRange = 0f;

        towerUpgradeLevel = 0;
        towerUpgradeTier = 0;

        towerMaxTarget = 0;
    }

    public enum TowerAttackType
    {
        Shooter,
        Thrower,
        Area
    }
    private void Awake()
    {
        TowerInfo();
        enemyLayer = 1 << 6; // Enemy Layer
        towerPos = gameObject.transform.position;
                                              
        if (towerAttackType == TowerAttackType.Thrower)
        {
            thorwObjArray = GetComponentsInChildren<ThrowObject>(true);
            throwObjects.AddRange(thorwObjArray);
        }

        if (towerAttackType == TowerAttackType.Area)
        {
            areaObjArray = GetComponentsInChildren<AreaObject>(true);
            areaObjects.AddRange(areaObjArray);
        }
    }

    private void Update()
    {
        if (isCoolTime == false)
        {
            Detecting();
        }
    }

    
    public void Detecting()
    {
        Collider[] colliders = Physics.OverlapSphere(towerPos, towerAttackRange, enemyLayer);
        enemyTargets.Clear();

        if (colliders.Length != 0)
        {

            foreach (Collider collider in colliders)
            {
                if (targetsCount >= towerMaxTarget) { break; }
                
                MonsterMove _enemy = collider.GetComponent<MonsterMove>();

                switch (towerAttackType)
                {
                    case TowerAttackType.Shooter:

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(AttackEnemy(_enemy));
                        }
                        break;

                    case TowerAttackType.Thrower:

                        if (towerMaxTarget != throwObjects.Count)
                        {
                            Debug.LogError("Tower Max Target != ThrowObject Count");
                        }

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(ThrowerEnemy(thorwObjArray[targetsCount], collider));
                            targetsCount++;
                        }
                        
                        break;

                    case TowerAttackType.Area:

                        if (towerMaxTarget != areaObjects.Count)
                        {
                            Debug.LogError("Tower Max Target != AreaObject Count");
                        }

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(AreaEnemy(areaObjArray[targetsCount], collider));
                            targetsCount++;
                        }
                            break;
                }
            }
            targetsCount = 0;
        }
    }
    IEnumerator AttackEnemy(MonsterMove _enemy)
    {
        isCoolTime = true;
        targetsCount = targetsCount + 1;
        _enemy.GetComponent<MonsterMove>().DamagedAction(damage);
        yield return new WaitForSeconds(towerAttackSpeed);
        targetsCount = 0;
        isCoolTime = false;
    }

    IEnumerator ThrowerEnemy(ThrowObject throwObject, Collider collider)
    {
        isCoolTime = true;
        throwObject.transform.position = gameObject.transform.position;
        throwObject.GetComponent<ThrowObject>().GetThrowObjectInfo(collider.transform.position, towerAttackDamage, throwObjSpeed);
        yield return new WaitForSeconds(towerAttackSpeed);
        isCoolTime = false;
    }

    IEnumerator AreaEnemy(AreaObject areaObject, Collider collider)
    {
        isCoolTime = true;
        areaObject.GetComponent<AreaObject>().GetAreaObjectInfo(collider.transform.position, towerAttackDamage, areaDuration, areaAttDelay, collider.gameObject.layer);
        yield return new WaitForSeconds(towerAttackSpeed);
        isCoolTime = false;
    }

    public float damage;

    public void TowerUpgradeLevel()
    {
        damage =towerAttackDamage + (towerAttackDamage * upgradePercent * 0.1f);

        //UpGrade_Manager.instance.UpGradeTower(this.GetComponent<Twr_TestTower>());
    }

    public void TowerTierLevel()
    {

    }
}