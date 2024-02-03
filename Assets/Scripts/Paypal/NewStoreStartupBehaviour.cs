using UnityEngine;
using UnityEngine.UI;

public class NewStoreStartupBehaviour : MonoBehaviour
{
	public static NewStoreStartupBehaviour INSTANCE;

	void Awake()
	{
		INSTANCE = this;
	}

	// [HideInInspector]
	public string accessToken;

	// Use this for initialization
	void Start()
	{

		GetAccessTokenAPI_Call getAccessTokenAPI_Call = PayPalCallbackStore.INSTANCE.gameObject.AddComponent<GetAccessTokenAPI_Call>();
		getAccessTokenAPI_Call.payPallCallbackBase = PayPalCallbackStore.INSTANCE;

	}

}
