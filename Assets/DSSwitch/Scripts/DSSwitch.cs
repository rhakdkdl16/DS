using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DSSwitch : MonoBehaviour
{    
    public bool isOn;                 // 스위치 on/off 상태
    [Range(0,3)]
    public float moveDuration = 3f;   // 스위치이동시간

    const float totalHandleMoveLength = 72f;  
    const float halfMoveLength = totalHandleMoveLength / 2;

    Image handleImage;                      //스위치 핸들 이미지
    Image backgroundImage;                  //스위치 배경 이미지
    RectTransform handleRectTransform;     // 스위치 핸들 RectTransform

    void Start()
    {
        //핸들초기화
        GameObject handleObject = transform.Find("Handle").gameObject;
        handleRectTransform = handleObject.GetComponent<RectTransform>();
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
        //여기서 1번
        StartCoroutine(moveHandle(fromPosition,toPosition,duration));
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
    //2.  터치시 핸들의 배경 색상을 바꿔주는 동작(함수)
}
