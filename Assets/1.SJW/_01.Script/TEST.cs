using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.AddComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        Outline line = GetComponent<Outline>();
        line.enabled = true;
    }

    private void OnMouseUp()
    {
        switch (ClickSystem.instance.playerMode)
        {

            case ClickSystem.PlayerMode.Nomal:
                Ui_Manager.instance.InfoPannelActive = true;
                Ui_Manager.instance.InfoPannelRefresh(gameObject);

                break;
            case ClickSystem.PlayerMode.TowerSell: //타워 판매 구현
                Sound_Manager.instance.NarPlay(16);
                Tower_Manager.instance.TowerSell(gameObject,true);
                break;
            case ClickSystem.PlayerMode.TowerBuild:
                

                break;

                /*case ClickSystem.PlayerMode.TowerMix:
                Tower_Manager.instance.TowerUp(gameObject);
                break;*/
        }

    }



    private void OnMouseExit()
    {
        Outline line = GetComponent<Outline>();
        line.enabled = false;
    }
    // Update is called once per frame

    private void Update()
    {
        if(GetComponent<Outline>().enabled)
        { OutLineColorChange(); }
        
    }

    public void OutLineColorChange()
    {
        Outline line = GetComponent<Outline>();
        switch (ClickSystem.instance.playerMode)
        {
            case ClickSystem.PlayerMode.Nomal:
                line.OutlineColor = Color.green;

                break;
            case ClickSystem.PlayerMode.TowerSell:
                line.OutlineColor = Color.red;

                break;
            case ClickSystem.PlayerMode.TowerBuild:
                line.OutlineColor = Color.clear;

                break;
            case ClickSystem.PlayerMode.TowerMix:
                line.OutlineColor = Color.red;

                break;
        }
    }

}
