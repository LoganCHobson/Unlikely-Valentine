using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public GameObject dialogBox;

    private string[] currentDialog;
    private int currentLine = 0;

    public void StartDialog(string[] dialog)
    {
        currentDialog = dialog;
        currentLine = 0;
        ShowDialog();
    }

    public void ShowDialog()
    {
        if (currentLine < currentDialog.Length)
        {
            dialogBox.SetActive(true);
            dialogText.text = currentDialog[currentLine];
            currentLine++;
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        // Additional logic for ending dialog (e.g., reward player, trigger events, etc.)
    }
}
