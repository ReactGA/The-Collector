using UnityEngine;

public class ItemsChecker : MonoBehaviour
{
    [SerializeField] PlayerObject plObj;
    private void OnTriggerEnter(Collider c)
    {

        if (c.CompareTag("Ingredent") && !plObj.HasHoldIng)
        {
            plObj.NumOfIngOn += 1;
            plObj.IngredentText.text = (plObj.NumOfIngOn).ToString();
            plObj.IngToKeep = c.gameObject;
            Invoke("WearPPE", 1);
            Invoke("ResetPPEbool", 2);
            if (plObj.NumOfIngOn == 5)
            {
                plObj.WinGame();
            }
            plObj.HasHoldIng = true;
        }
    }
    void ResetPPEbool()
    {
        plObj.HasHoldIng = false;
    }
    void WearPPE()
    {
        //plObj.KeepIngr();
    }
}
