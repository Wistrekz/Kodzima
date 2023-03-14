using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public GameObject Player;
    public AudioClip WinningSound;
    public Animator FinishwordAnim;
    public GameObject PausePanel, ContinueButton;
    public Text Skillstext, WinningWord, PauseWord;

    public static bool LevelComplete;

    private bool SoundPlayed;
    private bool PauseStatus, LevelRestart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Kodzima")
        {
            LevelComplete = true;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelRestart = true;
    }

    public void Pause(bool Status)
    {
        if(Status)
        {
            Time.timeScale = 0f;
            WinningWord.color = new Color(WinningWord.color.a, WinningWord.color.g, WinningWord.color.b, 0);
            PausePanel.SetActive(true);
            Skillstext.color = new Color(Skillstext.color.a, Skillstext.color.g, Skillstext.color.b, 0);
            PauseWord.text = "Пауза";
        }
        else
        {
            Time.timeScale = 1f;
            WinningWord.color = new Color(WinningWord.color.a, WinningWord.color.g, WinningWord.color.b, 1);
            PausePanel.SetActive(false);
            Skillstext.color = new Color(Skillstext.color.a, Skillstext.color.g, Skillstext.color.b, 1);
        }
    }

    public IEnumerator Waiter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Escape"))
        {
            PauseStatus = !PauseStatus;
            Pause(PauseStatus);
        }

        if(LevelRestart)
        {
            LevelComplete = false;
            SoundPlayed = false;
            Moving.StartMoving();
            CoinsCollectoiner.Coins = 0;
            Time.timeScale = 1f;
        }
        if(LevelComplete)
        {
            FinishwordAnim.SetBool("LevelFinished", true);
            if(!SoundPlayed)
            {
                GetComponent<AudioSource>().PlayOneShot(WinningSound);
                SoundPlayed = true;
            }
            Player.GetComponent<Rigidbody2D>().drag = 7;
            Moving.StopMoving();
        }

        if(!GetComponent<AudioSource>().isPlaying && SoundPlayed)
        {
            Pause(true);
            ContinueButton.SetActive(false);
            PauseWord.text = "Победа!";
            SoundPlayed = false;
            LevelComplete = false;
        }
    }
}
