using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


//목표1: 정면으로 이동하기
//필요속성1: 이동속도,방향

//목표2: Enemy체력 구현
//필요속성2: EnemyHp

//목표3: 정면으로 이동하다가 특정조건 시 왼쪽으로 돌기

//목표4: 죽거나 끝까지 도달하면 파괴 이펙트 생성
//필요속성4: 이펙트 파티클 시스템, 파괴 효과 게임 오브젝트
public class EnemyMove : MonoBehaviour
{
    //필요속성1: 이동속도
    public float enemySpeed;
    //필요속성2: EnemyHp
    public float hp;
    public Transform[] Pos;
    int posloc = 0;
    public Transform startpos;
    public bool isDead = false;
    Animator animator;

    




// Start is called before the first frame update
void Start()
    {
        
        hp = 100;
        enemySpeed = 1;
        StageManager.instance.monsterCount++;
        animator = GetComponent<Animator>();

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
        //transform.Translate(-dir * enemySpeed * Time.deltaTime, Space.Self);
        
        //enemyHp--;

        if (hp == 0)
        {
            StartCoroutine(Die());
            StageManager.instance.monsterCount--;
        }
    }


    
    IEnumerator Die()
    {
        Data_Manager.instance.money1++;
        Ui_Manager.instance.UiRefresh();
        animator.SetTrigger("RunToDie");
        enemySpeed = 0;
        
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    
    public void OnTriggerEnter(Collider others)
    {
      if (others.gameObject.tag == "DestroyZone")
      {
            //monsterCount--;
      }
    }

    IEnumerator GoToPos(Transform setpos)
    {
        Vector3 dir = setpos.transform.position - transform.position;
        Vector3 loc = dir.normalized;

        while (dir.magnitude > 0.1)
        {
            transform.LookAt(setpos.transform);
            //transform.position = Vector3.Lerp(transform.position,setpos.transform.position,0.3f);
            transform.position = Vector3.MoveTowards(transform.position, setpos.transform.position, 0.5f);
            //transform.Translate(loc,Space.World);
            dir = setpos.transform.position - transform.position;
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

    public void DamagedAction(float _damage)
    {
        if (isDead == true) { return; }

        hp = hp - _damage;

        print(gameObject.name + " has damaged " + _damage + "FinalHP" + hp);

        if (hp <= 0)
        {
            DeadAction();
        }
    }
    public void DeadAction()
    {
        isDead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log(gameObject.name + "has dead");
    }
}