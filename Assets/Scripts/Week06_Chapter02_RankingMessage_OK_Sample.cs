using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections.Generic; // List �̂��߂̎Q��
using System.Collections;       // IEnumerator �̂��߂̎Q��
using UnityEngine.Networking;   // UnityWebRequest �̂��߂̎Q��
using System;                   // Serializable �̂��߂̎Q��
using System.Text;              // Encoding �̂��߂̎Q��


public class Week06_Chapter02_RankingMessage_OK_Sample : MonoBehaviour
{
    // ��M���� JSON �f�[�^�� Unity �ň����f�[�^�ɂ��� ResponseData �x�[�X�N���X
    // ����� data �̒��g���z��ŁAList �̒��g���`���� ResponseDataList �^���쐬�B
    [Serializable]
    public class ResponseData
    {
        public List<ResponseDataList> data;
    }

    // name , point �̃v���p�e�B�������Ă���̂ŁAList �̒��g���`���� ResponseDataList �^���쐬
    [Serializable]
    public class ResponseDataList
    {
        public string name;
        public int point;
    }

    void Start()
    {
        GetDataCore();
    }

    public void GetDataCore()
    {
        StartCoroutine("GetData");
    }

    // �A�N�Z�X���� URL
    // �T�[�o�[URL + /pointlist �ŃA�N�Z�X
    string urlAPI = "";

    IEnumerator GetData()
    {
        // HTTP ���N�G�X�g����(GET ���\�b�h) UnityWebRequest ���Ăяo��
        // �A�N�Z�X�����͕ϐ� urlAPI �Őݒ�
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

                // �������������
                string textRankingList = "[Ranking]\n";

                Debug.Log(response.data);

                // �f�[�^��������f����
                for (int i = 0; i < response.data.Count; i++)
                {
                    ResponseDataList currentLine = response.data[i];

                    Debug.Log(currentLine);

                    // �������A��
                    textRankingList += "[" + i.ToString() + "]" + currentLine.name + " " + currentLine.point.ToString() + "pt" + "\n";
                }

                // ���b�Z�[�W�ɔ��f
                this.transform.GetComponent<TextMesh>().text = textRankingList;

                break;
        }


    }
}
