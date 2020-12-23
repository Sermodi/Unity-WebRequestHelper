using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Models;

public class WebRequestHelper
{

    protected static WebRequestHelper self;

    protected string BASE_URL= "https://api.sermodi.opalstacked.com/rest/";

    public bool isNetworkError;
    public DownloadHandler downloadHandler; 

    public static WebRequestHelper getInstance(){
        if (self == null)
        {
            self = this;
        }
        return self;
    }

    public string getBaseUrl(){
        return BASE_URL;
    }
    public void setBaseUrl(string url){
        BASE_URL = url;
    }

    public IEnumerator get(string addUri = "", Dictionary<string, string> headers = null ){
        string url = BASE_URL + addUri;
        var request = UnityWebRequest.Get(url);
        #region addHeaders
        if(headers != null){
            var keys = header.Keys;
            foreach (string k in keys)
            {
                request.SetRequestHeader(k, headers[k])    
            }
        }
        #endregion
        yield return request.SendWebRequest();
        isNetworkError = request.isNetworkError;
        error = request.error;
        downloadHandler = request.downloadHandler;
    }

    public IEnumerator post (T serializable_model, string addUri = "", Dictionary<string, sting> headers = null){
        string url = BASE_URL + addUri;

        var request = new UnityWebRequest(url, "POST");
        #region body (json)
        string json = JsonUtility.ToJson(serializable_model);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        #endregion
        #region addHeaders
        if(headers != null){
            var keys = header.Keys;
            foreach (string k in keys)
            {
                request.SetRequestHeader(k, headers[k])    
            }
        }
        #endregion

        //Send the request then wait here until it returns
        yield return request.SendWebRequest();
        isNetworkError = request.isNetworkError;
        error = request.error;
        downloadHandler = request.downloadHandler;
    }

}
