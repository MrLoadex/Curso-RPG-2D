using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dañoTMP;
    
    public void EstablecerTexto(float cantidad, Color color)
    {
        dañoTMP.text = cantidad.ToString();
        dañoTMP.color = color;
    }
}
