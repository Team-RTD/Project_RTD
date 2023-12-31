using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image mask1;
    public Image mask2;
    public Image mask3;


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
        currentTime += Time.deltaTime;
        if (StageManager.instance.monsterCount == 0 && nextStage == true && currentTime > nextStageTime)
        {
            nextStage = false;
            if (StageManager.instance.stageNum >= 1&& StageManager.instance.stageNum<51)
            {
                if (StageManager.instance.stageNum > 1)
                {
                    Data_Manager.instance.money1 += 200;
                    
                }
                StageManager.instance.monsterMaxHp = StageManager.instance.SetMonsterHP(StageManager.instance.stageNum);
                StartCoroutine(SpawnEnemy(StageManager.instance.stageNum));
            }
            currentTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            mission1();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            mission2();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            mission3();
        }
    }


    //목표1: 적을 생성하는 기능 구현
    IEnumerator SpawnEnemy(int stage)
    {

        Data_Manager.instance.curRound = StageManager.instance.stageNum;
        
        Ui_Manager.instance.UiRefresh();
        

        if (StageManager.instance.stageNum % 10 == 0 && StageManager.instance.stageNum > 1)
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

                GameObject enemyGO = Instantiate(monsterList[stage - 1], transform.position, Quaternion.Euler(0, 180, 0));


                //목표3: 몬스터 소환 시 이펙트 생성
                GameObject spawnEffect = Instantiate(spawnEffect0, transform.position, Quaternion.identity);
                Destroy(spawnEffect, 0.5f);
                currentTime = 0f;
                yield return new WaitForSeconds(0.3f);
                currentTime = 0f;

            }

        }
        nextStage = true;
        StageManager.instance.stageNum++;
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
        mask1.rectTransform.sizeDelta = new Vector2(100, 100);
        while (!missionTrigger1)
        {
            missionCoolTime1 += Time.deltaTime;
            if (missionCoolTime1 > 1)
            {
                if (StageManager.instance.mission1CoolTime >= 20)
                {
                    missionTrigger1 = true;
                }
                else
                {
                    StageManager.instance.mission1CoolTime++;
                    mask1.rectTransform.sizeDelta = new Vector2(100, 100-StageManager.instance.mission1CoolTime*5);
                    Ui_Manager.instance.UiRefresh();
                }
                missionCoolTime1 = 0;
            }
            yield return null;
        }
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
        mask2.rectTransform.sizeDelta = new Vector2(100, 100);
        while (!missionTrigger2)
        {
            missionCoolTime2 += Time.deltaTime;
            if (missionCoolTime2 > 1)
            {
                if (StageManager.instance.mission2CoolTime >= 20)
                {   
                    missionTrigger1 = true;
                }
                else
                {
                    StageManager.instance.mission2CoolTime++;
                    mask2.rectTransform.sizeDelta = new Vector2(100, 100 - StageManager.instance.mission1CoolTime * (100/30));
                    Ui_Manager.instance.UiRefresh();
                }
                missionCoolTime2 = 0;
            }
            yield return null;
        }
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
            mask3.rectTransform.sizeDelta = new Vector2(100, 100);
        while (!missionTrigger3)
        {
            missionCoolTime3 += Time.deltaTime;
            if (missionCoolTime3 > 1)
            {
                if (StageManager.instance.mission3CoolTime >= 20)
                {
                    missionTrigger3 = true;
                }
                else
                {
                    StageManager.instance.mission3CoolTime++;
                    mask3.rectTransform.sizeDelta = new Vector2(100, 100 - StageManager.instance.mission1CoolTime * (100 / 40));
                    Ui_Manager.instance.UiRefresh();
                }
                missionCoolTime3 = 0;
            }
            yield return null;
        }

    }

    public void mission1()
        {
            if (missionTrigger1 == true)
            {
            Sound_Manager.instance.EffectPlay(5);
            StartCoroutine(PersonalMission1());
                StageManager.instance.missionNum = 1;
                StageManager.instance.monsterCount++;
                StageManager.instance.mission1CoolTime = 0;
                Ui_Manager.instance.UiRefresh();
            }
        }
    public void mission2()
        {
            if (missionTrigger2 == true)
        {
                Sound_Manager.instance.EffectPlay(6);
                StartCoroutine(PersonalMission2());
                StageManager.instance.missionNum = 2;
                StageManager.instance.monsterCount++;
                StageManager.instance.mission2CoolTime = 0;
                Ui_Manager.instance.UiRefresh();
            }
        }
    public void mission3()
        {
            if (missionTrigger3 == true)
        {
                Sound_Manager.instance.EffectPlay(7);
                StartCoroutine(PersonalMission3());
                StageManager.instance.missionNum = 3;
                StageManager.instance.monsterCount++;
                StageManager.instance.mission3CoolTime = 0;
                Ui_Manager.instance.UiRefresh();
            }
        }
}

