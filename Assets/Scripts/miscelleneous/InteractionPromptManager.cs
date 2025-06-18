using UnityEngine;
using TMPro; // use this if using TextMeshPro, otherwise use UnityEngine.UI for Text

public class InteractionPromptManager : MonoBehaviour
{
    public static InteractionPromptManager Instance;

    [SerializeField] private TMP_Text promptText;  // Drag your TextMeshPro UI here
    // OR use: public Text promptText; if using UnityEngine.UI.Text

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        promptText.text = message;
        promptText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
