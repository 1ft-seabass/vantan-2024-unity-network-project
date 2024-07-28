using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;       // IEnumerator �̂��߂̎Q��
using UnityEngine.Networking;   // UnityWebRequest �̂��߂̎Q��
using System;                   // Serializable �̂��߂̎Q��

using System.Collections.Generic; // List �̂��߂̎Q��

public class Week05_Chapter02_CubeEvent01 : MonoBehaviour, IPointerClickHandler
{
    // API �̐ڑ���
    // ����� �T�[�o�[URL + /api/get ��ǂݍ���
    string urlAPI = "https://super-duper-potato-jrw4r76996fq44x-1880.app.github.dev/sample01";

    // ��M���� JSON �f�[�^�� Unity �ň����f�[�^�ɂ��� ResponseData �x�[�X�N���X
    // ���ɂ� ResponseDataItem �ō\������Ă��܂�
    //
    //{
    //  "data": [
    //    {
    //      "id": "33d3",
    //      "title": "A"
    //    },
    //    {
    //      "id": "230d",
    //      "title": "B"
    //    },
    //    {
    //      "id": "93b3",
    //      "title": "C"
    //    },
    //    {
    //      "id": "4fe7",
    //      "title": "D"
    //    }
    //  ]
    //}
    // �Ƃ��� data �̃I�u�W�F�N�g�̒��ɔz�񂪓����Ă��܂�
    [Serializable]
    public class ResponseData
    {
        public List<ResponseDataItem> data;
    }

    // ResponseDataItem �� data �̒��� id, title �̃��X�g�Ŏ擾�ł���悤�\�������܂�
    [Serializable]
    public class ResponseDataItem
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // �}�E�X�N���b�N�C�x���g

        // HTTP ���N�G�X�g��񓯊�������҂��߃R���[�`���Ƃ��ČĂяo��
        StartCoroutine("GetData");
    }

    IEnumerator GetData()
    {
        // HTTP ���N�G�X�g����(GET ���\�b�h) UnityWebRequest ���Ăяo��
        // �A�N�Z�X�����͕ϐ� urlGitHub �Őݒ�
        UnityWebRequest request = UnityWebRequest.Get(urlAPI);

        // ���N�G�X�g�J�n
        yield return request.SendWebRequest();

        // ���ʂɂ���ĕ���
        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("���N�G�X�g��");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("���N�G�X�g����");

                // �R���\�[���ɕ\��
                Debug.Log($"responseData: {request.downloadHandler.text}");

                // ResponseData �N���X�� Unity �ň�����f�[�^��
                ResponseData response = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);


                break;
        }


    }
}