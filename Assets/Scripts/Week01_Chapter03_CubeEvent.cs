using UnityEngine;
using UnityEngine.EventSystems;

public class Week01_Chapter03_CubeEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // �}�E�X�N���b�N�C�x���g
        Debug.Log($"�I�u�W�F�N�g {this.name} ���N���b�N���ꂽ��I");
    }
}