using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;           // IEnumerator �̂��߂̎Q��
using UnityEngine.Networking;       // UnityWebRequest �̂��߂̎Q��
using System;                       // Serializable �̂��߂̎Q��
using System.Collections.Generic;   // List �̂��߂̎Q��

public class Sample02_GetAPI : MonoBehaviour
{
    // �A�N�Z�X���� URL
    string urlAPI = "";

    void Start()
    {
        StartCoroutine(GetAPI(urlAPI));
    }

    // �e�N�X�`���ǂݍ���
    IEnumerator GetAPI(string url)
    {
        // �e�N�X�`���� GET ���N�G�X�g�œǂݍ��ށB�u���E�U�ł������B
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        // ���N�G�X�g�J�n
        yield return request.SendWebRequest();

        Debug.Log("���N�G�X�g�J�n");

        // ���ʂɂ���ĕ���
        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("���N�G�X�g��");
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("ConnectionError");
                Debug.Log(request.error);
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("ProtocolError");
                Debug.Log(request.responseCode);
                Debug.Log(request.error);
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("���N�G�X�g����");

                // �e�N�X�`���Ɋ��蓖��
                Texture loadedTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                GameObject.Find("Tile0").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", loadedTexture);

                break;
        }
    }
}
