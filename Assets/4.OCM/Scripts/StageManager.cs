using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text clearTimer;
    public TMP_Text killCount;
    public TMP_Text curHp;
    public GameObject gameClear;
    public bool endBool;

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
        endBool = false;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageManager.instance.stageNum >= 51 && StageManager.instance.monsterCount == 0)
        {
            Data_Manager.instance.isPause = true;
            Time.timeScale = 0;
            print(1);
            endScene();
            gameClear.SetActive(true);
        }
    }

    public int SetMonsterHP(int stageNum)
    {
        monsterMaxHp = stageNum *50 ;
        return monsterMaxHp = stageNum * 50;
    }

    public void endScene()
    {
        if(endBool == false)
        {
            StageManager.instance.stageNum = 1;
            StageManager.instance.clearTimer.text = Ui_Manager.instance.Timer.text;
            StageManager.instance.killCount.text = Data_Manager.instance.killcount.ToString();
            StageManager.instance.curHp.text = Data_Manager.instance.CurHp.ToString();
            endBool = true;
        }
        
    }
}
