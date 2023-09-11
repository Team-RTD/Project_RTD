using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSystem : MonoBehaviour
{

    public static ClickSystem instance { get; private set; }

    public Camera cam;
    public GameObject test;
    private RaycastHit[] hit;

    private int towerRank;
    private string componentName;
    private GameObject towerzone;
    private GameObject clickedTower;

    public GameObject SummonEffect;
    public AudioClip SummonSound;

    public enum PlayerMode
    {
        Nomal,
        TowerBuild,
        TowerMix,
        TowerSell
    }

    public PlayerMode playerMode = PlayerMode.Nomal;

    public GameObject[] towerZone;



    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        towerZone = null;
        towerZone = GameObject.FindGameObjectsWithTag("TowerZone");
        foreach (GameObject zone in towerZone)
        {
            zone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (playerMode == PlayerMode.TowerSell)
        {
          
        }



        if(Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                switch (playerMode)
                {
                    case PlayerMode.TowerBuild://Ÿ���Ǽ���忡�� Ŭ�� ��

                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        hit = Physics.RaycastAll(ray, 100f);
                        if (hit != null)
                        {
                            foreach (RaycastHit hitob in hit)
                            {
                                GameObject hitObject = hitob.transform.gameObject;
                                if (hitObject.tag == "TowerZone")
                                {
                                    Tower_Manager.instance.TowerInstance(hitObject, 1);
                                }
                                else
                                {
                                    print("������ ����");
                                }

                            }
                        }
                        break;

                    case PlayerMode.TowerSell:


                        break;

                    /*case PlayerMode.TowerMix:

                        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);

                        RaycastHit hit1;

                        if (Physics.Raycast(ray1, out hit1))
                        {
                            Component targetComponent = hit1.collider.GetComponent(componentName);

                            if (targetComponent != null)
                            {
                                clickedTower = hit1.collider.gameObject;
                                int towerRank = (int)targetComponent.GetType().GetField("towerRank").GetValue(targetComponent);

                                if (towerRank < 6 && towerRank >= 1)
                                {
                                    string componentName = targetComponent.name;
                                    Debug.Log(targetComponent.name);

                                    GameObject towerzone = (GameObject)targetComponent.GetType().GetField("TowerZone").GetValue(targetComponent);
                                    //���� �Ŵ����� ���� ����Ʈ���� ���� �̸��� ���� �ٸ� Ÿ���� ã�ƾ��Ѵ�.

                                    string listName = "Tower" + towerRank;
                                    List<GameObject> towerList = (List<GameObject>)typeof(Tower_Manager).GetField(listName).GetValue(Tower_Manager.instance);

                                    GameObject towerObject = towerList.Find(GameObject => GameObject != clickedTower && targetComponent.name == GameObject.name);

                                    if (towerObject != null)
                                    {
                                        Tower_Manager.instance.TowerSell(towerObject, false);
                                        Tower_Manager.instance.TowerSell(clickedTower, false);

                                        Tower_Manager.instance.TowerInstance(towerzone, towerRank + 1);
                                    }
                                    else
                                    {
                                        print("�ռ��� ������ Ÿ���� �����ϴ�.");
                                    }

                                    *//*foreach (GameObject towerObject in towerList)
                                    {
                                        if (towerObject != clickedTower && targetComponent.name == towerObject.name)
                                        {
                                            Tower_Manager.instance.TowerSell(towerObject, false);
                                            Tower_Manager.instance.TowerSell(clickedTower, false);

                                            Tower_Manager.instance.TowerInstance(towerzone, towerRank + 1);
                                        }
                                        else
                                        {
                                            print("�ռ��� ������ Ÿ���� �����ϴ�.");
                                        }
                                    }*//*
                                }
                                else
                                {
                                    print("�ռ� �� �� ���� Ÿ�� �Դϴ�.");
                                }
                            }
                        }

                        break;*/



                }               
            }
        }





        if(playerMode == PlayerMode.TowerSell)
        {


        }

    }


   

    public void TowerBuildBtn()
    {
        if(playerMode!=PlayerMode.TowerBuild)
        {
            playerMode = PlayerMode.TowerBuild;
            Ui_Manager.instance.state.text = "�Ǽ� ���";
            BtnColorReset();
            BuildBtnDark();

            foreach (GameObject zone in towerZone)
            {
                if(!zone.GetComponent<TowerZone>().towerOn)
                { 
                    zone.SetActive(true); 
                }
                else
                {
                    zone.SetActive(false);
                }
                
            }
        }
        else
        {
            playerMode = PlayerMode.Nomal;
            BtnColorReset();
            Ui_Manager.instance.state.text = "";
            Image[] imgs = Input_Manager.instance.towerBuildBtn.GetComponentsInChildren<Image>();

            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false); 
                }
                else
                {
                    zone.SetActive(true);
                }
            }

        }
        
    }


    public void TowerSellBtn() 
    {
        if (playerMode != PlayerMode.TowerSell)
        {
            playerMode = PlayerMode.TowerSell;
            Ui_Manager.instance.state.text = "�Ǹ� ���";
            BtnColorReset();
            SellBtnDark();

            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }
        }
        else
        {
            playerMode = PlayerMode.Nomal;
            Ui_Manager.instance.state.text = "";
            BtnColorReset();
            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }

        }
    }

    public void TowerMixBtn()
    {
        if (playerMode != PlayerMode.TowerMix)
        {
            playerMode = PlayerMode.TowerMix;
            Ui_Manager.instance.state.text = "�ռ� ���";
            BtnColorReset();
            MixBtnDark();

            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }
        }
        else
        {
            playerMode = PlayerMode.Nomal;
            Ui_Manager.instance.state.text = "";
            BtnColorReset();
            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }

        }
    }

    public void BtnColorReset()
    {
        Image[] imgs = Input_Manager.instance.towerBuildBtn.GetComponentsInChildren<Image>();
        for(int i = 0; i < imgs.Length; i++) 
        {
            imgs[i].color = Input_Manager.instance.SaveColor[i];
        }
          
        

        imgs = Input_Manager.instance.towerSellBtn.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].color = Input_Manager.instance.SaveColor[i];
        }

        imgs = Input_Manager.instance.towerMixBtn.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].color = Input_Manager.instance.SaveColor[i];
        }

    }

    public void BuildBtnDark()
    {
        Image[] imgs = Input_Manager.instance.towerBuildBtn.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
        {
            img.color -= new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }
    public void SellBtnDark()
    {
        Image[] imgs = Input_Manager.instance.towerSellBtn.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
        {
            img.color -= new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }
    public void MixBtnDark()
    {
        Image[] imgs = Input_Manager.instance.towerMixBtn.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
        {
            img.color -= new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }


}
