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

    private float lastAttackTime;

    Enemy _enemy;

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
        lastAttackTime = Time.time; // 초기화
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
    int targetsCount = 0;
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

                    // 일정 간격으로 공격 실행
                    if (Time.time - lastAttackTime >= towerAttackSpeed)
                    {
                        AttackArea();
                        lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
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

    private void AttackArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, towerAttackRange);

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 적이라면 DamagedAction 실행
                enemy.DamagedAction(10); // 데미지 량은 필요에 따라 설정
            }
        }
    }

    public void TowerUpgradeLevel()
    {
        int x = 10; // Upgrade Percent
        towerAttackDamage = towerAttackDamage + ((towerAttackDamage / 100) * x);
    }

    public void TowerTierLevel()
    {

    }
}