using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textBox;



    [TextArea]
    public string dialogue1;
    [TextArea]
    public string dialogue2;

    public DialogueVertexAnimator dialogueVertexAnimator;
    void Awake() {
        dialogueVertexAnimator = new DialogueVertexAnimator(textBox);
    }
    private void Update()
    {
        Debug.Log(dialogueVertexAnimator.hadEnded);
    }
    public void PlayDialogue1() {
        PlayDialogue(dialogue1);
    }
    public void AcelerateDialogue()
        
    {
        dialogueVertexAnimator.secondsPerCharacter = 0.2f / 150f;
    }
    public void BackToNormalDialogue()

    {
        dialogueVertexAnimator.secondsPerCharacter = 5f / 150f;
    }
    public void PlayDialogue2()
    {
        PlayDialogue(dialogue2);
    }


    private Coroutine typeRoutine = null;
    void PlayDialogue(string message) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        dialogueVertexAnimator.textAnimating = false;
        List<DialogueCommand> commands = DialogueUtility.ProcessInputString(message, out string totalTextMessage);
        typeRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, null));
    }
}
