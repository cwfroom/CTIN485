using UnityEngine;
using System.Collections;

public class ItemType : MonoBehaviour {

    //public string FactoryTag;
    //public string CourtyardTag;
    public string ItemTag;
    public bool onHand;

    void Start() {

        //if (GameManager.LayerString == "Factory")
        //{
			//transform.GetChild(1).gameObject.SetActive(false);
        //}
        //else if (GameManager.LayerString == "Courtyard")
       // {
	     	//transform.GetChild(0).gameObject.SetActive(false);
        //}
        onHand = false;
    }

    void Update()
    {
        //if (GameManager.LayerString == "Factory")
       // {
			//gameObject.layer = LayerMask.NameToLayer ("Factory");

           // transform.GetChild(1).gameObject.SetActive(false);
           // transform.GetChild(0).gameObject.SetActive(true);
        //}
        //else if (GameManager.LayerString == "Courtyard")
        //{
			//gameObject.layer = LayerMask.NameToLayer ("Courtyard");

            //transform.GetChild(0).gameObject.SetActive(false);
            //transform.GetChild(1).gameObject.SetActive(true);
        //}
    }

    public string GetObjectTag() {

        //if (GameManager.LayerString == "Factory") {
           // return FactoryTag;
        //}
        //else if (GameManager.LayerString == "Courtyard") {
           // return CourtyardTag;
        //}
        return ItemTag;
    }

    public bool IsObjectActive() {

        //if (GameManager.LayerString == "Factory") {
            //return transform.GetChild(0).gameObject.activeInHierarchy;
        //}
        //else if (GameManager.LayerString == "Courtyard") {
            //return transform.GetChild(1).gameObject.activeInHierarchy;
        //}
        return gameObject.activeSelf;
    }

    //public void SetObjectActive(string tag, bool trigger) {

        //if (tag == "Factory"){
            //transform.GetChild(0).gameObject.SetActive(trigger);
       // }
        //else if (tag == "Courtyard"){
           // transform.GetChild(1).gameObject.SetActive(trigger);
        //}
    //}
}
