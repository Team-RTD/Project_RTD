using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCombine : MonoBehaviour
{
    public Camera cam;    
    public RaycastHit hit;

    public GameObject[] clickedObjects; // Ŭ���� ������Ʈ���� ������ �迭
    private int currentIndex = 0; // ������ ������Ʈ �ʱ�ȭ�� ����ϴ� �μ�.

    public void Start()
    {
        
        //������ �� �迭 ����
        clickedObjects = new GameObject[2];
    }

    private void Update()
    {
        //WŰ�� ������ ��� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.W))
        {
            //���� ����
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Tower") 
                {
                    clickedObjects[0]= hit.collider.gameObject;
                }
                else
                {
                    print("�߸��� ������Ʈ�� ���� �߽��ϴ�");
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
                    print("�߸��� ������Ʈ�� ���� �߽��ϴ�");
                }
            }
        }

        if (clickedObjects[0] == clickedObjects[1]&& gameObject.tag!="Tower3")
        {
            
            Destroy(gameObject);
            //������Ʈ ����
        }
        else
        {
            print("�ش� ������Ʈ�� �� �̻� ��ȭ�� �� �����ϴ�");
        }
    }

    
}
