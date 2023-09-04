using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    public static Data_Manager instance { get; private set; }

    public int maxRound = 50;
    public int curRound;

    public int maxHp = 25;
    public int curHp;

    public int money1 = 0;
    public int money2 = 0;
    public int money3 = 0;



    public void DataReset()
    {
        curRound = 1;

        maxHp = 25;
        curHp = 25;

        money1 = 0;
        money2 = 0;
        money3 = 0;
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
