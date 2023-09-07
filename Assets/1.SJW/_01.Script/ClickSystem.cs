using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSystem : MonoBehaviour
{

    public static ClickSystem instance;

    public Camera cam;
    public GameObject test;
    private RaycastHit[] hit;

    public GameObject SummonEffect;
    public AudioClip SummonSound;
    public GameObject[] hitObjects;
    private int currentIndex = 0;
    public GameObject towerPrefab;
    public GameObject[] clickedObjects;

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
        clickedObjects = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {

        if (playerMode == PlayerMode.TowerSell)
        {
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //hit = Physics.RaycastAll(ray, 100f);
            //if (hit != null)
            //{
            //    foreach (RaycastHit hitob in hit)
            //    {
            //        GameObject hitObject = hitob.transform.gameObject;
            //        if (hitObject.tag == "Tower")
            //        {

            //            GameObject tower1 = hitObject;

            //            Outline charliner = tower1.GetComponent<Outline>(); //���� Ÿ���� �ܰ��� �߰�
            //            charliner.enabled = true;

            //        }
            //        else
            //        {
            //        }

            //    }
            //}
        }



        if (Input.GetMouseButtonDown(0))
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
                                    TowerZone t_zone = hitObject.GetComponent<TowerZone>();
                                    if (!t_zone.towerOn)
                                    {
                                        Material mat = hitObject.GetComponent<Renderer>().material;
                                        t_zone.towerOn = true;
                                        mat.SetColor("_EmisColor", t_zone.summonZoneColor[1]);
                                        GameObject tower1 = Instantiate(test, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        GameObject summoneffect = Instantiate(SummonEffect, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        Destroy(summoneffect, 3); // ����Ʈ�� 3�ʵ� ����
                                        Sound_Manager.instance.EffectPlay(SummonSound);

                                        Outline charliner = tower1.AddComponent<Outline>(); //���� Ÿ���� �ܰ��� �߰�
                                        charliner.OutlineColor = Color.red;
                                        charliner.OutlineWidth = 2;
                                        charliner.enabled = false;

                                        hitObject.SetActive(false);
                                        //hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_EmisColor");
                                        //print(zone.GetComponent<Renderer>().material);
                                        //print(mat.enabledKeywords.ToString());
                                        //print(mat.shader.GetPropertyName(0));
                                        //print(mat.shader.GetPropertyName(1));
                                    }
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
                    //Ÿ�� CombineMode.
                    case PlayerMode.TowerMix:
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (!EventSystem.current.IsPointerOverGameObject())
                            {
                                Ray _ray = cam.ScreenPointToRay(Input.mousePosition);
                                hit = Physics.RaycastAll(_ray, 100f);
                                if (hit != null)
                                {                                     
                                    foreach (RaycastHit hitob in hit)
                                    {                                        
                                        GameObject hitObject = hitob.transform.gameObject;

                                        // Ÿ�� ���� ��ũ��Ʈ ��������
                                        Twr_0Base towerInfo = hitObject.GetComponent<Twr_0Base>();
                                        
                                        if (towerInfo != null)
                                        {
                                            // ��ũ ���� ��������
                                            int towerRank = towerInfo.towerRank;

                                            // �迭�� ������Ʈ ����
                                            clickedObjects[currentIndex] = hitObject;
                                            currentIndex++;

                                            if (currentIndex >= clickedObjects.Length)
                                            {
                                                currentIndex = 0;
                                            }

                                            if (clickedObjects[1] != null)
                                            {
                                                if (clickedObjects[0] != clickedObjects[1])
                                                {
                                                    // Ÿ�� ��ũ ��
                                                    if (clickedObjects[0].GetComponent<Twr_0Base>().towerRank == clickedObjects[1].GetComponent<Twr_0Base>().towerRank)
                                                    {
                                                        // ���ο� Ÿ�� ����
                                                        print("��ũ2 �Ǵ� ��ũ3�� Ÿ���� �����Ǿ����ϴ�");

                                                        Destroy(clickedObjects[0]);
                                                        Destroy(clickedObjects[1]);

                                                        clickedObjects[0] = null;
                                                        clickedObjects[1] = null;

                                                        SpawnNewTower(clickedObjects[0].GetComponent<Twr_0Base>().towerRank + 1);

                                                    }
                                                    else
                                                    {
                                                        // Ÿ�� �ռ� ����
                                                        print("�ռ� �� �� ���� ��� �Դϴ�.");

                                                        clickedObjects[0] = null;
                                                        clickedObjects[1] = null;
                                                    }
                                                }
                                                else
                                                {
                                                    // ���� Ÿ�� 2�� ����
                                                    print("�ٸ� Ÿ���� ������ �ֽʽÿ�.");
                                                    clickedObjects[1] = null;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            print("Ÿ�� ���� ��ũ��Ʈ�� �������� �ʽ��ϴ�.");
                                        }
                                    }
                                }
                            }

                        }
                        break;

                }

            }





            if (playerMode == PlayerMode.TowerSell)
            {


            }

        }
    }




    public void TowerBuildBtn()
    {
        if (playerMode != PlayerMode.TowerBuild)
        {
            playerMode = PlayerMode.TowerBuild;
            Ui_Manager.instance.state.text = "�Ǽ� ���";
            BtnColorReset();
            BuildBtnDark();

            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
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
        for (int i = 0; i < imgs.Length; i++)
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

    public Twr_TestTower component; // ���ϴ� ������Ʈ Ÿ������ ��ü�ؾ� �մϴ�.        
    public int newTowerRank;
    public GameObject newTower;
    void SpawnNewTower(int newRank)
    {
        if (newTower != null)
        {
            // ���ϴ� ������Ʈ�� ���� ��� ������Ʈ ã��
            Twr_TestTower[] components = FindObjectsOfType<Twr_TestTower>();

            List<Twr_TestTower> findObjects = new List<Twr_TestTower>();

            foreach ( Twr_TestTower component in components)
            {
                if(component.towerRank == newRank)
                {
                    findObjects.Add( component );
                }
            }

            int randomIndex = Random.Range(0, findObjects.Count );

            Twr_TestTower selectedComponent = findObjects[randomIndex];
            Instantiate(newTower, clickedObjects[1].transform.position, clickedObjects[0].transform.rotation);
        }
    }
}
