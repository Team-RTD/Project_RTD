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

            //            Outline charliner = tower1.GetComponent<Outline>(); //만든 타워에 외각선 추가
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
                    case PlayerMode.TowerBuild://타워건설모드에서 클릭 시

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
                                        Destroy(summoneffect, 3); // 이펙트는 3초뒤 삭제
                                        Sound_Manager.instance.EffectPlay(SummonSound);

                                        Outline charliner = tower1.AddComponent<Outline>(); //만든 타워에 외각선 추가
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
                                    print("렌더러 없음");
                                }

                            }
                        }
                        break;

                    case PlayerMode.TowerSell:


                        break;
                    //타워 CombineMode.
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

                                        // 타워 정보 스크립트 가져오기
                                        Twr_0Base towerInfo = hitObject.GetComponent<Twr_0Base>();
                                        
                                        if (towerInfo != null)
                                        {
                                            // 랭크 정보 가져오기
                                            int towerRank = towerInfo.towerRank;

                                            // 배열에 오브젝트 저장
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
                                                    // 타워 랭크 비교
                                                    if (clickedObjects[0].GetComponent<Twr_0Base>().towerRank == clickedObjects[1].GetComponent<Twr_0Base>().towerRank)
                                                    {
                                                        // 새로운 타워 생성
                                                        print("랭크2 또는 랭크3의 타워가 생성되었습니다");

                                                        Destroy(clickedObjects[0]);
                                                        Destroy(clickedObjects[1]);

                                                        clickedObjects[0] = null;
                                                        clickedObjects[1] = null;

                                                        SpawnNewTower(clickedObjects[0].GetComponent<Twr_0Base>().towerRank + 1);

                                                    }
                                                    else
                                                    {
                                                        // 타워 합성 실패
                                                        print("합성 할 수 없는 대상 입니다.");

                                                        clickedObjects[0] = null;
                                                        clickedObjects[1] = null;
                                                    }
                                                }
                                                else
                                                {
                                                    // 동일 타워 2번 선택
                                                    print("다른 타워를 선택해 주십시오.");
                                                    clickedObjects[1] = null;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            print("타워 정보 스크립트가 존재하지 않습니다.");
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
            Ui_Manager.instance.state.text = "건설 모드";
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
            Ui_Manager.instance.state.text = "판매 모드";
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
            Ui_Manager.instance.state.text = "합성 모드";
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

    public Twr_TestTower component; // 원하는 컴포넌트 타입으로 대체해야 합니다.        
    public int newTowerRank;
    public GameObject newTower;
    void SpawnNewTower(int newRank)
    {
        if (newTower != null)
        {
            // 원하는 컴포넌트를 가진 모든 오브젝트 찾기
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
