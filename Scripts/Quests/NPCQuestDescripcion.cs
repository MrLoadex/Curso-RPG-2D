using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;


    public override void ConfigurarQuestUI(Quest quest)
    {
        QuestCargado = quest;
        base.ConfigurarQuestUI(quest);
        questRecompensa.text = $"-{quest.RecompensaOro} Oro" + 
        $"-{quest.RecompensaExp} Exp " + 
        $"-{quest.RecompensaItem.item.Nombre} X {quest.RecompensaItem.item.Cantidad}";
    }

    public void AceptarQuest()
    {
        if(QuestCargado == null)
        {
            return;
        }

        QuestManager.Instance.AñadirQuest(QuestCargado);
        gameObject.SetActive(false);
    }
}
