using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    private RectTransform _transform;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(target.position);
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform, screenPoint, Camera.main, out uiPos);
        _transform.localPosition = uiPos;
    }
}
