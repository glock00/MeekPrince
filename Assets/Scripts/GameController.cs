using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject CoinPrefab;
    public GameObject DiamondPrefab;
    public Text ScoreText;
    public GameObject LoseText;
    public GameObject WinText;
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public GameObject IntroText;
    public GameObject playerPrefab;

    private bool gameEnded;
    private GameObject player;
    private int score = 0;
    private const int KILL_LIMIT = 10;
    private Platformer2DUserControl UserControl;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }        

        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Start()
    {
        player = GameObject.FindWithTag("Player");
        UserControl = player.GetComponent<Platformer2DUserControl>();
        Screen.SetResolution(1024, 768, false);

        UserControl.enabled = false;
        WinText.SetActive(false);
        LoseText.SetActive(false);
        yield return StartCoroutine(DoShowIntro());

        StartGame();
    }

    IEnumerator DoShowIntro()
    {
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
    }

    void Update()
    {
        if (score >= KILL_LIMIT)
        {
            LoseGame();
        }

        if (gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            }
        }
    }

    public void Score(int amount)
    {
        score += amount;
        UpdateScoreText();
        audioSource.PlayOneShot(pickupSound);
        player.GetComponent<Rigidbody2D>().mass += 0.03f;
    }

    void UpdateScoreText()
    {
        ScoreText.text = string.Format("Wealth: {0} / {1}", score, KILL_LIMIT);
    }

    void StartGame()
    {
        gameEnded = false;
        WinText.SetActive(false);
        LoseText.SetActive(false);
        IntroText.SetActive(false);
        player.SetActive(true);
        UserControl.enabled = true;
        score = 0;
    }

    public void WinGame()
    {
        WinText.SetActive(true);
        player.SetActive(false);
        gameEnded = true;
        UserControl.enabled = false;
    }

    public void LoseGame()
    {
        LoseText.SetActive(true);
        player.SetActive(false);
        gameEnded = true;
        UserControl.enabled = false;
    }
}
