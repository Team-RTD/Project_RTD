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

    public float missionCoolTime1;
    public float missionCoolTime2;
    public float missionCoolTime3;

    public bool missionTrigger1;
    public bool missionTrigger2;
    public bool missionTrigger3;

    public int missionNum;

     void Awake()
    {
       
        createTime = 0.2f;
        nextStage = true;
        missionTrigger1 = true;
        missionTrigger2 = true;
        missionTrigger3 = true;
        missionCoolTime1 = 0.0f;
        missionCoolTime2 = 0.0f;
        missionCoolTime3 = 0.0f;

    }
    // Start is called before the first frame update
    void Start()
    {
        StageManager.instance.stageNum = 1;
    }


    // Update is called once per frame
     void Update()
    {
        

        //목표2: 특정시간이 지나고, 몬스터가 다 생성된 다음이고, 몬스터가 다 죽은다음에 다음스테이지로 넘어간다.
        missionCoolTime1 += Time.deltaTime;
        missionCoolTime2 += Time.deltaTime;
        missionCoolTime3 += Time.deltaTime;
        currentTime += Time.deltaTime;
        if (missionCoolTime1 > 20.0f)
        {
            missionTrigger1 = true;
        }

        if (missionCoolTime2 > 20.0f)
        {
            missionTrigger2 = true;
        }

        if (missionCoolTime3 > 20.0f)
        {
            missionTrigger3 = true;
        }
        
        if (StageManager.instance.monsterCount == 0 && nextStage == true&& currentTime > nextStageTime)
        {
            nextStage = false;
            if (StageManager.instance.stageNum > 1)
            {
                Data_Manager.instance.money1 += 200;
            }
            
            StartCoroutine(SpawnEnemy(StageManager.instance.stageNum));
            currentTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z)&& missionTrigger1 == true)
        {
            StartCoroutine(PersonalMission1());
            StageManager.instance.missionNum = 1;
            StageManager.instance.monsterCount++;
        }

        if (Input.GetKeyDown(KeyCode.X) && missionTrigger2 == true)
        {
            StartCoroutine(PersonalMission2());
            StageManager.instance.missionNum = 2;
            StageManager.instance.monsterCount++;
        }

        if (Input.GetKeyDown(KeyCode.C) && missionTrigger3 == true)
        {
            StartCoroutine(PersonalMission3());
            StageManager.instance.missionNum = 3;
            StageManager.instance.monsterCount++;
        }
        

    }


    //목표1: 적을 생성하는 기능 구현
    IEnumerator SpawnEnemy(int stage)
    {
        
        Data_Manager.instance.curRound = StageManager.instance.stageNum;
        Ui_Manager.instance.UiRefresh();
        

        if (StageManager.instance.stageNum % 10 == 0 && StageManager.instance.stageNum>1)
        {
            StageManager.instance.monsterCount++;
            GameObject enemyGO = Instantiate(monsterList[stage - 1], transform.position, Quaternion.Euler(0, 180, 0));


            //목표3: 몬스터 소환 시 이펙트 생성
            GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
            Destroy(spawnEffect, 0.5f);
            currentTime = 0f;
            yield return new WaitForSeconds(0.3f);
            currentTime = 0f;
        }
        else
        {
            StageManager.instance.monsterCount = 20;
            for (int i = 0; i < 20; i++)
            {

                GameObject enemyGO = Instantiate(monsterList[stage - 1],transform.position, Quaternion.Euler(0, 180, 0));


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
        //StageManager.instance.missionNum = 1;
        GameObject enemyGO = Instantiate(missionMonster1, transform.position, Quaternion.Euler(0, 180, 0));
        enemyGO.GetComponent<MonsterMove>().SetEnum();



        GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
        Destroy(spawnEffect, 0.5f);


        missionTrigger1 = false;
        missionCoolTime1 = 0f;
        yield return null;
    }

    IEnumerator PersonalMission2()
    {
        //StageManager.instance.missionNum = 2;
        GameObject enemyGO = Instantiate(missionMonster2, transform.position, Quaternion.Euler(0, 180, 0));
        enemyGO.GetComponent<MonsterMove>().SetEnum();

        GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
        Destroy(spawnEffect, 0.5f);


        missionTrigger2 = false;
        missionCoolTime2 = 0f;
        yield return null;
    }

    IEnumerator PersonalMission3()
    {
        //StageManager.instance.missionNum = 3;
        GameObject enemyGO = Instantiate(missionMonster3, transform.position, Quaternion.Euler(0, 180, 0));
        enemyGO.GetComponent<MonsterMove>().SetEnum();


        GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
        Destroy(spawnEffect, 0.5f);


        missionTrigger3 = false;
        missionCoolTime3 = 0f;
        yield return null;
    }
}

