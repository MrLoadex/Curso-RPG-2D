using System;
using UnityEngine;

public enum InteracionExtraNPC
{
    Quests,
    Tienda,
    Crafting
}

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public Sprite Icono;
    public bool ContieneInteraccionExtra;
    public InteracionExtraNPC InteraccionExtra;

    [Header("Saludo")]
    [TextArea] public string Saludo;

    [Header("Chat")]
    public DialogoTexto[] Conversacion;

    [Header("Despedida")]
    [TextArea] public string Despedida; 
}

[Serializable]
public class DialogoTexto
{
    [TextArea] public string Oracion;
}