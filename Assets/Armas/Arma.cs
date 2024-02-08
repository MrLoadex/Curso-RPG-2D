using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma
{
    Magia,
    Melee
}


[CreateAssetMenu(menuName = "Personaje/Arma")]
public class Arma : ScriptableObject
{

    [Header("Configurcion")]

    public Sprite ArmaIcono;
    public Sprite SkillIcono;
    public TipoArma Tipo;
    public float Daño;

    [Header("Arma Magica")]
    public float ManaRequerida;

    [Header("Sats")]
    public float ChanceCritico;
    public float ChanceDeBloqueo;
}
