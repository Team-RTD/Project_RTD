using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMixCode : MonoBehaviour
{
    private Camera cam;
    private List<GameObject> foundObjects = new List<GameObject>();
    public int towerRank;

    public void Start()
    {
        cam = Camera.main;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject towerObject = hit.collider.gameObject;

                //레이에 닿은 모든 오브젝트를 저장한다
                Component[] clickedObject = towerObject.GetComponents<Component>();

                //저장된 오브젝트 중 랭크를 가진 타워만을 찾는다.
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
                                // 이미 선택된 오브젝트가 있을 경우, 두 오브젝트를 비교하여 동일한지 확인합니다.
                                if (foundObjects[0] == towerObject)
                                {
                                    //타워 합성 실시
                                    TowerMix();
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
        }
    }

    public void TowerMix()
    {
        // 타워 합성 로직 구현
        GameObject tower1 = foundObjects[0];
        GameObject tower2 = foundObjects[1];

        if (towerRank == 1)
        {
            //tower1.towerRrank;
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }

        else if (towerRank == 2)
        {
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }

        else if (towerRank == 3)
        {
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }

        else if (towerRank == 4)
        {
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }

        else if (towerRank == 5)
        {
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }
    }
}



