using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


//��ǥ1: �������� �̵��ϱ�
//�ʿ�Ӽ�1: �̵��ӵ�,����

//��ǥ2: Enemyü�� ����
//�ʿ�Ӽ�2: EnemyHp

<<<<<<< HEAD
//��ǥ3: �������� �̵��ϴٰ� Ư������ �� �������� ����
=======

//��ǥ3: �̵�, �׾��� �� �ִϸ��̼� ���� �� �ڿ� ����
//�ʿ�Ӽ�3: �ִϸ�����

>>>>>>> main

//��ǥ4:  ������ �����ϸ� �ı� ����Ʈ ����
//�ʿ�Ӽ�4: ����Ʈ ��ƼŬ �ý���, �ı� ȿ�� ���� ������Ʈ
<<<<<<< HEAD
public class EnemyMove : MonoBehaviour
{
    //�ʿ�Ӽ�1: �̵��ӵ�
    public float enemySpeed;
    //�ʿ�Ӽ�2: EnemyHp
    public float hp;
    public Transform[] Pos;
    int posloc = 0;
    public Transform startpos;
=======

//��ǥ5: �������� �޾��� �� ü���� ��� 0���ϰ� �Ǹ� �״´�.

public class MonsterMove : MonoBehaviour
{
    //�ʿ�Ӽ�1: Ư����ǥ,�̵��ӵ�
    public float monsterSpeed=0.1f;
    public Transform[] Pos;
    int posloc = 0;
    public Transform startpos;
    

    //�ʿ�Ӽ�2: EnemyHp
    public float hp ;
    


    //�ʿ�Ӽ�3: �ִϸ�����
>>>>>>> main
    public bool isDead = false;
    Animator animator;

    




// Start is called before the first frame update
void Start()
    {
<<<<<<< HEAD
        
        hp = 100;
        enemySpeed = 1;
        animator = GetComponent<Animator>();

        Transform ArrowPosParent = GameObject.Find("ArrowPos").transform;
        Pos = new Transform[ArrowPosParent.childCount];

        for (int i = 0; i < ArrowPosParent.childCount; i++)
        {
            Pos[i] = ArrowPosParent.GetChild(i);
=======
        //��ǥ2: Enemyü�� ����
        if (StageManager.instance.stageNum % 10 == 0 && StageManager.instance.stageNum>1)
        { 
            hp = StageManager.instance.monsterMaxHp*10; 
>>>>>>> main
        }
        else
        {
            hp = StageManager.instance.monsterMaxHp;
        }
        
            
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
<<<<<<< HEAD
        //transform.Translate(-dir * enemySpeed * Time.deltaTime, Space.Self);
        
        //enemyHp--;

        if (hp == 0)
        {
            StartCoroutine(Die());
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
=======
        DamagedAction(1);
    }
    
>>>>>>> main

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



    //��ǥ5: �������� �޾��� �� ü���� ��� 0���ϰ� �Ǹ� �״´�.
    public void DamagedAction(float _damage)
    {
        if (isDead == true) { return; }

        hp = hp - _damage;

        print(gameObject.name + " has damaged " + _damage + "FinalHP" + hp);

        if (hp <= 0)
        {
<<<<<<< HEAD
            DeadAction();
=======
            StageManager.instance.monsterCount--;
            StartCoroutine(DeadAction());
>>>>>>> main
        }
    }
    public void DeadAction()
    {
        isDead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log(gameObject.name + "has dead");
<<<<<<< HEAD
=======
        animator.SetTrigger("RunToDie");
        monsterSpeed = 0;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
        //��ǥ3: �̵�, �׾��� �� �ִϸ��̼� ���� �� �ڿ� ����
        Data_Manager.instance.money1++;
        Ui_Manager.instance.UiRefresh();

>>>>>>> main
    }
}