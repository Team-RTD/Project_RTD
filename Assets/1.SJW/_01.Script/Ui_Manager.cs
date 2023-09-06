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


    public GameObject towerInfo;

    public GameObject btnMenu;

    private bool drawerRound = false;
    private Vector2 roundrec;
    private Vector2 moneyrec;
    private Vector2 end_pos;
    private bool drawerMoney = false;

    public GameObject OptionPannel;

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




}
