using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//필드에 미리 생성할 오브젝트가 존재해야만 새로운 타워를 합성할 수 있다.
//필드에 타워가 없으면 생성할 대상을 찾지 못해서 기존의 타워만 사라진다.
//씬 어딘가 안보이는곳에 타워를 넣어 두기만 하면 잘 작동할 것이다.
public class TowerCombine : MonoBehaviour
{
    //컴포넌트를 무조건 카메라에 넣어둘 것.
    public Camera cam;
    public RaycastHit hit;

    public GameObject[] clickedObjects; // 클릭한 오브젝트들을 저장할 배열
    private int currentIndex = 0; // 저장한 오브젝트 초기화에 사용하는 인수.

    public GameObject towerPrefab; // 생성할 타워의 프리팹

    public string towerTag2 = "Tower_2";

    public string towerTag3 = "Tower_3";

    public bool combineProsses = false;

    public void Start()
    {
        //저장할 빈 배열 선언
        clickedObjects = new GameObject[2];
    }

    private void Update()
    {
        if (combineProsses == false)
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                Combine();
            }
        }
    }

    private void Combine()
    {        
        print("확인");
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            combineProsses = true;
            // 마우스 클릭한 위치에서 레이를 쏘아 오브젝트 검출
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //2랭크의 타워 합성 시퀸스
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.gameObject.tag == "Tower_1")
                {
                    // 배열에 오브젝트 저장
                    clickedObjects[currentIndex] = clickedObject;

                    currentIndex++; // 다음 인덱스로 이동
                                    //같은 타워를 선택했는지 확인
                    if (clickedObjects[0] != clickedObjects[1])
                    {
                        if (currentIndex >= clickedObjects.Length)
                        {
                            currentIndex = 0; // 인덱스가 배열 크기를 넘어가면 처음부터 다시 저장
                        }
                        if (clickedObjects[1] != null)
                        {
                            // 타워를 합성해서 다음 등급의 타워를 생성해야 한다.
                            if (clickedObjects[0].tag == clickedObjects[1].tag)
                            {
                                // 새로운 타워 생성
                                SpawnNewTowerRank2();
                                print("랭크2의 타워가 생성되었습니다");

                                Destroy(clickedObjects[0].gameObject);
                                Destroy(clickedObjects[1].gameObject);

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;
                                combineProsses = false;
                            }
                            else
                            {
                                //타워 합성에 실패 했을 경우 출력.
                                print("합성 할 수 없는 대상 입니다.");

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;
                                combineProsses = false;
                            }
                        }
                    }
                    else
                    {
                        //동일 타워 2번 선택시 출력.
                        print("다른 타워를 선택해 주십시오.");
                        clickedObjects[1] = null;
                        combineProsses = false;
                    }
                }

                //랭크3 타워 합성 시퀸스
                else if (clickedObject.gameObject.tag == "Tower_2")
                {
                    // 배열에 오브젝트 저장
                    clickedObjects[currentIndex] = clickedObject;

                    currentIndex++; // 다음 인덱스로 이동

                    if (currentIndex >= clickedObjects.Length)
                    {
                        currentIndex = 0; // 인덱스가 배열 크기를 넘어가면 처음부터 다시 저장
                    }
                    if (clickedObjects[1] != null)
                    {
                        //같은 타워를 선택했는지 확인
                        if (clickedObjects[0] != clickedObjects[1])
                        {
                            // 타워를 합성해서 다음 등급의 타워를 생성해야 한다.
                            if (clickedObjects[0].tag == clickedObjects[1].tag)
                            {
                                // 새로운 타워 생성
                                SpawnNewTowerRank3();
                                print("랭크3의 타워가 생성되었습니다");

                                Destroy(clickedObjects[0].gameObject);
                                Destroy(clickedObjects[1].gameObject);

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;

                                combineProsses = false;
                            }
                            else
                            {
                                //타워 합성에 실패 했을 경우 출력.
                                print("합성 할 수 없는 대상 입니다.");

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;

                                combineProsses = false;
                            }
                        }
                        else
                        {
                            //동일 타워 2번 선택시 출력.
                            print("다른 타워를 선택해 주십시오.");
                            clickedObjects[1] = null;
                        }
                    }
                }
                else if (clickedObject.gameObject.tag == "Tower_3")
                {
                    //3티어 타워 선택시 출력.
                    print("해당 타워는 더 이상 합성 할 수 없습니다.");
                    clickedObjects[1] = null;
                    clickedObjects[0] = null;
                }
                else
                {
                    print("타워를 선택해 주십시요");
                }
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                combineProsses = false;
                print("반환합니다.");
                return;
            }
        }

        print("밖입니다");
        combineProsses = false;
    }

    //1티어 타워로 2티어 타워를 합성할 경우 실행.
    public void SpawnNewTowerRank2()
    {
        GameObject newTowerObject = FindObjectWithTag("Tower_2");
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }
    //2티어 타워로 3티어 타워를 합성할 경우 실행.
    public void SpawnNewTowerRank3()
    {
        GameObject newTowerObject = FindObjectWithTag("Tower_3");
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }
    //추가로 티어를 추가해 더 높은 티어의 타워의 합성이 필요할 경우 복사해서 함수 이름만 바꿔서 작성.



    //Do not change--------------
    // 특정 태그를 가지는 오브젝트 찾기
    GameObject FindObjectWithTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        if (taggedObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, taggedObjects.Length);
            return taggedObjects[randomIndex];
        }
        return null;
    }
    //--------------------------
}
