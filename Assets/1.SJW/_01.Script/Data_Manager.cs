using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    public static Data_Manager instance { get; private set; }

    public int maxRound = 50;
    public int curRound;

    public int maxHp = 5;
    public int curHp;

    public int money1 = 0;
    public int money2 = 0;
    public int money3 = 0;

    public int killcount=0;

    public bool isPause = false;

    public void DataReset()
    {
        curRound = 1;

        maxHp = 5;
        curHp = 5;

        money1 = 500;
        money2 = 50;
        money3 = 0;
    }



    public void ChangeBlueGreen()
    {
        if(money1 >=100)
        {
            money1 -= 100;

            money2 += Random.Range(40, 161);
        }
        else
        {
            Ui_Manager.instance.state.text = "재화 부족!";

        }
        Ui_Manager.instance.UiRefresh();
    }

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
