using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public GameObject Canvas_dia;
    // public GameObject peopleEndTag_change;
    // public GameObject huaitagChange;
    // public BoxCollider coli_false;
    // public BoxCollider people_colli_false_netherland;
    // public BoxCollider people_colli_false_huai;
    // public BoxCollider people_colli_false_netSir;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
        void Update()
    {
        //enter繼續對話
        if(Input.GetKeyDown(KeyCode.E))//enter(KeyCode.Return)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Canvas_dia.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence (string sentence)
        {
            dialogueText.text="";
            foreach(char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

    void EndDialogue()
    {
        Canvas_dia.SetActive(false);
        Time.timeScale=1;
        Debug.Log("End of conversation");
        // peopleEndTag_change.tag="Untagged";
        // huaitagChange.tag="Untagged";
        // coli_false.enabled=false;
        //-------------把coli關掉=不觸發對話
        // people_colli_false_netherland.enabled=false;
        // people_colli_false_huai.enabled=false;
        // people_colli_false_netSir.enabled=false;

    }


}
