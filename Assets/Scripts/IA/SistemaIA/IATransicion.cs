using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IATransicion
{
    public IADecision Decision;
    public IAEstado EstadoVerdadero;//Estado si decision es verdadero
    public IAEstado EstadoFalso;//Si decision es falso


}
