using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
    public static Ui_Manager instance { get; private set; }

    public GameObject roundZone;
    public GameObject moneyZone;

    public TMP_Text roundTxt;
    public TMP_Text lifeTxt;
    public TMP_Text money1Txt;
    public TMP_Text money2Txt;
    public TMP_Text money3Txt;

    public TMP_Text upgrade_plus;

    public TMP_Text state;

    public GameObject towerInfo;

    public GameObject btnMenu;

    public GameObject InfoZone;
    public Image tower_portrait;
    public TMP_Text tower_Name;
    public TMP_Text tower_Info;
    public TMP_Text tower_rank;

    private bool drawerRound = false;
    private Vector2 roundrec;
    private Vector2 moneyrec;
    private Vector2 end_pos;
    private bool drawerMoney = false;

    public GameObject OptionPannel;
    GameObject lastinfoOb;

    private void Awake()
    {
        instance = this;

    }


    private void Start()
    {
        UIreset();
    }



    public void UiRefresh()
    {
        roundTxt.text = Data_Manager.instance.curRound + "/" + Data_Manager.instance.maxRound;
        lifeTxt.text = Data_Manager.instance.curHp.ToString();


        money1Txt.text = Data_Manager.instance.money1.ToString();
        money2Txt.text = Data_Manager.instance.money2.ToString();
        money3Txt.text = Data_Manager.instance.money3.ToString();

        if(UpGrade_Manager.instance.upgrade_rank != 0)
        upgrade_plus.text = "+"+ UpGrade_Manager.instance.upgrade_rank;

        if(lastinfoOb != null)
        InfoPannelRefresh(lastinfoOb);
    }


    void UIreset() // ui 초기값으로 배치
    {
        Data_Manager.instance.DataReset();

        roundTxt.text = Data_Manager.instance.curRound + "/" + Data_Manager.instance.maxRound;
        lifeTxt.text = Data_Manager.instance.curHp.ToString();


        money1Txt.text = Data_Manager.instance.money1.ToString();
        money2Txt.text = Data_Manager.instance.money2.ToString();
        money3Txt.text = Data_Manager.instance.money3.ToString();

        roundrec = roundZone.GetComponent<RectTransform>().anchoredPosition;
        moneyrec = moneyZone.GetComponent<RectTransform>().anchoredPosition;

        state.text = "";
        upgrade_plus.text = "";
        tower_portrait.color = Color.clear;
    }


    public void RoundUidrawer() 
    { 
        RectTransform rect = roundZone.GetComponent<RectTransform>();
        Vector2 start_pos = rect.anchoredPosition;
        Vector2 end_pos;
        if (!drawerRound) // 열려있을때
        {
             end_pos = start_pos + new Vector2(0, 140);
             drawerRound = true;
        }
        else // 닫혀있을때
        {
             end_pos = roundrec;
            drawerRound = false;
        }
       

        StartCoroutine(LerfUI(rect, start_pos, end_pos,0.2f));
    }

    public void MoneydUidrawer()
    {
        RectTransform rect = moneyZone.GetComponent<RectTransform>();
        Vector2 start_pos = rect.anchoredPosition;
        Vector2 end_pos;
        if (!drawerMoney)
        {
            end_pos = start_pos + new Vector2(0, 140);
            drawerMoney = true;
        }
        else
        {
            end_pos = moneyrec;
            drawerMoney = false;
        }


        StartCoroutine(LerfUI(rect, start_pos, end_pos, 0.2f));
    }

    public void OptionPannelOpen()
    {
        if(OptionPannel.activeSelf) 
        {
            OptionPannel.SetActive(false);
        }
        else
        {
            OptionPannel.SetActive(true);
        }
    }

    IEnumerator LerfUI(RectTransform target,Vector2 start_pos,Vector2 end_pos,float mtime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < mtime)
        {
            float t = elapsedTime / mtime;
            target.anchoredPosition = Vector2.Lerp(start_pos, end_pos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.anchoredPosition = end_pos;
        //isMoving = false;

    }



    public void InfoPannelRefresh(GameObject infoOb)
    {
        lastinfoOb = infoOb;
        Twr_0Base towerinfo = infoOb.gameObject.GetComponent<Twr_0Base>();
        TowerZone t_zone = towerinfo.TowerZone.GetComponent<TowerZone>();
        string _towerType="";
        string _towerAttackType="";

        switch(towerinfo.towerType)
        {
            case Twr_0Base.TowerType.Archer:
                _towerType = "궁수";
                break;
            case Twr_0Base.TowerType.Mage:
                _towerType = "마법사";
                break;
            case Twr_0Base.TowerType.Warrior:
                _towerType = "전사";
                break;
        }

        switch (towerinfo.towerAttackType)
        {
            case Twr_0Base.TowerAttackType.Area:
                _towerAttackType = "범위형";
                break;
            case Twr_0Base.TowerAttackType.Shooter:
                _towerAttackType = "즉발형";
                break;
            case Twr_0Base.TowerAttackType.Thrower:
                _towerAttackType = "전사형";
                break;
        }
        tower_portrait.sprite = towerinfo.portrait;
        tower_portrait.color = Color.white;
        tower_Name.text = towerinfo.towerName;
        tower_rank.text = "★" + towerinfo.towerUpgradeTier;
        tower_Info.text = "타입 : " + _towerType + "/"+_towerAttackType+
          "\n공격력 : " + towerinfo.towerAttackDamage +"(+"+UpGrade_Manager.instance.upgradePercent + "%)"+
          "\n공격속도 : " + towerinfo.towerAttackSpeed +
          "\n사정거리 : " + towerinfo.towerAttackRange;

    
    }

}
