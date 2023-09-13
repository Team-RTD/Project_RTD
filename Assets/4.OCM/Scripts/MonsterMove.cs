using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


//��ǥ1: Ư�� ��ǥ�� ȸ���ϸ鼭 �̵��ϱ�
//�ʿ�Ӽ�1: Ư����ǥ,�̵��ӵ�


//��ǥ2: Enemyü�� ����
//�ʿ�Ӽ�2: EnemyHp


//��ǥ3: �̵�, �׾��� �� �ִϸ��̼� ���� �� �ڿ� ����
//�ʿ�Ӽ�3: �ִϸ�����


//��ǥ4:  ������ �����ϸ� �ı� ����Ʈ ����
//�ʿ�Ӽ�4: ����Ʈ ��ƼŬ �ý���, �ı� ȿ�� ���� ������Ʈ

//��ǥ5: �������� �޾��� �� ü���� ��� 0���ϰ� �Ǹ� �״´�.

public class MonsterMove : MonoBehaviour
{
    //�ʿ�Ӽ�1: Ư����ǥ,�̵��ӵ�
    public float monsterSpeed;
    public Transform[] Pos;
    int posloc;
    public Transform startpos;
    

    //�ʿ�Ӽ�2: EnemyHp
    public float hp ;
    public int monsterStageNum;


    //�ʿ�Ӽ�3: �ִϸ�����
    public bool isDead = false;
    Animator animator;

    public MonsterType monsterType = MonsterType.normal ;

    public enum MonsterType
    {
        normal,
        mission
    }

    public void SetEnum()
    {
        monsterType = MonsterType.mission ;
    }
    public void HpSett()
    {
        if (monsterType == MonsterType.normal)
        {
            if (StageManager.instance.stageNum % 10 == 0)
            {
                hp = StageManager.instance.monsterMaxHp * 10;
            }
            else
            {
                hp = StageManager.instance.monsterMaxHp;
            }
        }
        else 
        {
            if (StageManager.instance.missionNum == 1)
                hp = StageManager.instance.missionMonster1Hp;
            else if (StageManager.instance.missionNum == 2)
                hp = StageManager.instance.missionMonster2Hp;
            else
                hp = StageManager.instance.missionMonster3Hp;
        }
        
    }

    public Sprite portrait;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        monsterSpeed = 15.0f;
        posloc = 0;
        startpos = transform;
        isDead = false;
        monsterStageNum = StageManager.instance.stageNum;
    }
    void Start()
    {
        HpSett();

        
        Transform ArrowPosParent = GameObject.Find("ArrowPos").transform;
        Pos = new Transform[ArrowPosParent.childCount];

        for (int i = 0; i < ArrowPosParent.childCount; i++)
        {
            Pos[i] = ArrowPosParent.GetChild(i);
        }

        //dir = Vector3.forward;
        //angle = 1;
        StartCoroutine(GoToPos(Pos[posloc]));
        startpos = transform;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    

    IEnumerator GoToPos(Transform setpos)
    {
        Vector3 dir = setpos.transform.position - transform.position;
        //Vector3 loc = dir.normalized;
        float speed = 2.0f;
        Vector3 nextPoint = setpos.transform.position - new Vector3(0, 0.4f, 0);
        while (dir.magnitude > 1f)
        {
            if (Data_Manager.instance.isPause)
            {
                speed = 0;
            }
            else
            {
                speed = monsterSpeed;
            }

            transform.LookAt(setpos.transform);
            //transform.position = Vector3.Lerp(transform.position,setpos.transform.position,0.3f);
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed*Time.deltaTime) ;
            //transform.Translate(loc,Space.World);
            dir = nextPoint - transform.position;
            yield return null;
        }
        posloc++;
        if (posloc <= 11)
        { StartCoroutine(GoToPos(Pos[posloc])); }
        else
        {
            yield return new WaitForSeconds(3f);
            posloc = 0;
            transform.position = startpos.position;
            StartCoroutine(GoToPos(Pos[posloc]));
        }

    }



    //��ǥ5: �������� �޾��� �� ü���� ��� 0���ϰ� �Ǹ� �״´�.
    public void DamagedAction(float _damage)
    {
        if (isDead == true) { return; }

        hp = hp - _damage;

        print(gameObject.name + " has damaged " + _damage + "FinalHP" + hp);

        if (hp <= 0)
        {
            if (monsterType == MonsterType.normal)
            {
                if (monsterStageNum % 10 == 0)
                {
                    Data_Manager.instance.money3++;
                    Data_Manager.instance.curHp++;
                }
                else
                {
                    Data_Manager.instance.money1 += 5;
                }
            }
            else
            {
                if (StageManager.instance.missionNum == 1)
                    Data_Manager.instance.money1 += 100;
                else if (StageManager.instance.missionNum == 2)
                    Data_Manager.instance.money1 += 200;
                else
                    Data_Manager.instance.money1 += 300;
            }
            
            StageManager.instance.monsterCount--;
            Data_Manager.instance.killcount++;
            Ui_Manager.instance.UiRefresh();
            StartCoroutine(DeadAction());
        }
        
    }
    
IEnumerator DeadAction()    
    {
        isDead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log(gameObject.name + "has dead");
        animator.SetTrigger("RunToDie");
        monsterSpeed = 0;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
        ////��ǥ3: �̵�, �׾��� �� �ִϸ��̼� ���� �� �ڿ� ����
        //Ui_Manager.instance.UiRefresh();

    }
}