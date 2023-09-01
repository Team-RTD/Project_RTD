using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCombine : MonoBehaviour
{
    public Camera cam;
    public RaycastHit hit;

    public GameObject[] clickedObjects; // 클릭한 오브젝트들을 저장할 배열
    private int currentIndex = 0; // 저장한 오브젝트 초기화에 사용하는 인수.

    public GameObject towerPrefab; // 생성할 타워의 프리팹

    public string towerTag2="Tower 2";

    public string towerTag3 = "Tower 3";

    public void Start()
    {
        //저장할 빈 배열 선언
        clickedObjects = new GameObject[2];
    }

    private void Update()
    {
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭한 위치에서 레이를 쏘아 오브젝트 검출
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.gameObject.tag == "Tower")
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


                        }
                        else
                        {
                            print("합성 할 수 없는 대상 입니다.");

                            clickedObjects[1] = null;
                            clickedObjects[0] = null;
                        }
                    }
                }
                if (clickedObject.gameObject.tag == "Tower 2")
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


                        }
                        else
                        {
                            print("합성 할 수 없는 대상 입니다.");

                            clickedObjects[1] = null;
                            clickedObjects[0] = null;
                        }
                    }
                }
                if (clickedObject.gameObject.tag == "Tower 3")
                {
                    print("해당 타워는 더 이상 합성 할 수 없습니다.");
                    clickedObjects[1] = null;
                    clickedObjects[0] = null;
                }
            }
        }
    }

    public void SpawnNewTowerRank2()
    {
        GameObject newTowerObject = FindObjectWithTag(towerTag2);
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }
    public void SpawnNewTowerRank3()
    {
        GameObject newTowerObject = FindObjectWithTag(towerTag3);
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }

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
}
