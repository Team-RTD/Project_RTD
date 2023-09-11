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

    private List<GameObject> foundObjects = new List<GameObject>();
    private MixTowerManager mixTowerManager;
    public int towerRank;

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

                                        int random = Random.Range(0, Tower_Manager.instance.Towers.Length);

                                        // GameObject tower1 = Instantiate(test, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        GameObject tower1 = Instantiate(Tower_Manager.instance.Towers[random], hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        t_zone.Tower = tower1; //타워존에 타워 오브젝트 할당
                                        tower1.GetComponent<Twr_0Base>().TowerZone = t_zone.gameObject;
                                        GameObject summoneffect = Instantiate(SummonEffect, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        Destroy(summoneffect, 3); // 이펙트는 3초뒤 삭제
                                        Sound_Manager.instance.EffectPlay(SummonSound);

                                        //업그래이드 매니저 호출-------------------
                                        //UpGrade_Manager.Instance.AddTowerToList(tower1);
                                        //-------------------


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

                    case PlayerMode.TowerMix:

                        RaycastHit hit1;
                        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray1, out hit1))
                        {

                            GameObject towerObject = hit1.collider.gameObject;

                            Component[] clickedObject = towerObject.GetComponents<Component>();

                            foreach (Component component in clickedObject)
                            {
                                if (component.GetType().GetField("towerRank") != null)
                                {
                                    //가져온 랭크를 int 값으로 변환하는 코드, 나중에 타워를 생성할 때 참고함.
                                    int towerRank = (int)component.GetType().GetField("towerRank").GetValue(component);

                                    //타워의 등급을 최대 3단계라고 봤을때 3단계의 타워는 합성이 불가능하게 설정.
                                    if (towerRank != 3)
                                    {
                                        //이미 클릭한 오브젝트가 있을 경우.
                                        if (foundObjects != null)
                                        {
                                            if (foundObjects.Count < 2)
                                            {
                                                //클릭한 데이터가 이미 저장되어 있는 정보가 아니라면 이행.
                                                if (!foundObjects.Contains(towerObject))
                                                {
                                                    foundObjects.Add(towerObject);
                                                }
                                                else
                                                {
                                                    print("다른 타워를 선택해야 합니다");
                                                    foundObjects.Clear();
                                                }
                                            }
                                            // 이미 선택된 오브젝트가 있을 경우, 두 오브젝트를 비교하여 조건에 맞는지 확인합니다.
                                            if (foundObjects[0] == foundObjects[1])
                                            {
                                                //타워 합성 실시
                                                TowerMixFountion();
                                                foundObjects.Clear();
                                            }
                                            else
                                            {
                                                print("같은 종류의 타워만 합성이 가능합니다.");
                                                foundObjects.Clear();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        print("해당 타워는 합성이 불가능 합니다.");
                                        clickedObject = new Component[0];

                                    }

                                    break;
                                }
                            }


                        }


                        break;
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
            Ui_Manager.instance.state.text = "건설 모드";
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

    public void TowerMixFountion()
    {
        // 타워 합성 로직 구현
        GameObject tower1 = foundObjects[0];
        GameObject tower2 = foundObjects[1];

        if (towerRank == 1)
        {
            List<GameObject> rank2tower = mixTowerManager.rank2Tower;

            int randomeIndex = Random.Range(0, rank2tower.Count);
            GameObject instantiateTower = rank2tower[randomeIndex];

            Vector3 spwan = foundObjects[1].transform.position;
            GameObject newTower = Instantiate(instantiateTower, spwan, Quaternion.identity);

            Destroy(foundObjects[0]);
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }
        else if (towerRank == 2)
        {
            List<GameObject> rank3tower = mixTowerManager.rank3Tower;

            int randomeIndex = Random.Range(0, rank3tower.Count);
            GameObject instantiateTower = rank3tower[randomeIndex];

            Vector3 spwan = foundObjects[1].transform.position;
            GameObject newTower = Instantiate(instantiateTower, spwan, Quaternion.identity);

            Destroy(foundObjects[0]);
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }

    }
}
