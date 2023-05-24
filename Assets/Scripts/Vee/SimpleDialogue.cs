using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    [System.Serializable]
    public class StringCollection
    {
        public List<string> lines = new List<string>();
    }

    [Header("Set Up")]
    [SerializeField] private GameObject Player;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private Image DialogueBox;
    [SerializeField] private Image CharacterBust;

    [SerializeField] private StringCollection opening;
    [SerializeField] private List<StringCollection> convo;
    [ReadOnlyInspector][SerializeField] private bool firstPress = false;
    [ReadOnlyInspector][SerializeField] private bool secondPress = false;

    void Start()
    {
        DialogueBox.GetComponent<Image>().enabled = false;
        CharacterBust.GetComponent<Image>().enabled = false;
        textBox.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    private void Update()
    {
        SpacePress();
    }

    private void SpacePress()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !firstPress)
            firstPress = true;
        else if (Input.GetKeyDown(KeyCode.Space) && firstPress && !secondPress)
            secondPress = true;
    }

    public void showUI(bool toggle)
    {
        Player.GetComponent<VeeManager>().interactSign.GetComponent<SpriteRenderer>().enabled = !toggle;
        DialogueBox.GetComponent<Image>().enabled = toggle;
        CharacterBust.GetComponent<Image>().enabled = toggle;
        textBox.GetComponent<TextMeshProUGUI>().enabled = toggle;
    }

    public void talkToVee()
    {
        showUI(true);
        if (!VeeMain.Instance.NotFirstTime)
        {
            VeeMain.Instance.NotFirstTime = true;
            StartCoroutine(RevealText(opening));
            VeeMain.Instance.TalkToVee = true;
        }
        else
        {
            int i;
            while (true)
            {
                i = Random.Range(0, convo.Count);
                if (i != VeeMain.Instance.PastLine) break;
            }
            VeeMain.Instance.PastLine = i;

            StringCollection tmp = convo[i];
            StartCoroutine(RevealText(tmp));
            VeeMain.Instance.TalkToVee = true;
        }
    }

    IEnumerator RevealText(StringCollection input)
    {
        foreach (string text in input.lines)
        {
            textBox.text = "";

            var numCharsRevealed = 0;
            while (numCharsRevealed < text.Length)
            {

                if (firstPress)
                {
                    textBox.text = text;
                    numCharsRevealed = text.Length;
                    break;
                }

                numCharsRevealed++;

                textBox.text = text.Substring(0, numCharsRevealed);

                yield return new WaitForSeconds(0.05f);
            }

            // Wait for the Player to press Space
            while (!secondPress && numCharsRevealed == text.Length)
            {
                yield return null;
            }

            firstPress = false;
            secondPress = false;
            yield return null;
        }
        showUI(false);
        yield return null;
    }
}
