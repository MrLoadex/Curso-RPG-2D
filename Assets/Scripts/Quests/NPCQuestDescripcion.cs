using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;


    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        questRecompensa.text = $"-{quest.RecompensaOro} Oro \n" + 
        $"-{quest.RecompensaExp} Exp\n" + 
        $"-{quest.RecompensaItem.item.Nombre} X {quest.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if(QuestPorCompletar == null)
        {
            return;
        }

        QuestPorCompletar.QuestAceptado = true;

        QuestManager.Instance.AñadirQuest(QuestPorCompletar);
        gameObject.SetActive(false);
    }
}
