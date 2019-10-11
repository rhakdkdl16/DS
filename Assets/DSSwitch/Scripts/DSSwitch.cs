using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DSSwitch : MonoBehaviour
{    
    public bool isOn;                 // 스위치 on/off 상태
    [Range(0,3)]
    public float moveDuration = 3f;   // 스위치이동시간
    //Color
    public Color handleColor = Color.white;
    public Color offBackGroundColor = Color.blue;
    public Color onBackGroundColor = Color.red;

    const float totalHandleMoveLength = 72f;  
    const float halfMoveLength = totalHandleMoveLength / 2;

    Image handleImage;                      //스위치 핸들 이미지
    Image backgroundImage;                  //스위치 배경 이미지
    RectTransform handleRectTransform;     // 스위치 핸들 RectTransform

    Coroutine moveHandleCoroutine;       //핸들이동 코루틴
    Coroutine changeBackgroundColorCoroutine; //배경색변경 코루틴
    void Start()
    {
        //핸들초기화
        GameObject handleObject = transform.Find("Handle").gameObject;
        handleRectTransform = handleObject.GetComponent<RectTransform>();

        //Handle Image
        handleImage = handleObject.GetComponent<Image>();
        handleImage.color = handleColor;
        //BackGround Image
        backgroundImage = GetComponent<Image>();
        backgroundImage.color = offBackGroundColor;
           
        if(isOn)
        {
            handleRectTransform.anchoredPosition = new Vector2(halfMoveLength, 0);
        }
        else
        {
            handleRectTransform.anchoredPosition = new Vector2(-halfMoveLength, 0);
        }
    }
    public void OnClickSwitch()
    {
        isOn = !isOn;

        Vector2 fromPosition = handleRectTransform.anchoredPosition;
        Vector2 toPosition = (isOn) ? new Vector2(halfMoveLength, 0) : new Vector2(-halfMoveLength, 0);
        Vector2 distance = toPosition - fromPosition;

        float ratio = Mathf.Abs( distance.x) / totalHandleMoveLength;
        float duration = moveDuration * ratio;
        //handle Move Coroutine
        if(moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
        moveHandleCoroutine = StartCoroutine(moveHandle(fromPosition,toPosition,duration));
        //BackGround Color Change Coroutine

        Color fromColor = backgroundImage.color;  
        Color toColor = (isOn) ? onBackGroundColor : offBackGroundColor;

        if(changeBackgroundColorCoroutine != null)
        {
            StopCoroutine(changeBackgroundColorCoroutine);
            changeBackgroundColorCoroutine = null;
        }
        changeBackgroundColorCoroutine = StartCoroutine(ChangeBackgroundColor(fromColor, toColor,duration));
    }
    /// <summary>
    /// 핸들을 이동하는 함수
    /// </summary>
    /// <param name="fromPosition">핸들의 위칫값</param>
    /// <param name="toPosition">핸들의 목적지 위치</param>
    /// <param name="duration">핸들이 이동하느 시간</param>
    /// <returns>없음</returns>
    IEnumerator moveHandle(Vector2 fromPosition, Vector2 toPosition, float duration)
    {       
        float currentTime = 0f;
        while(currentTime <= duration)
        {
            float t = currentTime / duration;
            Vector2 newPostion = Vector2.Lerp(fromPosition, toPosition, t);
            handleRectTransform.anchoredPosition = newPostion;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
   /// <summary>
   /// 스위치 배경색 변경함수
   /// </summary>
   /// <param name="fromColor">초기색상</param>
   /// <param name="toColor">변경할 색상</param>
   /// <param name="duration">변경시간</param>
   /// <returns>없음</returns>
    IEnumerator ChangeBackgroundColor(Color fromColor, Color toColor , float duration)
    {
        float currentTIme = 0f;
        while(currentTIme < duration)
        {
            float t = currentTIme / duration;
            Color newColor = Color.Lerp(fromColor, toColor, t);

            backgroundImage.color = newColor;

            currentTIme += Time.deltaTime;
            yield return null;
        }
    }
}
