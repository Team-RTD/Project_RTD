using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


//목표1: 특정 좌표로 회전하면서 이동하기
//필요속성1: 특정좌표,이동속도


//목표2: Enemy체력 구현
//필요속성2: EnemyHp


//목표3: 이동, 죽었을 때 애니메이션 구현 및 자원 증가
//필요속성3: 애니메이터


//목표4:  끝까지 도달하면 파괴 이펙트 생성
//필요속성4: 이펙트 파티클 시스템, 파괴 효과 게임 오브젝트

//목표5: 데미지를 받았을 때 체력이 닳고 0이하가 되면 죽는다.

public class MonsterMove : MonoBehaviour
{
    //필요속성1: 특정좌표,이동속도
    public float monsterSpeed=0.1f;
    public Transform[] Pos;
    int posloc = 0;
    public Transform startpos;
    

    //필요속성2: EnemyHp
    public float hp ;
    


    //필요속성3: 애니메이터
    public bool isDead = false;
    Animator animator;


    public Sprite portrait;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        //목표2: Enemy체력 구현
        if (StageManager.instance.stageNum % 10 == 0 && StageManager.instance.stageNum>1)
        { 
            hp = StageManager.instance.monsterMaxHp*10; 
        }
        else
        {
            hp = StageManager.instance.monsterMaxHp;
        }
        
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
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed) ;
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



    //목표5: 데미지를 받았을 때 체력이 닳고 0이하가 되면 죽는다.
    public void DamagedAction(float _damage)
    {
        if (isDead == true) { return; }

        hp = hp - _damage;

        print(gameObject.name + " has damaged " + _damage + "FinalHP" + hp);

        if (hp <= 0)
        {
            Data_Manager.instance.money1++;
            StageManager.instance.monsterCount--;
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
        //목표3: 이동, 죽었을 때 애니메이션 구현 및 자원 증가
        Ui_Manager.instance.UiRefresh();

    }
}