using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ǥ1: ���� �����ϴ� ��� ����
//�ʿ�Ӽ�1: �����ӿ�����Ʈ,����ð�,Ư���ð�


//��ǥ2: Ư���ð��� ������, ���Ͱ� �� ������ �����̰�, ���Ͱ� �� ���������� �������������� �Ѿ��.
//�ʿ�Ӽ�2:���� ��,�������� ����


//��ǥ3: ���� ��ȯ �� ����Ʈ ����
//�ʿ�Ӽ�3: ����Ʈ
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



    // Start is called before the first frame update
    void Start()
    {
        StageManager.instance.stageNum = 1;
        createTime = 0.2f;
        nextStage = true;
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

    }


    //��ǥ1: ���� �����ϴ� ��� ����
    IEnumerator SpawnEnemy(int stage)
    {
        
        Data_Manager.instance.curRound = StageManager.instance.stageNum;
        Ui_Manager.instance.UiRefresh();
        StageManager.instance.monsterCount=20;
        for (int i = 0; i < 20; i++)
        {
            GameObject enemyGO = Instantiate(monsterList[stage-1]);
            enemyGO.transform.position = transform.position;
            enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);

            //��ǥ3: ���� ��ȯ �� ����Ʈ ����
            GameObject spawnEffect = Instantiate(spawnEffect0,transform.position, Quaternion.identity);
            Destroy(spawnEffect, 3f);
            currentTime = 0f;
            yield return new WaitForSeconds(0.3f);
            currentTime = 0f;
            
        }
        nextStage=true;
        StageManager.instance.stageNum++;
    }
}

