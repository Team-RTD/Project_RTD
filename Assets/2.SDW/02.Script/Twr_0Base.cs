using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Twr_0Base : MonoBehaviour
{
    // Declare -----------------------------------------------

    // Must Input --
    public Sprite portrait;
    public GameObject TowerZone;
    public AudioClip fireSound;

    public TowerAttackType towerAttackType;
    public TowerType towerType;
    public string towerName;
    public int towerPrice;

    public float[] towerAttackDamage = new float[6];
    public float[] towerAttackSpeed = new float[6];
    public float[] towerAttackRange = new float[6];
    public int[] towerMaxTarget = new int[6];

    public int towerUpgradeLevel;
    public int towerUpgradeTier;
    // Must Input --


    // Throw Input --
    // * if you create Throw Tower [(GameObject : Throw Object) Count == towerMaxTarget]
    public float throwObjSpeed;
    // Throw Input --



    // Area Input --
    // * if you create Area Tower [(GameObject : Area Object) Count == towerMaxTarget]
    protected float areaDuration;
    protected float areaAttDelay;
    // Area Input --



    // Optional
    protected float damage;
    protected float upgradePercent = 10f;
    public int towerRank;



    public GameObject shooterParticle; //0907

    // Declare -----------------------------------------------






    // DO NOT EDIT THIS LIST *************************************************
    protected Vector3 enemyDir;
    protected Animator animator; //0907
    protected Vector3 towerPos;
    public bool isCoolTime = false;
    protected int targetsCount = 0;

    protected List<MonsterMove> enemyTargets = new List<MonsterMove>();
    protected LayerMask enemyLayer;

    protected List<ThrowObject> throwObjects = new List<ThrowObject>();
    protected ThrowObject[] thorwObjArray;

    protected List<AreaObject> areaObjects = new List<AreaObject>();
    protected AreaObject[] areaObjArray;

    public GameObject thorwObjInstance; // 0910
    // DO NOT EDIT THIS LIST *************************************************






    public virtual void TowerInfo()
    {
        towerAttackType = TowerAttackType.Shooter;

        towerName = "Null";
        towerPrice = 0;

        towerUpgradeLevel = 0;
        towerUpgradeTier = 0;

        towerRank = 0;
    }

    public enum TowerAttackType
    {
        Shooter,
        Thrower,
        Area,
        InstanceThorwer
    }

    public enum TowerType
    {
        Warrior,
        Mage,
        Archer
    }
    public virtual void Awake()
    {
        towerAttackDamage = new float[6];
        towerAttackSpeed = new float[6];
        towerAttackRange = new float[6];
        towerMaxTarget = new int[6];

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

        animator = GetComponentInChildren<Animator>(); //0907

        AfterAwake();
    }

    protected virtual void AfterAwake()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (isCoolTime == false)
        {
            Detecting();
        }
    }


    protected virtual void Detecting()
    {
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
                    case TowerAttackType.Shooter:

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            gameObject.transform.LookAt(collider.transform.position); //0907
                            StartCoroutine(AttackEnemy(_enemy));
                        }
                        break;

                    case TowerAttackType.Thrower:

                        if (towerMaxTarget[towerRank] > throwObjects.Count)
                        {
                            Debug.LogError("Tower Max Target > ThrowObject Count");
                        }

                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            gameObject.transform.LookAt(collider.transform.position); //0907
                            StartCoroutine(ThrowerEnemy(thorwObjArray[targetsCount], collider));
                            targetsCount++;
                        }

                        break;

                    case TowerAttackType.Area:

                        if (towerMaxTarget[towerRank] > areaObjects.Count)
                        {
                            Debug.LogError("Tower Max Target > AreaObject Count");
                        }
                        if (_enemy != null)
                        {
                            enemyTargets.Add(_enemy);
                            StartCoroutine(AreaEnemy(areaObjArray[targetsCount], collider)); // 여기서 오류
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
                        break;
                }
            }
            targetsCount = 0;
        }
    }
    //DoNotErase --------------------------------------
    public virtual void CoolTimeFalseDetectOn()
    {

    }

    public virtual void CoolTimeTrueDetectOff()
    {

    }
    //DoNotErase --------------------------------------
    IEnumerator AttackEnemy(MonsterMove _enemy)
    {
        isCoolTime = true;
        CoolTimeTrueDetectOff();
        animator.SetTrigger("IdleToAttack"); //0907
        ShooterHitAction(_enemy);
        targetsCount = targetsCount + 1;
        DamageSetting();
        _enemy.GetComponent<MonsterMove>().DamagedAction(damage);
        yield return new WaitForSeconds(towerAttackSpeed[towerRank]);
        targetsCount = 0;
        isCoolTime = false;
        CoolTimeFalseDetectOn();
    }

    protected virtual void ShooterHitAction(MonsterMove _enemy)
    {
        GameObject particleInstance = Instantiate(shooterParticle, _enemy.transform.position, Quaternion.identity); //0907
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>(); //0907
        float x = particleSystem.main.duration;//0907
        Destroy(particleInstance, x); //0907
    }

    IEnumerator ThrowerEnemy(ThrowObject throwObject, Collider collider)
    {
        isCoolTime = true;
        CoolTimeTrueDetectOff();
        throwObject.transform.position = gameObject.transform.position;
        throwObject.transform.LookAt(collider.transform); //0907
        DamageSetting();
        throwObject.GetComponent<ThrowObject>().GetThrowObjectInfo(DirSett(collider.transform.position), damage, throwObjSpeed);
        yield return new WaitForSeconds(towerAttackSpeed[towerRank]);
        isCoolTime = false;
        CoolTimeFalseDetectOn();
    }

    protected virtual IEnumerator AreaEnemy(AreaObject areaObject, Collider collider)
    {
        isCoolTime = true;
        CoolTimeTrueDetectOff();
        animator.SetTrigger("IdleToAttack"); //0907
        gameObject.transform.LookAt(collider.transform.position);
        DamageSetting();
        Debug.LogWarning(collider.transform.position == null);
        Debug.LogWarning(gameObject.transform.position == null);
        Debug.LogWarning(areaDuration == null);
        Debug.LogWarning(damage == null);
        Debug.LogWarning(areaAttDelay == null);
        Debug.LogWarning(collider.gameObject.layer == null);
        areaObject.GetComponent<AreaObject>().GetAreaObjectInfo(collider.transform.position, gameObject.transform.position, damage, areaDuration, areaAttDelay, collider.gameObject.layer); //여기서 오류
        yield return new WaitForSeconds(towerAttackSpeed[towerRank]);
        isCoolTime = false;
        CoolTimeFalseDetectOn();
    }

    IEnumerator InstanceThrowObjectDelay(Collider collider)
    {
        isCoolTime = true;
        CoolTimeTrueDetectOff();
        animator.SetTrigger("IdleToAttack");
        GameObject _throwObjInstance = Instantiate(thorwObjInstance);
        _throwObjInstance.GetComponent<ThrowObjectInstans>().GetThrowObjectInfo(DirSett(collider.transform.position), throwObjSpeed, towerAttackDamage[towerRank]);
        _throwObjInstance.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(towerAttackSpeed[towerRank]);
        isCoolTime = false;
        CoolTimeFalseDetectOn();
    }

    public Vector3 DirSett(Vector3 collider)
    {
        enemyDir = (collider - transform.position).normalized;
        return enemyDir;
    }


    public void DamageSetting()
    {

        switch (towerType)
        {
            case TowerType.Warrior:
                damage = towerAttackDamage[towerRank] + towerAttackDamage[towerRank] * 0.01f * UpGrade_Manager.instance.warriorUpgrade;
                break;

            case TowerType.Mage:
                damage = towerAttackDamage[towerRank] + towerAttackDamage[towerRank] * 0.01f * UpGrade_Manager.instance.mageUpgrade;
                break;

            case TowerType.Archer:
                damage = towerAttackDamage[towerRank] + towerAttackDamage[towerRank] * 0.01f * UpGrade_Manager.instance.archerUpgrade;
                break;
        }

    }
}
