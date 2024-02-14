using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;

    public ItemArma ArmaEquipada {get; private set;}

    public void EquiparArma(ItemArma itemArma)
    {
        ArmaEquipada = itemArma;
        armaIcono.sprite = itemArma.Arma.ArmaIcono;
        armaSkillIcono.sprite = itemArma.Arma.SkillIcono;
        
        armaIcono.gameObject.SetActive(true);
        armaSkillIcono.gameObject.SetActive(true);
    }

    public void RemoverArma()
    {
        armaIcono.gameObject.SetActive(false);
        armaSkillIcono.gameObject.SetActive(false);
        ArmaEquipada = null;
    }
}
