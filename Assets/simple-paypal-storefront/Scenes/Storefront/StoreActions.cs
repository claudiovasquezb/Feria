﻿using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class StoreActions : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern void openWindow(string url);
	
	public static StoreActions INSTANCE;

	void Awake() {
		INSTANCE = this;
	}

	public GameObject MainStoreScreen;
	public GameObject PurchaseItemScreen;

	public Image PurchaseItemImageField;
	public Text PurchaseItemNameField;
	public Text PurchaseItemCostField;
	public Text PurchaseItemCurrCodeField;
	public Text PurchaseItemDescField;

	public Text PurchaseStatusField;
	public Text PurchaseActionText;
	public Text PurchasePromptText;
	public Button PurchaseActionButton;

	public GameObject PurchaseItemFields;
	public GameObject PurchaseScreenDividers;
	public GameObject PurchaseScreenSpinner;

	public Text StoreTitleField;

	public enum PurchaseStatus {
		NO_ITEM_SELECTED,
		CREATING_PURCHASE, 
		CHECKOUT_READY,
		WAITING,
		COMPLETE,
		INCOMPLETE
	};

	public PurchaseStatus currentPurchaseStatus = PurchaseStatus.NO_ITEM_SELECTED;

	public void OpenStore() {

		MainStoreScreen.SetActive (true);
		changePurchaseStatus (PurchaseStatus.NO_ITEM_SELECTED);

	}

	public void CloseStore() {
		SceneManager.UnloadSceneAsync(this.gameObject.scene);
	}

	public void purchaseScreenActionButtonClick() {

		if (currentPurchaseStatus == PurchaseStatus.CHECKOUT_READY) {
			//Open Checkout page in browser
			string checkoutUrl = GlobalPayPalProperties.INSTANCE.GetComponent<CreatePaymentAPI_Call> ().API_SuccessResponse.links [1].href;

			if (Application.platform.Equals (RuntimePlatform.WebGLPlayer)) {
				openWindow (checkoutUrl);
			}
            else {
				Application.OpenURL (checkoutUrl);
			}

			ShowPaymentAPI_Call apiCall = this.gameObject.AddComponent<ShowPaymentAPI_Call>();
			apiCall.payPallCallbackBase = PayPalCallbackStore.INSTANCE;

			apiCall.accessToken = StoreStartupBehaviour.INSTANCE.accessToken;
			apiCall.payID = PayPalCallbackStore.INSTANCE.payId;

			changePurchaseStatus (PurchaseStatus.WAITING);

		} else if (currentPurchaseStatus == PurchaseStatus.WAITING) {
			
			DialogScreenActions.INSTANCE.setContextConfirmAbortPayment();
			DialogScreenActions.INSTANCE.ShowDialogScreen();

		} 

	}

	public void resetCheckoutScreen() {
		Destroy (GlobalPayPalProperties.INSTANCE.gameObject.GetComponent<CreatePaymentAPI_Call> ());
		Destroy (GlobalPayPalProperties.INSTANCE.gameObject.GetComponent<ShowPaymentAPI_Call> ());
		Destroy (GlobalPayPalProperties.INSTANCE.gameObject.GetComponent<ExecutePaymentAPI_Call> ());
		changePurchaseStatus (PurchaseStatus.NO_ITEM_SELECTED);
	}

	public void OpenPurchaseItemScreen(StoreItemContent itemToPurchase) {

		MenuNavigation.INSTANCE.selectPurchaseIcon ();

		PurchaseItemImageField.sprite = itemToPurchase.itemImage;
		PurchaseItemNameField.text = itemToPurchase.itemName;

		PurchaseItemCostField.text = string.Format("{0:N}", itemToPurchase.itemCost);
		PurchaseItemCostField.text = CurrencyCodeMapper.GetCurrencySymbol (GlobalPayPalProperties.INSTANCE.currencyCode) + PurchaseItemCostField.text;

		PurchaseItemDescField.text = itemToPurchase.itemDesc;
		PurchaseItemCurrCodeField.text = GlobalPayPalProperties.INSTANCE.currencyCode;

		changePurchaseStatus (PurchaseStatus.CREATING_PURCHASE);

		CreatePaymentAPI_Call existingCreatePaymentAPIcall = GlobalPayPalProperties.INSTANCE.gameObject.GetComponent<CreatePaymentAPI_Call> ();
		if (existingCreatePaymentAPIcall != null) {
			Destroy (existingCreatePaymentAPIcall);
		}

		CreatePaymentAPI_Call createPaymentAPICall = GlobalPayPalProperties.INSTANCE.gameObject.AddComponent<CreatePaymentAPI_Call> ();
		createPaymentAPICall.payPallCallbackBase = PayPalCallbackStore.INSTANCE;

		createPaymentAPICall.accessToken = StoreStartupBehaviour.INSTANCE.accessToken;
		createPaymentAPICall.transactionDescription = string.Format("In-game PayPal purchase for {0}: {1}", new object[] {Application.productName, itemToPurchase.itemImage});
		createPaymentAPICall.itemCurrency = GlobalPayPalProperties.INSTANCE.currencyCode;
		createPaymentAPICall.itemDescription = itemToPurchase.itemDesc;
		createPaymentAPICall.itemName = itemToPurchase.itemName;
		createPaymentAPICall.itemPrice = itemToPurchase.itemCost;

	}

	public void changePurchaseStatus (PurchaseStatus newStatus) {

		switch (newStatus) {

			case PurchaseStatus.NO_ITEM_SELECTED : {
				PurchaseItemFields.SetActive(false);
				PurchaseActionButton.gameObject.SetActive(true);
				PurchaseScreenSpinner.SetActive (false);
				PurchaseActionText.text = "Return";
				PurchaseStatusField.text = "No Item Selected";
				PurchasePromptText.text = "No item is currently selected.";
			} break;

			case PurchaseStatus.CREATING_PURCHASE : {
				PurchaseItemFields.SetActive(true);
				PurchaseActionButton.gameObject.SetActive(false);
				PurchaseScreenSpinner.SetActive (true);
				PurchaseStatusField.text = "Creating Purchase...";
				PurchasePromptText.text = "Please wait while the purchase is being set up for the following item:";
			} break;

			case PurchaseStatus.CHECKOUT_READY : {				
				PurchaseItemFields.SetActive(true);
				PurchaseActionButton.gameObject.SetActive(true);
				PurchaseScreenSpinner.SetActive (false);
				PurchaseActionText.text = "Checkout";
				PurchaseStatusField.text = "Checkout Ready";
				PurchasePromptText.text = "Please click the 'Checkout' button to complete the purchase for this item with PayPal:";
			} break;

			case PurchaseStatus.WAITING : {				
				PurchaseItemFields.SetActive(true);
				PurchaseActionButton.gameObject.SetActive(true);
				PurchaseScreenSpinner.SetActive (true);
				PurchaseActionText.text = "Cancel";
				PurchaseStatusField.text = "Waiting";
				PurchasePromptText.text = "A Paypal tab has been opened in your web browser to complete the purchase for the following item:";				
			} break;
				
			case PurchaseStatus.COMPLETE : {				
				PurchaseItemFields.SetActive(true);
				PurchaseActionButton.gameObject.SetActive(false);
				PurchaseScreenSpinner.SetActive (false);
				PurchaseStatusField.text = "Purchase Completed!";
				PurchasePromptText.text = "Purchase complete!";
				DialogScreenActions.INSTANCE.setContextPurchaseSuccess();
				DialogScreenActions.INSTANCE.ShowDialogScreen();
			} break;

		}

		currentPurchaseStatus = newStatus;

	}

}