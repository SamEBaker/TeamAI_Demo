using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int answer; //0 is ai, 1 is real
    [SerializeField] int type; //0 is image, 1 is text
    [SerializeField] int clicked;
    [SerializeField] int amtOfQuestions = 1;
    public int score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text textGenerated;
    public List<string> textSnippets;
    public Image currentImage;
    public GameObject textContent;
    public GameObject imgContent;
    public GameObject loseScreen;
    public GameObject winScreen;
    public List<Sprite> AIimages;
    public List<Sprite> Realimages;

    void Start()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        textContent.SetActive(false);
        Next();
    }
    private void Update()
    {
        //run timer
        //if timer runs out skip to next question
    }

    public void ClickAI()
    {
        clicked = 0;
        CheckAnswer();
    }
    public void ClickReal()
    {
        clicked = 1;
        CheckAnswer();
    }

    public void CheckAnswer()
    {
        if(answer == clicked)
        {
            score ++;
            scoreText.text = "Score: " + score;
            Tips(winScreen);
        }
        else
        {
            Tips(loseScreen);
            //set text
        }
        amtOfQuestions++;
    }

    public void Next()
    {
        questionText.text = "Question#: " + amtOfQuestions + " / ?";
        //reset timer
        type = Random.Range(0, 2);
        answer = Random.Range(0, 2);
        Debug.Log(type);
        if (type == 0)
        {
            Debug.Log("image");
            textContent.SetActive(false);
            imgContent.SetActive(true);
            if (answer == 0)
            {
                currentImage.sprite = AIimages[Random.Range(0, AIimages.Count)];
                
            }
            else if (answer == 1)
            {
                currentImage.sprite = Realimages[Random.Range(0, Realimages.Count)];
            }
        }
        else if (type == 1)
        {
            //random answer for random text for now
            Debug.Log("text");
            textGenerated.text = textSnippets[Random.Range(0, textSnippets.Count)];
            imgContent.SetActive(false);
            textContent.SetActive(true);

        }
    }
    public void Tips(GameObject screen)
    {
        StartCoroutine(Popuptips(screen));

    }
    IEnumerator Popuptips(GameObject Thisscreen)
    {
        //disable buttons while active
        Thisscreen.SetActive(true);
        yield return new WaitForSeconds(2);
        Thisscreen.SetActive(false);
        Next();
    }
}
