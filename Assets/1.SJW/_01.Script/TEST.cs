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
        Twr_0Base towerinfo;
        TowerZone t_zone;

        switch (ClickSystem.instance.playerMode)
        {

            case ClickSystem.PlayerMode.Nomal:
                Ui_Manager.instance.InfoPannelRefresh(gameObject);

                break;
            case ClickSystem.PlayerMode.TowerSell: //타워 판매 구현
                 towerinfo = gameObject.GetComponent<Twr_0Base>();
                 t_zone = towerinfo.TowerZone.GetComponent<TowerZone>();
                Material mat = t_zone.gameObject.GetComponent<Renderer>().material;
                t_zone.towerOn = false;
                mat.SetColor("_EmisColor", t_zone.summonZoneColor[0]);
                Destroy(t_zone.Tower);
                t_zone.gameObject.SetActive(false);

                Data_Manager.instance.money1 += 75;
                Ui_Manager.instance.UiRefresh();

                break;
            case ClickSystem.PlayerMode.TowerBuild:
                

                break;
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
        }
    }

}
