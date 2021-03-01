using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteract : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject dialogueOptionsContainer;
    [SerializeField] Transform dialogueOptionsParent;
    [SerializeField] GameObject dialogueOptionsButtonPrefab;
    [SerializeField] DialogueObject startDialogueObject;
    private GameObject newButton;
    bool optionSelected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(DisplayDialogue(startDialogueObject));
    }

    public void StartDialogue(DialogueObject _dialogueObject)
    {
        StartCoroutine(DisplayDialogue(_dialogueObject));
    }

    public void OptionSelected(DialogueObject selectedOption)
    {
        optionSelected = true;
        StartDialogue(selectedOption);
    }

    IEnumerator DisplayDialogue(DialogueObject _dialogueObject)
    {
        yield return null;

        List<GameObject> spawnedButtons = new List<GameObject>();

        dialogueCanvas.enabled = true;

        foreach (var dialogue in _dialogueObject.dialogueSegments)
        {
            dialogueText.text = dialogue.dialogueText;

            if(dialogue.dialogueChoices.Count == 0)
            {
                yield return new WaitForSeconds(dialogue.dialogueDisplayTime);
            }

            else
            {
                dialogueOptionsContainer.SetActive(true);
                //Open Options Panel
                /*foreach (var option in dialogue.dialogueChoices)
                {
                    
                }*/

                for (int i = 0; i < dialogue.dialogueChoices.Count; i++)
                {
                    newButton = Instantiate(dialogue.dialogueChoices[i].spawnPrefabTest, dialogueOptionsParent);
                    spawnedButtons.Add(newButton);
                    newButton.GetComponent<UIDialogueOption>().Setup(this, dialogue.dialogueChoices[i].followOnDialogue, dialogue.dialogueChoices[i].dialogueChoice);
                }

                while(!optionSelected)
                {
                    yield return null;
                }

                break;
            }
            
        }

        dialogueOptionsContainer.SetActive(false);
        dialogueCanvas.enabled = false;
        optionSelected = false;

        spawnedButtons.ForEach(x => Destroy(x));
    }
}