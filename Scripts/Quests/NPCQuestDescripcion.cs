using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;


    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        questRecompensa.text = $"-{questPorCargar.RecompensaOro} Oro" + 
        $"-{questPorCargar.RecompensaExp} Exp " + 
        $"-{questPorCargar.RecompensaItem.item.Nombre} X {questPorCargar.RecompensaItem.item.Cantidad}";
    }
}
