using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public Object dialogDataFile;

    public GameObject dialogBox;
    public TMP_Text dialogText;
    public GameObject portraitContainer;
    public float typingSpeed = 0.05f;

    private List<string> dialogTexts = new List<string>();
    private List<string> portraitNames = new List<string>();
    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        LoadDialogData(dialogDataFile.name + ".txt");
        StartDialog();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                ShowDialog();
            }
            else
            {
                StopCoroutine("TypeText");
                isTyping = false;
                dialogText.text = dialogTexts[currentLine - 1];
            }
        }
    }

    void LoadDialogData(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string portraitTag = line.Substring(1, line.IndexOf('>') - 1);
            string dialog = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);

            portraitNames.Add(portraitTag);
            dialogTexts.Add(dialog);
        }
    }

    public void StartDialog()
    {
        currentLine = 0;
        ShowDialog();
    }

    public void ShowDialog()
    {
        DestroyPortraits();

        if (currentLine < dialogTexts.Count)
        {
            dialogBox.SetActive(true);
            StartCoroutine(TypeText(dialogTexts[currentLine]));

            DisplayPortrait(portraitNames[currentLine]);

            currentLine++;
        }
        else
        {
            EndDialog();
        }
    }

    void DestroyPortraits()
    {
        foreach (Transform child in portraitContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }


    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in text)
        {
            if (isTyping)
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            else
            {
                yield return null;
            }
        }
        isTyping = false;
    }

    void DisplayPortrait(string portraitName)
    {
        GameObject portraitPrefab = Resources.Load<GameObject>("Portraits/" + portraitName + "Prefab");
        if (portraitPrefab != null)
        {
            Instantiate(portraitPrefab, portraitContainer.transform);
        }
        else
        {
            Debug.LogWarning("Portrait prefab not found for: " + portraitName);
        }
    }


    public void EndDialog()
    {
        dialogBox.SetActive(false);
        dialogText.gameObject.SetActive(false);
        portraitContainer.SetActive(false);
    }
}
