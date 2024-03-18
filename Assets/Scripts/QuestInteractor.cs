using UnityEngine;
using System.Collections.Generic;

public class QuestInteractor : Interactive
{
    public List<QuestData> quests = new List<QuestData>();
    [HideInInspector] public int current = 0;

    public override void OnInteraction()
    {
        Debug.LogWarning("Interaction pas cod�e");
    }

    public virtual void GiveQuest()
    {
        if (quests.Count > 0 && current < quests.Count)
        {
            QuestData QFD = new QuestData(quests[current],this);
            QuestManager.Instance.TakeQuest(QFD);

            if (quests[current].GetCurrentStep().requirements.Count > 0)
            {
                //Setting up requirements to finish quests
                foreach (QuestItem item in quests[current].GetCurrentStep().requirements)
                {
                    requiredItems.Add(item);
                }
            }
        }
    }

    public void SetupStep()
    {
        if (quests[current].GetCurrentStep().requirements.Count > 0)
        {
            //Setting up requirements to finish quests
            foreach (QuestItem item in quests[current].GetCurrentStep().requirements)
            {
                requiredItems.Add(item);
            }
        }
    }

    public virtual void FinishQuest()
    {
        DialogueManager.instance.PlayDialogue(quests[current].GetCurrentStep().EndDialogue);


        //Dialogue end quest
        if (quests[current].GetCurrentStep().requirements.Count > 0)
        {
            foreach (QuestItem required in quests[current].GetCurrentStep().requirements)
            {
                Inventory.Instance.RemoveFromInventory(required.item, required.quantity);
            }
            requiredItems.Clear();
        }
        //QuestManager.Instance.CompleteQuest(quests[current]);
        quests[current].NextStep();

        if (!quests[current].IsFinished())
        {
            SetupStep();
            return;
        }
        current++;

        if (current == quests.Count)
        {
            Destroy(this);
        }
        else
        {
            Invoke("GiveQuest", 1f);
        }
    }
}