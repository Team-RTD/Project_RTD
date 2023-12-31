using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_Manager : MonoBehaviour
{
    public ClickSystem test;
    public static Input_Manager instance { get; private set; }

    public GameObject towerBuildBtn;
    public GameObject towerSellBtn;
    public GameObject towerMixBtn;
    public Color[] SaveColor = new Color[2];

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        Image[] imgs = towerBuildBtn.GetComponentsInChildren<Image>();
        for(int i = 0; i < imgs.Length; i++) 
        {
            SaveColor[i] = imgs[i].color;
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClickSystem.instance.TowerBuildBtn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClickSystem.instance.TowerMixBtn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClickSystem.instance.TowerSellBtn();
        }

        if( Input.GetKeyDown(KeyCode.Escape))
        {
            Ui_Manager.instance.OptionPannelOpen();
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpGrade_Manager.instance.WarriorUpgrade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UpGrade_Manager.instance.ArcherUpgrade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            UpGrade_Manager.instance.MageUpgrade();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Data_Manager.instance.ChangeBlueGreen();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Ui_Manager.instance.PmoneyPannelOpen();
        }


    }

}
