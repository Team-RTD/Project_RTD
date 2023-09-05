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

    protected int towerUpgradeLevel;
    protected int towerUpgradeTier;

    protected TowerAttackType towerAttackType;
    public int towerMaxTarget;
    protected int towerBounceCount;
    protected float throwSpeed;

    public Vector3 towerPos;
    public Vector3 enemyPos;
    public LayerMask enemyLayer;
    protected bool isAttack = false;
    protected bool throwReady = true;

    public string[] enemyName; // TEST

    public GameObject area;

    Enemy _enemy;
    GameObject enemyCollider = _enemy.GetComponent<Collider>; 

    List<ThrowObject> throwObjects = new List<ThrowObject>();
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
        Area
    }
    private void Awake()
    {
        TowerInfo();
        enemyLayer = 1 << 6; // Enemy Layer
        towerPos = gameObject.transform.position;

        ThrowObject[] thorwObjArray = GetComponentsInChildren<ThrowObject>();
        throwObjects.AddRange(thorwObjArray.ToList());
        //foreach (ThrowObject throwObject in throwObjects)
        //{
        //    throwObject.DoSomething(); // TEST
        //}

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

    List<Enemy> enemyTargets = new List<Enemy>();
    public bool isCoolTime = false;
    public void Detecting()
    {
        Collider[] colliders = Physics.OverlapSphere(towerPos, towerAttackRange, enemyLayer);
        //enemyTargets.AddRange(colliders);
        enemyTargets.Clear();

        if (colliders.Length != 0)
        {
            Debug.Log("Detected"); // TEST

            switch (towerAttackType)
            {
                case TowerAttackType.Shooter:
                    int targetsCount = 0;
                    //for (int i = 0; i < colliders.Length;i++)
                    foreach (Collider collider in colliders)
                    {
                        if (targetsCount >= towerMaxTarget)
                        {
                            break;
                        }


                        Enemy _enemy = collider.GetComponent<Enemy>();

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(AttackEnemy(_enemy));
                            targetsCount++;
                        }
                    }
                    break;

                case TowerAttackType.Thrower:
                    
                    break;

                case TowerAttackType.Area:
                    int targetCount = 0;
                    foreach (Collider collider in colliders )
                    {
                        GameObject _area = area;
                        if (targetCount == 1)
                        {
                            break;
                        }

                        _enemy = collider.GetComponent<Enemy>();

                        float areaDamage = towerAttackDamage;

                        if (_enemy != null)
                        {
                            area.SetActive(true);

                            area.transform.position = _enemy.transform.position;
                        }
                    }
                    break;
            }

        }
    }
    IEnumerator AttackEnemy(Enemy _enemy)
    {
        isCoolTime = true;
        _enemy.GetComponent<Enemy>().DamagedAction(towerAttackDamage);
        yield return new WaitForSeconds(towerAttackSpeed);
        isCoolTime = false;
    }

    IEnumerator AttackArea(DamageArea area)
    {
        isCoolTime = true;
        area.GetComponent<DamageArea>().OnTriggerEnter(GameObject enemyCollider);
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