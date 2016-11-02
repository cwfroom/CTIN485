using UnityEngine;
using System.Collections;

public class CoinProperty : UnitInteractible
{
    public int m_Timeout = 5;
    public GameManager m_Gmr;

    void Start()
    {
        StartCoroutine(TimeoutDestroy());
    }


    public static CoinProperty Create(GameManager gmr, Vector3 initialPos)
    {
        GameObject nCoin = Instantiate(Resources.Load("Prefabs/Coin")) as GameObject;
        nCoin.transform.position = initialPos;

        CoinProperty nCP = nCoin.GetComponent<CoinProperty>();
        nCP.m_Gmr = gmr;
        return nCP;
    }

    public override void UnitInteract(UnitProperty interactingUnit)
    {
        m_Gmr.AddCoin(1, interactingUnit.m_Team);
        Destroy(this.gameObject);
    }

    IEnumerator TimeoutDestroy()
    {
        yield return new WaitForSeconds(m_Timeout);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    { 
       if (collision.transform.GetComponentInChildren<UnitProperty>())
        {
            m_Gmr.AddCoin(1, collision.transform.GetComponentInChildren<UnitProperty>().m_Team);
            Destroy(this.gameObject);
        }
    }


}
