using Firebase.Extensions;
using Google;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoogleSignInManager : MonoBehaviour
{
    [SerializeField] string google_webclient_ID = "";
    [SerializeField] Text status;
    /*[SerializeField] TMP_Text googleSignInText;
    [Space(20)]
    [SerializeField] TMP_InputField id;
    [SerializeField] TMP_InputField token;
    [SerializeField] TMP_InputField name_;
    [SerializeField] TMP_InputField mail;
    [SerializeField] TMP_InputField profileUrl;
    [SerializeField] RawImage profileImage;
    [SerializeField] TMP_InputField authCode;
    [SerializeField] Button googleSiginInButton;
    [SerializeField] Button googleSignOutButton;*/

#if UNITY_STANDALONE_WIN
    private string redirectUri = "http://localhost:8080";
    private string authorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
    private string tokenEndpoint = "https://oauth2.googleapis.com/token";
    private string scope = "profile email";
#endif

    private GoogleSignInConfiguration config;

    private void Awake()
    {
       

        config = new GoogleSignInConfiguration()
        {
            RequestEmail = true,
            RequestIdToken = true,
            RequestProfile = true,
            RequestAuthCode = true,
            UseGameSignIn = false,
            WebClientId = google_webclient_ID
        };

        
    }

    private void Start()
    {
        if (status == null)
        {
            Debug.LogError("Status text is not assigned.");
        }
    }

    public void OnClick_GoogleLoginIn()
    {
#if UNITY_ANDROID
        if (GoogleSignIn.Configuration == null)
        {
            GoogleSignIn.Configuration = config;
        }

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnAuthenticationFinished);
        Debug.Log($"Google sign in configuration \t {JsonUtility.ToJson(GoogleSignIn.Configuration)}");
#endif
#if UNITY_STANDALONE_WIN
        string authorizationRequest = string.Format("{0}?response_type=code&scope={1}&redirect_uri={2}&client_id={3}",
                authorizationEndpoint, Uri.EscapeDataString(scope), Uri.EscapeDataString(redirectUri), google_webclient_ID);

        Application.OpenURL(authorizationRequest);
        StartLocalServer();
#endif
    }

    private void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
#if UNITY_ANDROID
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.Log("Got Error: " + error.Status + " " + error.Message);
                    status.text = "Got Error: " + error.Status + " " + error.Message;
                }
                else
                {
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception);
                    status.text = "Got Unexpected Exception?!?" + task.Exception;
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceled");
            status.text = "Cancelled";
        }
        else
        {
            GoogleSignInUser user = task.Result;
            status.text = $"{user.DisplayName} \t {user.Email}";
        }
#endif
    }

    IEnumerator GetTexture()
    {
        yield return null;
        /*UnityWebRequest www = UnityWebRequestTexture.GetTexture(profileUrl.text);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            profileImage.texture = myTexture;
        }*/
    }

    public void OnSignOut()
    {
#if UNITY_ANDROID
        GoogleSignIn.DefaultInstance.SignOut();
#endif
    }

    private async void StartLocalServer()
    {
#if UNITY_STANDALONE_WIN
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(redirectUri + "/");
        listener.Start();

        var context = await listener.GetContextAsync();
        var code = context.Request.QueryString.Get("code");

        listener.Stop();
#endif
    }
}
