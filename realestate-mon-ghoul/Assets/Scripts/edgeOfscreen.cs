using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeOfscreen : MonoBehaviour
{
    public float colDepth = 3f;
    private Vector2 screenSize;
    private Transform topCollider;
    private Transform bottomCollider;
    private Transform rightCollider;
    private Transform leftCollider;
    public Vector3 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        topCollider = new GameObject().transform;
        bottomCollider = new GameObject().transform;
        rightCollider = new GameObject().transform;
        leftCollider = new GameObject().transform;

        topCollider.name = "topCOLLIDER";
        bottomCollider.name = "bottomCOLLIDER";
        rightCollider.name = "rightCOLLIDER";
        leftCollider.name = "leftCOLLIDER";

        topCollider.gameObject.AddComponent<BoxCollider2D>();
        bottomCollider.gameObject.AddComponent<BoxCollider2D>();
        rightCollider.gameObject.AddComponent<BoxCollider2D>();
        leftCollider.gameObject.AddComponent<BoxCollider2D>();

        topCollider.parent = transform;
        bottomCollider.parent = transform;
        rightCollider.parent = transform;
        leftCollider.parent = transform;

        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f,0f)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,0f))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f,0f)), Camera.main.ScreenToWorldPoint(new Vector2(0f,Screen.height))) * 0.5f;

        rightCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        rightCollider.position = new Vector3(cameraPos.x + screenSize.x + (rightCollider.localScale.x * 0.5f), cameraPos.y, 0f );

        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, 0f );

        topCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        topCollider.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topCollider.localScale.y * 0.5f), 0f );

        bottomCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomCollider.localScale.y * 0.5f), 0f );


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
