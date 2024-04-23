using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject portraitImg;
    public TMP_Text dialogText;
    public GameObject dialogBox;
    public float typingSpeed = 0.05f; 

    private Image[] currentPortrait;
    private int currentPortraitIndex;
    private string[] currentDialog;
    private int currentLine = 0;
    private bool isTyping = false;

    public string[] test;
    public Image[] test2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
        {
            ShowDialog();
        }
    }

    public void StartDialog(string[] dialog, Image[] portrait)
    {
        currentPortrait = portrait;
        currentDialog = dialog;
        currentLine = 0;
        ShowDialog();
    }

    public void ShowDialog()
    {
        if (currentLine < currentDialog.Length)
        {
            dialogBox.SetActive(true);
            StartCoroutine(TypeText(currentDialog[currentLine]));
            currentLine++;
        }
        else
        {
            EndDialog();
        }

        if(currentPortraitIndex < currentPortrait.Length)
        {
            portraitImg.gameObject.SetActive(true);
            portraitImg.GetComponent<Image>().sprite = test2[currentPortraitIndex].sprite;
            currentPortraitIndex++;
        }
        else
        {
            EndDialog();
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in text)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void EndDialog()
    {
        portraitImg.gameObject.SetActive(false);
        dialogBox.SetActive(false);
        dialogText.gameObject.SetActive(false);
        
    }

    private void Start()
    {
        StartDialog(test, test2);
    }
}
