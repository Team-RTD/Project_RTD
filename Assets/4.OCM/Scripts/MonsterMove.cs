using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


//��ǥ1: �������� �̵��ϱ�
//�ʿ�Ӽ�1: �̵��ӵ�,����

//��ǥ2: Enemyü�� ����
//�ʿ�Ӽ�2: EnemyHp

//��ǥ3: �������� �̵��ϴٰ� Ư������ �� �������� ����

//��ǥ4: �װų� ������ �����ϸ� �ı� ����Ʈ ����
//�ʿ�Ӽ�4: ����Ʈ ��ƼŬ �ý���, �ı� ȿ�� ���� ������Ʈ
public class MosnterMove : MonoBehaviour
{
    public Sprite portrait;
    //�ʿ�Ӽ�1: �̵��ӵ�
    public float monsterSpeed=1f;
    //�ʿ�Ӽ�2: EnemyHp
    public float hp=100;
    public Transform[] Pos;
    int posloc = 0;
    public Transform startpos;
    public bool isDead = false;
    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
       
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
    }
    //public void OnTriggerEnter(Collider others)
    //{
    //  if (others.gameObject.tag == "DestroyZone")
    //  {
           
    //  }
    //}

    IEnumerator GoToPos(Transform setpos)
    {
        Vector3 dir = setpos.transform.position - transform.position;
        Vector3 loc = dir.normalized;

        while (dir.magnitude > 0.1)
        {
            transform.LookAt(setpos.transform);
            //transform.position = Vector3.Lerp(transform.position,setpos.transform.position,0.3f);
            transform.position = Vector3.MoveTowards(transform.position, setpos.transform.position, monsterSpeed) ;
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
            StartCoroutine(DeadAction());
            StageManager.instance.monsterCount--;
        }
    }
    
IEnumerator DeadAction()    
    {
        isDead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log(gameObject.name + "has dead");

        Data_Manager.instance.money1++;
        Ui_Manager.instance.UiRefresh();
        animator.SetTrigger("RunToDie");
        monsterSpeed = 0;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}