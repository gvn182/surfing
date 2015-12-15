using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour
{
    public CharacterController Player;
    public StateController GameState;

    public Texture2D YouSurfedTex;
    public Texture2D YouCollectedTex;
    public Texture2D SurfShopTex;
    public Texture2D SurfAgainTex;

    public Texture2D PauseTex;
    public Texture2D SurfMetterTex;
    public Texture2D CoinsCollectedTex;
    public Texture2D PauseBackgroundTex;
    public Texture2D ResumeTex;
    public Texture2D QuitTex;
    public Texture2D MuteFXTex;
    public Texture2D MuteSoundTex;
    public Texture2D MuteFXTexClicked;
    public Texture2D MuteSoundTexClicked;

    private Texture2D MuteFXTexInUse;
    private Texture2D MuteSoundTexInUse;
    public GUIStyle MyFontStyle;
    public GUIStyle MyPauseBackgroundStyle;
    [HideInInspector]
    public bool isGamePaused = false;
    [HideInInspector]
    public bool isSoundMuted = false;
    [HideInInspector]
    public bool isFXMuted = false;

    public GUIStyle emptyButtonStyle;

    Vector3 savedVelocity;
    float savedAngularVelocity;

    void OnGUI()
    {
      
        #region UI
        if (GUI.Button(new Rect(Screen.width - Screen.width * 0.06f, 0, Screen.width * 0.06f, Screen.width * 0.06f), PauseTex, emptyButtonStyle))
        {
            Player.Rigid.isKinematic = true;
            isGamePaused = true;
            savedVelocity = Player.Rigid.velocity;
            savedAngularVelocity = Player.Rigid.angularVelocity;
        }
        //SurfMetter

        GUI.DrawTexture(new Rect(0, Screen.width * 0.005f, Screen.width * 0.22f, Screen.height * 0.12f ), SurfMetterTex);

        //Coins collected
        GUI.Box(new Rect(Screen.width - Screen.width * 0.25f, Screen.height * 0.020f, Screen.width * 0.18f, Screen.height * 0.10f), CoinsCollectedTex, emptyButtonStyle);
        GUI.Label(new Rect(Screen.width - Screen.width * 0.20f, Screen.height * 0.055f, Screen.width * 0.1f, Screen.height * 0.1f), "500000", MyFontStyle);
        #endregion
        #region Pause
        if (isGamePaused)
        {

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", MyPauseBackgroundStyle);

            GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.width * 0.35f, Screen.height * 0.05f, Screen.width * 0.7f, Screen.height * 0.8f), PauseBackgroundTex);
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width * 0.30f, Screen.height / 2 - Screen.height * 0.025f, Screen.width * 0.15f, Screen.height * 0.3f), ResumeTex, emptyButtonStyle))
            {
                isGamePaused = false;
                Player.Rigid.isKinematic = false;
                Player.Rigid.velocity = savedVelocity;
                Player.Rigid.angularVelocity = savedAngularVelocity;
            }
            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width * 0.15f, Screen.height / 2 - Screen.height * 0.025f, Screen.width * 0.15f, Screen.height * 0.3f), QuitTex, emptyButtonStyle))
            {
              
            }
            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width * 0.05f, Screen.height / 2 + Screen.height * 0.2f, Screen.width * 0.15f, Screen.height * 0.25f), MuteFXTexInUse, emptyButtonStyle))
            {
                isFXMuted = !isFXMuted;
                if (isFXMuted)
                    MuteFXTexInUse = MuteFXTexClicked;
                else
                    MuteFXTexInUse = MuteFXTex;

            }

            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width * 0.15f, Screen.height / 2 + Screen.height * 0.2f, Screen.width * 0.15f, Screen.height * 0.25f), MuteSoundTexInUse, emptyButtonStyle))
            {
                isSoundMuted = !isSoundMuted;
                if (isSoundMuted)
                    MuteSoundTexInUse = MuteSoundTexClicked;
                else
                    MuteSoundTexInUse = MuteSoundTex;
            }

        }
        #endregion
        if (Player.dead)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", MyPauseBackgroundStyle);
            GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.width * 0.45f, Screen.height / 2 - Screen.height * 0.40f, Screen.height * 0.98f, Screen.width * 0.4f), YouSurfedTex);
            GUI.DrawTexture(new Rect(Screen.width - Screen.width * 0.38f, Screen.height / 2 - Screen.height * 0.38f, Screen.width * 0.27f, Screen.height * 0.2f), YouCollectedTex);
            if (GUI.Button(new Rect(Screen.width - Screen.width * 0.39f, Screen.height / 2 - Screen.height * 0.17f, Screen.width * 0.3f, Screen.height * 0.5f), SurfShopTex, emptyButtonStyle))
            {

            }
            if (GUI.Button(new Rect(Screen.width - Screen.width * 0.36f, Screen.height / 2 + Screen.height * 0.2f, Screen.width * 0.23f, Screen.height * 0.20f), SurfAgainTex))
            {
                Debug.Log("Clicked");
                Application.LoadLevel(Application.loadedLevelName);
            }

        }
        //GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), PauseTex);
        //GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom-left");
        //GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom-right");
    }


    // Use this for initialization
    void Start()
    {
        MuteFXTexInUse = MuteFXTex;
        MuteSoundTexInUse = MuteSoundTex;
    }

    // Update is called once per frame
    void Update()
    {

    }
}