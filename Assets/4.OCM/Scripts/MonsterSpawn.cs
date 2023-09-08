using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;

//목표1: 적을 생성하는 기능 구현
//필요속성1: 적게임오브젝트,현재시간,특정시간


//목표2: 특정시간이 지나고, 몬스터가 다 생성된 다음이고, 몬스터가 다 죽은다음에 다음스테이지로 넘어간다.
//필요속성2:몬스터 수,스테이지 구별


//목표3: 몬스터 소환 시 이펙트 생성
//필요속성3: 이펙트


//목표4: z,x,c를 누르면 개인미션1,2,3 을 시작한다.
//필요속성4: 몬스터1,2,3   미션시간1,2,3
public class MonsterSpawn : MonoBehaviour
{
    //필요속성1: 적게임오브젝트,생성시간,총생성시간
    public float createTime;
    public float currentTime;
    public float nextStageTime;
    public List<GameObject> monsterList = new List<GameObject>();


    //필요속성2:,몬스터 수,스테이지 구별
    public bool nextStage;
    public int monsterCount;


    //필요속성3: 이펙트
    public GameObject spawnEffect0;


    //필요속성4: 몬스터1,2,3   미션시간1,2,3
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
        

        //목표2: 특정시간이 지나고, 몬스터가 다 생성된 다음이고, 몬스터가 다 죽은다음에 다음스테이지로 넘어간다.
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


    //목표1: 적을 생성하는 기능 구현
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

            //목표3: 몬스터 소환 시 이펙트 생성
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

                //목표3: 몬스터 소환 시 이펙트 생성
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

