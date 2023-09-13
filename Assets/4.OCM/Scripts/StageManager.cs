using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��ǥ1: StageManager�� monsterCount�� �ޱ�
//�ʿ�Ӽ�1: ���ͼ�

public class StageManager : MonoBehaviour
{
    //�ʿ�Ӽ�1: ���ͼ�
    public int monsterCount;
    public int monsterMaxHp;
    public int stageNum;
    public int missionMonster1Hp;
    public int missionMonster2Hp;
    public int missionMonster3Hp;
    public int missionNum;
    public int mission1CoolTime;
    public int mission2CoolTime;
    public int mission3CoolTime;

    public static StageManager instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    { 
        instance = this;
        monsterCount = 0;
        monsterMaxHp = 50;
        stageNum = 1;
        missionMonster1Hp = 5000;
        missionMonster2Hp = 15000;
        missionMonster3Hp = 50000;
        mission1CoolTime = 20;
        mission2CoolTime = 30;
        mission3CoolTime = 40;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SetMonsterHP(int stageNum)
    {
        monsterMaxHp = stageNum *50 ;
        return monsterMaxHp = stageNum * 50;
    }
}
