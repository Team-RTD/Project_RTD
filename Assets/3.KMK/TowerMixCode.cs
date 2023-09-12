using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerMixCode: MonoBehaviour
{
    public static TowerMixCode instance { get; private set; }

    public Camera cam;
    public string componentName;
    public GameObject towerzone;
    public GameObject clickedTower;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        Tower_Manager tower_Manager = Tower_Manager.instance;
        cam = Camera.main;
    }



    // Update is called once per frame
    void Mix(RaycastHit hit)
    {
        RaycastHit hit1;
        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray1, out hit1))
        {
            GameObject clickedTower = hit1.collider.gameObject;
            if (clickedTower != null)
            {
                Component[] components = clickedTower.GetComponents<Component>();
                foreach (Component component in components)
                {
                    if (component is Twr_0Base yourComponent)
                    {
                        // 컴포넌트 안에 int towerRank 값이 있는지 확인
                        if (yourComponent.GetType().GetField("towerRank") != null)
                        {
                            // int towerRank 값이 있는 경우, 원하는 작업 수행
                            int towerRank = (int)yourComponent.GetType().GetField("towerRank").GetValue(yourComponent);

                            GameObject towerZone = (GameObject)yourComponent.GetType().GetField("TowerZone").GetValue(yourComponent);



                            string listName = "Tower" + (towerRank + 1);
                            Debug.Log(listName);

                            List<GameObject> towerList = (List<GameObject>)typeof(Tower_Manager).GetField(listName).GetValue(Tower_Manager.instance);
                            GameObject towerObject = towerList.Find(GameObject => GameObject != clickedTower && GameObject.name == clickedTower.name);
                            Debug.Log(this + "이거");
                            if (towerRank < 6 && towerRank >= 0)
                            {
                                Debug.Log("지점");
                                //GameObject towerzone = (GameObject)targetObject.GetType().GetField("TowerZone").GetValue(targetObject);
                                //이제 매니저가 가진 리스트에서 같은 이름을 가진 다른 타워를 찾아야한다.

                                if (towerObject != null)
                                {
                                    Tower_Manager.instance.TowerSell(towerObject, false);
                                    Tower_Manager.instance.TowerSell(clickedTower, false);

                                    Tower_Manager.instance.TowerInstance(towerZone, towerRank + 2);
                                }
                                else
                                {
                                    print("합성이 가능한 타워가 없습니다.");
                                }
                            }
                            break; // 원하는 컴포넌트를 찾았으면 루프 종료
                        }
                    }
                }
            }
            else
            {
                print("합성 할 수 없는 타워 입니다.");
            }
        }
        //누른 오브젝트의 컴포넌트에서 랭크에 대한 정보를 찾는다
        //매니저가 가지고 있는 리스트에서 같은 랭크의 리스트를 찾고, 본인을 제외한 같은 이름을 가지는 오브젝트를 찾는다.
        //첮번째로 선택한 오브젝트의 타워존 정보를 기억한다.
        //Find함수를 쓰면 찾은 오브젝트중 처음 발견한 오브젝트를 저장한다.
        //판매함수로 오브젝트를 제거한다.
        //오브젝트 생성함수로 받은 타워존 정보를 입력해서 다음 랭크의 타워를 생성한다.
    }
}