using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Twr_0Base : MonoBehaviour
{
    //Declaration
    protected string towerNumber;
    protected int towerPrice;
    protected float towerAttackDamage;
    protected float towerAttackSpeed;
    protected float towerAttackRange;

<<<<<<< Updated upstream
    protected int towerUpgradeLevel;
    protected int towerUpgradeTier;
=======
    // Must Input --
    public Sprite portrait;
    public GameObject TowerZone;

    public TowerAttackType towerAttackType;
    public TowerBrood towerBrood;
    public string towerName;
    public int towerPrice;
    public float towerAttackDamage;
    public float towerAttackSpeed;
    public float towerAttackRange;

    public int towerUpgradeLevel;
    public int towerUpgradeTier;
>>>>>>> Stashed changes

    protected TowerAttackType towerAttackType;
    public int towerMaxTarget;
    protected int towerBounceCount;
    protected float throwObjSpeed;

    public Vector3 towerPos;
    public Vector3 enemyPos;
    public LayerMask enemyLayer;
    protected bool isAttack = false;
    protected bool throwReady = true;

    public string[] enemyName; // TEST

    List<ThrowObject> throwObjects = new List<ThrowObject>();
    List<AreaObject> areaObjects = new List<AreaObject>();

    ThrowObject[] thorwObjArray;
    AreaObject[] areaObjArray;
<<<<<<< Updated upstream
=======

    public GameObject thorwObjInstance; // 0910
    // DO NOT EDIT THIS LIST *************************************************



>>>>>>> Stashed changes

    public GameObject area;

    MonsterMove _enemy;

    public virtual void TowerInfo()
    {
        towerNumber = "Null";
        towerPrice = 0;
        towerAttackDamage = 0f;
        towerAttackSpeed = 0f;
        towerAttackRange = 0f;

        towerUpgradeLevel = 0;
        towerUpgradeTier = 0;
    }

    public enum TowerAttackType
    {
        Shooter,
        Thrower,
        Area,
        InstanceThorwer
    }

    public enum TowerBrood
    {
        Warrior,
        Bowman,
        Wizard
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

    private void Start()
    {
        Debug.Log(towerNumber); // TEST
    }

    private void Update()
    {
        if (isCoolTime == false)
        {
            Detecting();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(enemyTargets.Count);  //TEST
        }
    }

    List<MonsterMove> enemyTargets = new List<MonsterMove>();
    public bool isCoolTime = false;
    int targetsCount = 0;
    public void Detecting()
    {
        Collider[] colliders = Physics.OverlapSphere(towerPos, towerAttackRange, enemyLayer);
        //enemyTargets.AddRange(colliders);
        enemyTargets.Clear();

        if (colliders.Length != 0)
        {
            Debug.Log("Detected"); // TEST

            foreach (Collider collider in colliders)
            {
                if (targetsCount >= towerMaxTarget)
                {
                    //print("Break");
                    break;
                }

                MonsterMove _enemy = collider.GetComponent<MonsterMove>();

                switch (towerAttackType)
                {
                    case TowerAttackType.Shooter:
                        //targetsCount = 0;

                        //Enemy _enemy = collider.GetComponent<Enemy>();

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(AttackEnemy(_enemy));
                            
                        }
                        break;

                    case TowerAttackType.Thrower:

                        //targetsCount = 0;
                        if (towerMaxTarget != throwObjects.Count)
                        {
                            Debug.LogError("Tower Max Target != ThrowObject Count");
                        }

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            //print(enemyTargets.Count);
                           // print("towerMaxTarget: "+towerMaxTarget);
                            StartCoroutine(ThrowerEnemy(thorwObjArray[targetsCount], collider));
                             // foreach (ThrowObject throwObject in thorwObjArray)
                             // {
                             //   StartCoroutine(ThrowerEnemy(_enemy, throwObject, collider));
                             //}

                            targetsCount++;
                        }

                        
                        break;

                    case TowerAttackType.Area:

                        //if (towerMaxTarget != areaObjects.Count)
                        //{
                        //    Debug.LogError("Tower Max Target != AreaObject Count");
                        //}

<<<<<<< Updated upstream
                        //if (_enemy != null)
                        //{
                        //    enemyTargets.Add(_enemy);

                        //    //print(enemyTargets.Count);
                        //    // print("towerMaxTarget: "+towerMaxTarget);
                        //    StartCoroutine(AreaEnemy(areaObjArray[targetsCount], collider));
                        //    // foreach (ThrowObject throwObject in thorwObjArray)
                        //    // {
                        //    //   StartCoroutine(ThrowerEnemy(_enemy, throwObject, collider));
                        //    //}

                        //    targetsCount++;
                        //}
                        //int targetCount = 0;

                        //    GameObject _area = area;
                        //    if (targetCount == 1)
                        //    {
                        //        break;
                        //    }
                        //    _enemy = collider.GetComponent<Enemy>();

                        //    if (_enemy != null)
                        //    {                                
                        //        DamageArea damageArea = _area.GetComponent<DamageArea>();
                        //        if (damageArea != null)
                        //        {
                        //            area.SetActive(true);
                        //            StartCoroutine(AttackArea(damageArea, collider));
                        //            area.transform.position = _enemy.transform.position;
                        //        }
                        //    }
                        //    print(colliders);

=======
                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(AreaEnemy(areaObjArray[targetsCount], collider));
                            targetsCount++;
                        }
                            break;

                    case TowerAttackType.InstanceThorwer:

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            gameObject.transform.LookAt(collider.transform.position);
                            StartCoroutine(InstanceThrowObjectDelay(collider));
                            targetsCount++;
                        }
>>>>>>> Stashed changes
                        break;
                }
               
            }

            targetsCount = 0;
        }
    }
    IEnumerator AttackEnemy(MonsterMove _enemy)
    {
        isCoolTime = true;
        targetsCount = targetsCount + 1; //TEST
        _enemy.GetComponent<MonsterMove>().DamagedAction(towerAttackDamage);
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
    float areaDuration;
    float areaAttDelay;
    //IEnumerator AreaEnemy(AreaObject areaObject, Collider collider)
    //{
    //    isCoolTime = true;
    //    areaObject.transform.position = gameObject.transform.position;
    //    areaObject.GetComponent<AreaObject>().GetAreaObjectInfo(collider.transform.position, towerAttackDamage, areaDuration, areaAttDelay);
    //    yield return new WaitForSeconds(towerAttackSpeed);
    //    isCoolTime = false;
    //}
    //IEnumerator AttackArea(DamageArea area, Collider collider)
    //{
    //    isCoolTime = true;
    //    area.GetComponent<DamageArea>().OnTriggerEnter(collider);
    //    yield return new WaitForSeconds(towerAttackSpeed);
    //    isCoolTime = false;
    //}

    IEnumerator InstanceThrowObjectDelay(Collider collider)
    {
        isCoolTime = true;
        animator.SetTrigger("IdleToAttack");
        GameObject _throwObjInstance = Instantiate(thorwObjInstance);
        _throwObjInstance.GetComponent<ThrowObjectInstans>().GetThrowObjectInfo(collider.transform.position, throwObjSpeed, towerAttackDamage);
        _throwObjInstance.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(towerAttackSpeed);
        isCoolTime = false;
    }

    public void TowerUpgradeLevel()
    {
        int x = 10; // Upgrade Percent
        towerAttackDamage = towerAttackDamage + ((towerAttackDamage / 100) * x);
    }

    public void TowerTierLevel()
    {

    }
    //IEnumerator ThrowEnemy(Collider collider)
    //{
    //    if (throwReady  == true)
    //    {
    //        throwObject.transform.position = gameObject.transform.position;
    //        throwReady = false;
    //        GetComponent<ThrowObject>().ObjectFire(collider, throwSpeed);
    //    }
    //    yield return new WaitForSeconds(towerAttackSpeed);
    //}

    //public void TargetObjectHit(Collider collider)
    //{
    //    throwObject.SetActive(true);
    //    throwReady = true;
    //}
}