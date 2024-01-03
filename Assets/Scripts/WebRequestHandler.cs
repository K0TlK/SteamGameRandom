using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using StarterPack;

public class WebRequestHandler : Singleton<WebRequestHandler>
{
    public void MakeRequest(UnityWebRequest webRequest,
        Action<string> OnComplete,
        Action<float> OnProgress = null,
        Action<string> OnError = null)
    {
        StartCoroutine(PerformRequest(webRequest, OnComplete, OnProgress, OnError));
    }

    public void MakeRequest<T>(UnityWebRequest webRequest,
        Action<T> onComplete,
        Action<float> OnProgress = null,
        Action<string> OnError = null)
    {
        StartCoroutine(PerformRequest<T>(webRequest, onComplete, OnProgress, OnError));
    }

    public void MakeRequestSimple<T>(UnityWebRequest webRequest, Action<T> onComplete)
    {
        StartCoroutine(PerformRequestSimple<T>(webRequest, onComplete));
    }

    private IEnumerator PerformRequest(UnityWebRequest webRequest,
        Action<string> OnComplete,
        Action<float> OnProgress = null,
        Action<string> OnError = null)
    {
        var asyncOperation = webRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            OnProgress?.Invoke(asyncOperation.progress);
            yield return new WaitForSeconds(0.1f);
        }

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            if (OnError == null)
            {
                Debug.LogError("UnityWebRequest error: " + webRequest.responseCode + " " + webRequest.result);
            }
            else
            {
                OnError.Invoke(webRequest.responseCode.ToString());
            }
        }
        else
        {
            OnComplete?.Invoke(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PerformRequest<T>(UnityWebRequest webRequest, 
        Action<T> onComplete,
        Action<float> OnProgress = null,
        Action<string> OnError = null)
    {
        var asyncOperation = webRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            OnProgress?.Invoke(asyncOperation.progress);
            yield return new WaitForSeconds(0.1f);
        }

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            if (OnError == null)
            {
                Debug.LogError("UnityWebRequest error: " + webRequest.responseCode + " " + webRequest.result);
            }
            else
            {
                OnError.Invoke(webRequest.responseCode.ToString());
            }
        }
        else
        {
            try
            {
                T responseData = JsonConvert.DeserializeObject<T>(webRequest.downloadHandler.text);
                onComplete?.Invoke(responseData);
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Deserialization error: {ex.Message}");
            }
        }
    }

    private IEnumerator PerformRequestSimple<T>(UnityWebRequest webRequest, Action<T> onComplete)
    {
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("UnityWebRequest error: " + webRequest.responseCode + " " + webRequest.result);
        }
        else
        {
            try
            {
                T responseData = JsonConvert.DeserializeObject<T>(webRequest.downloadHandler.text);
                onComplete?.Invoke(responseData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Deserialization error: {ex.Message}");
            }
        }
    }
}
