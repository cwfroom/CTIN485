using UnityEngine;
using System.Collections;

public class CoinProperty : UnitInteractible
{
    public static CoinProperty Create(GameManager gmr, Vector3 initialPos)
    {
        GameObject nUnit = Instantiate(Resources.Load("Prefabs/Coin")) as GameObject;
        nUnit.transform.position = initialPos;

        CoinProperty nCP = nUnit.GetComponent<CoinProperty>();     
        return nCP;
    }

    public override void UnitInteract(UnitProperty interactingUnit)
    {
        Destroy(this.gameObject);
    }
}
