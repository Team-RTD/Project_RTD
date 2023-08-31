using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCombine : MonoBehaviour
{
    public Camera cam;    
    public RaycastHit hit;

    public GameObject[] clickedObjects; // 클릭한 오브젝트들을 저장할 배열
    private int currentIndex = 0; // 저장한 오브젝트 초기화에 사용하는 인수.

    public void Start()
    {
        
        //저장할 빈 배열 선언
        clickedObjects = new GameObject[2];
    }

    private void Update()
    {
        //W키를 눌렀을 경우 활성화
        if (Input.GetKeyDown(KeyCode.W))
        {
            //레이 선언
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Tower") 
                {
                    clickedObjects[0]= hit.collider.gameObject;
                }
                else
                {
                    print("잘못된 오브젝트를 선택 했습니다");
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Tower")
                {
                    clickedObjects[1] = hit.collider.gameObject;
                }
                else
                {
                    print("잘못된 오브젝트를 선택 했습니다");
                }
            }
        }

        if (clickedObjects[0] == clickedObjects[1]&& gameObject.tag!="Tower3")
        {
            
            Destroy(gameObject);
            //오브젝트 생성
        }
        else
        {
            print("해당 오브젝트는 더 이상 강화할 수 없습니다");
        }
    }

    
}
