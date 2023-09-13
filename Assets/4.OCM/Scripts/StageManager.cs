using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//목표1: StageManager가 monsterCount를 받기
//필요속성1: 몬스터수

public class StageManager : MonoBehaviour
{
    //필요속성1: 몬스터수
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
