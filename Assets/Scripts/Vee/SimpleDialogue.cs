using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueSet
    {
        public string line;
        public Sprite emoticon;
    }

    [System.Serializable]
    public class StringCollection
    {
        public List<DialogueSet> lines = new List<DialogueSet>();
    }

    [Header("Set Up")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private Image DialogueBox;
    [SerializeField] private Image CharacterBust;
    [SerializeField] private Sprite DefaultSprite;
    [SerializeField] private Image ContinueUI;

    [SerializeField] private StringCollection opening;
    [SerializeField] private List<StringCollection> convo;
    [ReadOnlyInspector][SerializeField] private bool firstPress = false;
    [ReadOnlyInspector][SerializeField] private bool secondPress = false;

    void Start()
    {
        DialogueBox.enabled = false;
        CharacterBust.GetComponent<Image>().enabled = false;
        textBox.GetComponent<TextMeshProUGUI>().enabled = false;
        ContinueUI.GetComponent<Image>().enabled = false;
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
        Target.GetComponent<VeeManager>().interactSign.GetComponent<SpriteRenderer>().enabled = !toggle;
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
        foreach (DialogueSet d in input.lines)
        {
            string text = d.line;
            if (d.emoticon == null) CharacterBust.sprite = DefaultSprite;
            else CharacterBust.sprite = d.emoticon;

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

            // Reveal ContinueUI and Wait for the Player to press Space
            ContinueUI.enabled = true;
            ContinueUI.GetComponent<FadeInAndOut>().toggle = true;
            StartCoroutine(ContinueUI.GetComponent<FadeInAndOut>().FadeInFadeOut());
            while (!secondPress && numCharsRevealed == text.Length)
            {
                yield return null;
            }
            ContinueUI.GetComponent<FadeInAndOut>().toggle = false;
            ContinueUI.GetComponent<FadeInAndOut>().Reset(ContinueUI);
            ContinueUI.enabled = false;

            firstPress = false;
            secondPress = false;
            yield return null;
        }
        showUI(false);
        Player.GetComponent<PlayerMovement>().TogglePlayerInput();
        Target.GetComponent<VeeManager>().ToggleTalkMode();
        yield return null;
    }
}
