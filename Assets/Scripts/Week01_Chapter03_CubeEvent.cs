using UnityEngine;
using UnityEngine.EventSystems;

public class Week01_Chapter03_CubeEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // マウスクリックイベント
        Debug.Log($"オブジェクト {this.name} がクリックされたよ！");
    }
}