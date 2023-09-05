using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ǥ1: ���� �����ϴ� ��� ����
//�ʿ�Ӽ�1: �����ӿ�����Ʈ,�����ð�,�����ܰ�� �Ѿ��.

public class EnemySpawn : MonoBehaviour
{
    //�ʿ�Ӽ�1: �����ӿ�����Ʈ,�����ð�,�ѻ����ð�
    //public GameObject enemy;
    public float createTime;
    public float currentTime;
    public float nextStageTime;
    public int stageNum;
    public bool nextStage;
    public int monsterCount;
    public List<GameObject> monsterList = new List<GameObject>();




    // Start is called before the first frame update
    void Start()
    {
        stageNum = 1;
        createTime = 0.2f;
        nextStage = true;
    }


    // Update is called once per frame
     void Update()
    {
        currentTime += Time.deltaTime;
        if (StageManager.instance.monsterCount == 0 && nextStage == true&& currentTime > nextStageTime)
        {
            nextStage = false;
            StartCoroutine(SpawnEnemy(stageNum));
            currentTime = 0;
        }

    }

    IEnumerator SpawnEnemy(int stage)
    {
        
        Data_Manager.instance.curRound = stageNum;
        Ui_Manager.instance.UiRefresh();
        for (int i = 0; i < 15; i++)
        {
            GameObject enemyGO = Instantiate(monsterList[stage-1]);
            enemyGO.transform.position = transform.position;
            enemyGO.transform.rotation = Quaternion.Euler(0, 180, 0);
            currentTime = 0f;
            yield return new WaitForSeconds(0.3f);
            currentTime = 0f;
            monsterCount++;
        }
        nextStage=true;
        stageNum++;
    }
}

