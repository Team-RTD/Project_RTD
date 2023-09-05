using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    public ClickSystem test;
    public static Input_Manager instance { get; private set; }


    private void Awake()
    {
        instance = this;

    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClickSystem.instance.TowerBuildBtn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClickSystem.instance.TowerSellBtn();
        }

    }
}
