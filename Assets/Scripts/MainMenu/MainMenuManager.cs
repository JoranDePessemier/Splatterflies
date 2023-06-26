using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum MenuState
{
    Main,
    About,
    StartGame,
    GameOverEndless,
    GameOverStory,
    GameComplete
}


public class MainMenuManager : MonoBehaviour
{
    private GameObject _camObject;

    [SerializeField]
    private Vector3 _camPositionUp;

    [SerializeField]
    private Vector3 _camPositionDown;

    [SerializeField]
    private Vector3 _camPositionMiddle;

    [SerializeField]
    private float _movementTime;

    [SerializeField]
    private UnityEvent _moveDown;

    [SerializeField]
    private UnityEvent _moveUp;

    [SerializeField]
    private string _endlessGameScene;

    [SerializeField]
    private string _storyGameScene;

    [SerializeField]
    private float _musicFadeSpeed;

    public MenuState State { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1f;

        _camObject = this.gameObject;
        transform.position = _camPositionMiddle;

        if(!(GlobalVariables.Instance.GameComplete || GlobalVariables.Instance.GameOver))
        {
            LeanTween.moveLocal(_camObject, _camPositionUp, _movementTime / 2).setEase(LeanTweenType.easeOutCubic);

            MusicManager.Instance.StartFadeIn("Neutral", _musicFadeSpeed);
            MusicManager.Instance.StartFadeIn("GardenBody", _musicFadeSpeed);
        }
        else
        {
            LeanTween.moveLocal(_camObject, _camPositionDown, _movementTime / 2).setEase(LeanTweenType.easeOutCubic);
            if (GlobalVariables.Instance.GameComplete)
            {
                State = MenuState.GameComplete;
            }
            else if(GlobalVariables.Instance.GameOver)
            {
                if(GlobalVariables.Instance.EndlessMode)
                {
                    State = MenuState.GameOverEndless;
                }
                else
                {
                    State = MenuState.GameOverStory;
                }
            }
        }
        
    }

    public void StartGame(bool _isEndless)
    {
        State = MenuState.StartGame;
        LeanTween.cancel(_camObject);
        LeanTween.moveLocal(_camObject, _camPositionMiddle, _movementTime / 2).setEase(LeanTweenType.easeInCubic).setOnComplete(() => GoToGameScene(_isEndless));
        MusicManager.Instance.StartFadeIn("GardenBody", _musicFadeSpeed);
        MusicManager.Instance.StartFadeIn("GardenMelody", _musicFadeSpeed);
        MusicManager.Instance.StartFadeOut("DungeonBody", _musicFadeSpeed);
        MusicManager.Instance.StartFadeIn("Neutral", _musicFadeSpeed);
    }

    public void GoToLink(string link)
    {
        Application.OpenURL(link);
    }

    private void GoToGameScene(bool isEndless)
    {
        GlobalVariables.Instance.ScreenState = ScreenType.Transition;
        GlobalVariables.Instance.CompletedOrders = 0;
        GlobalVariables.Instance.CurrentTime = 100;
        GlobalVariables.Instance.GameOver = false;
        GlobalVariables.Instance.GameComplete = false;
        GlobalVariables.Instance.TimerStarted = true;



        if (isEndless)
        {
            GlobalVariables.Instance.EndlessMode = true;
            SceneManager.LoadScene(_endlessGameScene);

        }
        else
        {
            GlobalVariables.Instance.EndlessMode = false;
            SceneManager.LoadScene(_storyGameScene);
        }
    }

    public void GoToMainMenu()
    {
        LeanTween.cancel(_camObject);
        LeanTween.moveLocal(_camObject, _camPositionUp, _movementTime).setEase(LeanTweenType.easeInOutCubic);
        State = MenuState.Main;

        MusicManager.Instance.StartFadeIn("GardenBody", _musicFadeSpeed);
        MusicManager.Instance.StartFadeOut("DungeonBody", _musicFadeSpeed);
        MusicManager.Instance.StartFadeIn("Neutral", _musicFadeSpeed);

    }

    public void GoToAbout()
    {
        LeanTween.cancel(_camObject);
        LeanTween.moveLocal(_camObject, _camPositionDown, _movementTime).setEase(LeanTweenType.easeInOutCubic);
        State = MenuState.About;
        MusicManager.Instance.StartFadeIn("DungeonBody", _musicFadeSpeed);
        MusicManager.Instance.StartFadeOut("GardenBody", _musicFadeSpeed);
    }
}
