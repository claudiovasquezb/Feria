using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStoreItemContent : MonoBehaviour
{
    public void BuyItemAction()
    {
        NewStoreAction.INSTANCE.purchaseScreenActionButtonClick();
        
    }
}
