using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��ǥ1: StageManager�� monsterCount�� �ޱ�
//�ʿ�Ӽ�1: ���ͼ�

public class StageManager : MonoBehaviour
{
    //�ʿ�Ӽ�1: ���ͼ�
    public int monsterCount = 0;
    public int monsterMaxHp = 50 ;
    public int stageNum = 1;

    public static StageManager instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
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