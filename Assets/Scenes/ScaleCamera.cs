using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCamera : MonoBehaviour
{
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;

        gameObject.transform.localScale = Vector3.one * cameraHeight / 0.45f;
        gameObject.transform.localPosition = new Vector3(Screen.width / 2.45f, Screen.height / 4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
