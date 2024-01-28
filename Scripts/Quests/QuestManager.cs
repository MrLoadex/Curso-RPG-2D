using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private Quest[] questDisponibles;
    

    [Header("NpcQuest")]
    [SerializeField] private NPCQuestDescripcion inspectorQuestPrfab;
    [SerializeField] private Transform inspectorQuestContenedor;
    
    private void Start() 
    {
        CargarQuestNPCs();    
    }

    private void CargarQuestNPCs()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            NPCQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrfab ,inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }
}
