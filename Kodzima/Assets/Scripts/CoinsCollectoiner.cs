using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCollectoiner : MonoBehaviour
{
    public Text SkillsText;
    public Animator GetCoinAnimate;
    public AudioClip GetCoinSound;

    public static int Coins;

    public void GetCoin()
    {
        Coins++;
        GetComponent<AudioSource>().PlayOneShot(GetCoinSound);
    }
    public void NotRepeat()
    {
        GetCoinAnimate.SetBool("Finished", false);
        Destroy(gameObject);
    }

    void Update()
    {
        SkillsText.text = "Скиллы: " + Coins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Kodzima")
        {
            GetCoinAnimate.SetBool("Finished", true);
        }
    }
}
