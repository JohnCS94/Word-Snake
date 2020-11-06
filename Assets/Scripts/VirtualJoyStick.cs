using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image totalArea;
    private Image bgImg;
    private Image joystickImage;
    private Vector3 inputVector;
    private Vector3 inputVector2;
    private Vector2 joyOrigin = -Vector2.one;

    private void Start()
    {
        totalArea = GetComponent<Image>();
        bgImg = transform.GetChild(0).GetComponent<Image>();
        joystickImage = bgImg.transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick Image
            joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));
            
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (!bgImg.gameObject.activeInHierarchy)
        {
            bgImg.gameObject.SetActive(true);
        }
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(totalArea.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / totalArea.rectTransform.sizeDelta.x);
            pos.y = (pos.y / totalArea.rectTransform.sizeDelta.y);

            inputVector2 = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector2 = (inputVector2.magnitude > 1.0f) ? inputVector2.normalized : inputVector2;

            bgImg.rectTransform.position = new Vector2(inputVector2.x * 7, (inputVector2.y * 12)-1);
            
        }
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
        bgImg.gameObject.SetActive(false);
    }

    public bool up()
    {
        if (inputVector.y > 0 && inputVector.y > Math.Abs(inputVector.x))
        {
            return true;
        }
        else
            return false;
    }
    public bool down()
    {
        if (inputVector.y < 0 && Math.Abs(inputVector.y) > Math.Abs(inputVector.x))
            return true;
        else
            return false;
    }
    public bool left()
    {
        if (inputVector.x < 0 && Math.Abs(inputVector.x) > Math.Abs(inputVector.y))
            return true;
        else
            return false;
    }
    public bool right()
    {
        if (inputVector.x > 0 && inputVector.x > Math.Abs(inputVector.y))
            return true;
        else
            return false;
    }
    void Update()
    {
        if (Time.timeScale == 1) 
        {
            totalArea.raycastTarget = true;
        }
        else if (Time.timeScale == 0)
        {
            totalArea.raycastTarget = false;
        }
    }
}
