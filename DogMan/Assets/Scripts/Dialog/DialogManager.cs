using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    private Queue<string> sentencesPudel;
    private Queue<string> sentencesSharick;

    public Text dialogSharick;
    public Text dialogPudel;

    public Dialog _dialogSharick;
    public Dialog _dialogPudel;

    private bool Pudel = false;

    public GameObject things;

    private AudioSource tab;

    [SerializeField]private AudioSource typing;

    private bool costil = true;
    private bool costil2 = true;
   
    public void Start()
    {
        sentencesSharick = new Queue<string>();
        sentencesPudel = new Queue<string>();
        tab = GetComponent<AudioSource>();
        
        foreach (string sentence in _dialogSharick.sentences)
        {
            sentencesSharick.Enqueue(sentence);
        }
        foreach (string sentence in _dialogPudel.sentences)
        {
            sentencesPudel.Enqueue(sentence);
        }
        DisplayNextSenetence();
    }

    public void DisplayNextSenetence()
    {
        if (costil2)
        {


            if (costil)
            {
                costil = false;
            }
            else
            {
                tab.Play();
            }

            if (sentencesSharick.Count == 0 && sentencesPudel.Count == 0)
            {
                EndDialog();
                return;
            }

            string sentences;
            if (Pudel)
            {
                sentences = sentencesPudel.Dequeue();
            }
            else
            {
                sentences = sentencesSharick.Dequeue();
            }

            if (sentencesPudel.Count == 0)
            {
                things.SetActive(true);
            }

            costil2 = false;
            StartCoroutine(TypeSentence(sentences));
            
        }
    }

    public void EndDialog()
    {
        SceneSwith.SwithToScene(2);
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (Pudel)
        {
            dialogPudel.text = "";
        }
        else
        {
            dialogSharick.text = "";
        }
        typing.Play();
        foreach (char letter in sentence.ToCharArray())
        {
            if (Pudel)
            {
                dialogPudel.text += letter;
            }
            else
            {
                dialogSharick.text += letter;
            }
            
            yield  return new WaitForSeconds (0.1f);
        }
        typing.Stop();
        if (Pudel)
        {
            Pudel = false;
        }
        else
        {
            Pudel = true;
        }
        costil2 = true;
    }
}
