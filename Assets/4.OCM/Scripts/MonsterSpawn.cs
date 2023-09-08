using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;

//��ǥ1: ���� �����ϴ� ��� ����
//�ʿ�Ӽ�1: �����ӿ�����Ʈ,����ð�,Ư���ð�


//��ǥ2: Ư���ð��� ������, ���Ͱ� �� ������ �����̰�, ���Ͱ� �� ���������� �������������� �Ѿ��.
//�ʿ�Ӽ�2:���� ��,�������� ����


//��ǥ3: ���� ��ȯ �� ����Ʈ ����
//�ʿ�Ӽ�3: ����Ʈ


//��ǥ4: z,x,c�� ������ ���ι̼�1,2,3 �� �����Ѵ�.
//�ʿ�Ӽ�4: ����1,2,3   �̼ǽð�1,2,3
public class MonsterSpawn : MonoBehaviour
{
    //�ʿ�Ӽ�1: �����ӿ�����Ʈ,�����ð�,�ѻ����ð�
    public float createTime;
    public float currentTime;
    public float nextStageTime;
    public List<GameObject> monsterList = new List<GameObject>();


    //�ʿ�Ӽ�2:,���� ��,�������� ����
    public bool nextStage;
    public int monsterCount;


    //�ʿ�Ӽ�3: ����Ʈ
    public GameObject spawnEffect0;


    //�ʿ�Ӽ�4: ����1,2,3   �̼ǽð�1,2,3
    public GameObject missionMonster1;
    public GameObject missionMonster2;
    public GameObject missionMonster3;

    public int missionCoolTime1;
    public int missionCoolTime2;
    public int missionCoolTime3;

    public bool missionTrigger1;
    public bool missionTrigger2;
    public bool missionTrigger3;

    // Start is called before the first frame update
    void Start()
    {
        StageManager.instance.stageNum = 1;
        createTime = 0.2f;
        nextStage = true;
        missionTrigger1 = true;
        missionTrigger2 = true;
        missionTrigger3 = true;
    }


    // Update is called once per frame
     void Update()
    {
        

        //��ǥ2: Ư���ð��� ������, ���Ͱ� �� ������ �����̰�, ���Ͱ� �� ���������� �������������� �Ѿ��.
        currentTime += Time.deltaTime;
        if (StageManager.instance.monsterCount == 0 && nextStage == true&& currentTime > nextStageTime)
        {
            nextStage = false;
            StartCoroutine(SpawnEnemy(StageManager.instance.stageNum));
            currentTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(PersonalMission1());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(PersonalMission2());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(PersonalMission3());
        }
        

    }


    //��ǥ1: ���� �����ϴ� ��� ����
    IEnumerator SpawnEnemy(int stage)
    {
        
        Data_Manager.instance.curRound = StageManager.instance.stageNum;
        Ui_Manager.instance.UiRefresh();
        StageManager.instance.monsterCount=20;

        if (StageManager.instance.stageNum % 10 == 0 && StageManager.instance.stageNum>1)
        {
            StageManager.instance.monsterCount = 1;
            GameObject enemyGO = Instantiate(monsterList[stage - 1]);
            enemyGO.transform.position = transform.position;
            enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);

            //��ǥ3: ���� ��ȯ �� ����Ʈ ����
            GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
            Destroy(spawnEffect, 0.5f);
            currentTime = 0f;
            yield return new WaitForSeconds(0.3f);
            currentTime = 0f;
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {

                GameObject enemyGO = Instantiate(monsterList[stage - 1]);
                enemyGO.transform.position = transform.position;
                enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);

                //��ǥ3: ���� ��ȯ �� ����Ʈ ����
                GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
                Destroy(spawnEffect, 0.5f);
                currentTime = 0f;
                yield return new WaitForSeconds(0.3f);
                currentTime = 0f;

            }
        }


        
        nextStage=true;
        StageManager.instance.stageNum++;
        StageManager.instance.monsterMaxHp = StageManager.instance.SetMonsterHP(StageManager.instance.stageNum);
    }




    IEnumerator PersonalMission1()
    {
        GameObject enemyGO = Instantiate(missionMonster1);
        enemyGO.transform.position = transform.position;
        enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
        Destroy(spawnEffect, 0.5f);
        missionTrigger1 = false;
        yield return null;
    }

    IEnumerator PersonalMission2()
    {
        GameObject enemyGO = Instantiate(missionMonster2);
        enemyGO.transform.position = transform.position;
        enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
        Destroy(spawnEffect, 0.5f);
        missionTrigger2 = false;    
        yield return null;
    }

    IEnumerator PersonalMission3()
    {
        GameObject enemyGO = Instantiate(missionMonster3);
        enemyGO.transform.position = transform.position;
        enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
        Destroy(spawnEffect, 0.5f);
        missionTrigger3 = false;    
        yield return null;
    }
}

