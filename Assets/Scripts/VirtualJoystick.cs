using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image backgroundimage;
    private Image controllerimage;
    private Vector2 touchposition;

    private void Awake()
    {
        backgroundimage = GetComponent<Image>();
        controllerimage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchposition = Vector2.zero;
        // 조이스틱의 위치와 무관하게 동일한 값을 연산하기 위함
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            backgroundimage.rectTransform, eventData.position, eventData.pressEventCamera, out touchposition))
        {
            // touchposition 값의 정규화
            touchposition.x = (touchposition.x / backgroundimage.rectTransform.sizeDelta.x);
            touchposition.y = (touchposition.y / backgroundimage.rectTransform.sizeDelta.y);

            // 좌, 중, 우 를 -1, 0, 1, 하, 중, 상 을 -1, 0, 1 로 바꾸기 위해 정규화
            touchposition = new Vector2(touchposition.x * 2 - 1, touchposition.y * 2 - 1);

            // 조이스틱이 배경 이미지 밖으로 터치가 나가면 -1~1보다 큰 값이 나올 수 있으므로
            // normalized를 이용하여 정규화
            touchposition = (touchposition.magnitude > 1) ? touchposition.normalized : touchposition;

            // 조이스틱 컨트롤러 이동
            controllerimage.rectTransform.anchoredPosition = new Vector2(
                touchposition.x * backgroundimage.rectTransform.sizeDelta.x / 2,
                touchposition.y * backgroundimage.rectTransform.sizeDelta.y / 2);

            Debug.Log("Touch & Drag : " + eventData);
        }
        //throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Touch Begin : " + eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controllerimage.rectTransform.anchoredPosition = Vector2.zero;
        // 컨트롤러 위치 초기화
        touchposition = Vector2.zero; // 이동방향 초기화
        //throw new System.NotImplementedException();
        Debug.Log("Touch End : " + eventData);
    }

    public float Horizontal()
    {
        return touchposition.x;
    }

    public float Vertical()
    {
        return touchposition.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
