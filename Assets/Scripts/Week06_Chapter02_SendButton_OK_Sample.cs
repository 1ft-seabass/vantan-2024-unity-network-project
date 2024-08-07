using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;       // IEnumerator �̂��߂̎Q��
using UnityEngine.Networking;   // UnityWebRequest �̂��߂̎Q��
using System;                   // Serializable �̂��߂̎Q��
using System.Text;              // Encoding �̂��߂̎Q��
using UnityEngine.UI;           // InputField �̂��߂̎Q��

public class Week06_Chapter02_SendButton_OK_Sample : MonoBehaviour, IPointerClickHandler
{

    // ���M���� Unity �f�[�^�� JSON �f�[�^������ PointRequestData �x�[�X�N���X
    [Serializable]
    public class PointRequestData
    {
        // point �Ƃ����v���p�e�B���� int �^�ŕϊ�
        public int point;
        // name �Ƃ����v���p�e�B���� int �^�ŕϊ�
        public string name;
    }

    // �A�N�Z�X���� URL
    // �T�[�o�[URL + /pointlist �ŃA�N�Z�X
    string urlGitHub = "https://legendary-broccoli-qvg9v4r495f4vxq-1880.app.github.dev/pointlist";

    public void OnPointerClick(PointerEventData eventData)
    {
        // HTTP ���N�G�X�g��񓯊�������҂��߃R���[�`���Ƃ��ČĂяo��
        StartCoroutine("PostPointData");
    }

    // POST ���N�G�X�g����{��
    IEnumerator PostPointData()
    {
        // HTTP ���N�G�X�g����(POST ���\�b�h) UnityWebRequest ���Ăяo��
        // �A�N�Z�X�����͕ϐ� urlGitHub �Őݒ�
        UnityWebRequest request = new UnityWebRequest(urlGitHub, "POST");

        // PointRequestData �x�[�X�N���X����Ƃ��ČĂяo��
        PointRequestData pointRequestData = new PointRequestData();
        // �f�[�^��ݒ�
        // ���݂̃|�C���g�𓾂�
        pointRequestData.point = GameObject.Find("ClickPart").GetComponent<Week06_Chapter02_ClickPart>().currentPoint;
        // �����̖��O��o�^
        // �Œ�l�łȂ� InputField ����擾����
        pointRequestData.name = "Seigo";

        // ���M�f�[�^�� JsonUtility.ToJson �� JSON ��������쐬
        // pointRequestData �̍\���Ɋ�Â��ĕϊ����Ă����
        string strJSON = JsonUtility.ToJson(pointRequestData);
        Debug.Log($"strJSON : {strJSON}");
        // ���M�f�[�^�� Encoding.UTF8.GetBytes �� byte �f�[�^��
        byte[] bodyRaw = Encoding.UTF8.GetBytes(strJSON);

        // �A�b�v���[�h�iUnity���T�[�o�j�̃n���h�����쐬
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        // �_�E�����[�h�i�T�[�o��Unity�j�̃n���h�����쐬
        request.downloadHandler = new DownloadHandlerBuffer();

        // JSON �ő���� HTTP �w�b�_�[�Ő錾����
        request.SetRequestHeader("Content-Type", "application/json");

        // ���N�G�X�g�J�n
        yield return request.SendWebRequest();
        Debug.Log("���N�G�X�g�J�n");

        // ���ʂɂ���ĕ���
        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("���N�G�X�g��");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("���N�G�X�g����");

                // ���M�����烊�Z�b�g
                GameObject.Find("ClickPart").GetComponent<Week06_Chapter02_ClickPart>().ResetPoint();

                // �R���\�[���ɕ\��
                Debug.Log($"responseData: {request.downloadHandler.text}");

                // RankingMessage �ōŐV�f�[�^���擾
                // GetDataCore �𓮂���
                GameObject.Find("RankingMessage").GetComponent<Week06_Chapter02_RankingMessage_OK_Sample>().GetDataCore();


                break;
        }

    }
}