using System;
using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class HomeScene : MonoBehaviour {

	public Button[] Buttons;

	public GameObject BannerPanel;
	public GameObject InterstitialPanel;
	public GameObject VideoPanel;
	public GameObject NativePanel;

	void CloseAllPanel() {
		BannerPanel.SetActive (false);
		InterstitialPanel.SetActive (false);
		VideoPanel.SetActive (false);
		NativePanel.SetActive (false);
		foreach(Button btn in Buttons) {
			btn.interactable = true;
		}


		if(bannerView != null) {
			bannerView.Destroy ();
			bannerView = null;
		}

		if(interstitial != null) {
			interstitial.Destroy ();
			interstitial = null;
		}

		if(nativeAd != null) {
			nativeAd.Destroy ();
			nativeAd = null;
		}
	}

	public void OnBannerButtonClick() {
		CloseAllPanel ();
		BannerPanel.SetActive (true);
		foreach(Button btn in Buttons) {
			if(btn.name == "Banner Button") {
				btn.interactable = false;
			}
		}
	}

	public void OnInterstitialButtonClick() {
		CloseAllPanel ();
		InterstitialPanel.SetActive (true);
		foreach(Button btn in Buttons) {
			if(btn.name == "Interstitial Button") {
				btn.interactable = false;
			}
		}
	}

	public void OnVideoButtonClick() {
		CloseAllPanel ();
		VideoPanel.SetActive (true);
		foreach(Button btn in Buttons) {
			if(btn.name == "Rewarded Video Button") {
				btn.interactable = false;
			}
		}
	}

	public void OnNativeButtonClick() {
		CloseAllPanel ();
		NativePanel.SetActive (true);
		foreach(Button btn in Buttons) {
			if(btn.name == "Native Ads Button") {
				btn.interactable = false;
			}
		}
	}


	// ========== Banner ================================================================================
	#region Banner
	BannerView bannerView = null;
	public void BannerInit() {
		if(bannerView != null) {
			bannerView.Destroy ();
			bannerView = null;
		}

		string adUnitId = "";
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			adUnitId = "ca-app-pub-7619324821036210/2963984288";
		} else {
			adUnitId = "ca-app-pub-7619324821036210/4440717488";
		}

		AdSize adsize = AdSize.Banner;
		AdPosition adPosition = AdPosition.Top;
		bannerView = new BannerView(adUnitId, adsize, adPosition);

		RegisterBannerCallback ();
	}

	public void LoadBanner() {
		if(bannerView != null) {
			AdRequest request = new AdRequest.Builder().Build();
			bannerView.LoadAd(request);
		}
	}

	public void ShowBanner() {
		if(bannerView != null) {
			bannerView.Show ();
		}
	}

	public void HideBanner() {
		if(bannerView != null) {
			bannerView.Hide ();
		}
	}

	void RegisterBannerCallback() {
		bannerView.OnAdLoaded += (object sender, EventArgs args) => {
			Debug.Log("[Admob] Banner OnAdLoaded");
		};

		bannerView.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) => {
			Debug.Log("[Admob] Banner OnAdFailedToLoad error:" + args.Message);
		};

		bannerView.OnAdOpening += (object sender, EventArgs args) => {
			Debug.Log("[Admob] Banner OnAdOpening");
		};

		bannerView.OnAdClosed += (object sender, EventArgs args) => {
			Debug.Log("[Admob] Banner OnAdClosed");
		};

		bannerView.OnAdLeavingApplication += (object sender, EventArgs args) => {
			Debug.Log("[Admob] Banner OnAdLeavingApplication");
		};

	}
	#endregion



	// ========== Interstitial ================================================================================
	#region Interstitial
	public Text HasInterstitialButtonText;

	InterstitialAd interstitial = null;
	public void LoadInterstitial() {
		if(interstitial != null) {
			interstitial.Destroy ();
			interstitial = null;
		}

		string adUnitId = "";
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			adUnitId = "ca-app-pub-7619324821036210/4239890283";
		} else {
			adUnitId = "ca-app-pub-7619324821036210/8809690685";
		}
			
		interstitial = new InterstitialAd(adUnitId);
		RegisterInterstitialCallback ();

		AdRequest request = new AdRequest.Builder().Build();
		interstitial.LoadAd (request);

		Debug.Log ("[admob] LoadInterstitial adUnitId:" + adUnitId);
	}

	public bool HasInterstitial() {
		return (interstitial != null && interstitial.IsLoaded ());
	}

	public void ShowInterstitial() {
		if(interstitial == null || !interstitial.IsLoaded()) {
			return;
		}
		interstitial.Show ();
	}

	void RegisterInterstitialCallback() {
		
		interstitial.OnAdLoaded += (object sender, EventArgs args) => {
			Debug.Log("[Admob] interstitial OnAdLoaded");
		};

		interstitial.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) => {
			Debug.Log("[Admob] interstitial OnAdFailedToLoad error:" + args.Message);
		};

		interstitial.OnAdOpening += (object sender, EventArgs args) => {
			Debug.Log("[Admob] interstitial OnAdLoaded");
		};

		interstitial.OnAdClosed += (object sender, EventArgs args) => {
			Debug.Log("[Admob] interstitial OnAdLoaded");
		};

		interstitial.OnAdLeavingApplication += (object sender, EventArgs args) => {
			Debug.Log("[Admob] interstitial OnAdLoaded");
		};
	}

	public void OnHasInterstitialButtonClick() {
		bool hasInterstitial = HasInterstitial ();
		HasInterstitialButtonText.text = (hasInterstitial) ? ("Has Interstitial True") : ("Has Interstitial False");
	}
	#endregion


	// ========== Video ================================================================================
	#region Video Ad
	public Text HasVideoButtonText;

	RewardBasedVideoAd videoAd = null;
	public void LoadVideo() {
		if(videoAd == null) {
			videoAd = RewardBasedVideoAd.Instance;
			RegisterVideoCallback ();
		}

		string adUnitId = "";
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			adUnitId = "ca-app-pub-7619324821036210/1343469481";
		} else {
			adUnitId = "ca-app-pub-7619324821036210/1483070282";
		}

		AdRequest request = new AdRequest.Builder().Build();
		videoAd.LoadAd(request, adUnitId);
		Debug.Log ("[admob] LoadVideo adUnitId:" + adUnitId);
	}

	public bool HasVideo() {
		return (videoAd != null && videoAd.IsLoaded ());
	}

	public void ShowVideo() {
		if(videoAd == null || !videoAd.IsLoaded()) {
			return;
		}
		videoAd.Show ();
	}

	void RegisterVideoCallback() {
		videoAd.OnAdLoaded += (object sender, EventArgs args) => {
			Debug.Log("[Admob] videoAd OnAdLoaded");
		};

		videoAd.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) => {
			Debug.Log("[Admob] videoAd OnAdFailedToLoad error:" + args.Message);
		};

		videoAd.OnAdOpening += (object sender, EventArgs args) => {
			Debug.Log("[Admob] videoAd OnAdLoaded");
		};

		videoAd.OnAdClosed += (object sender, EventArgs args) => {
			Debug.Log("[Admob] videoAd OnAdLoaded");
		};

		videoAd.OnAdLeavingApplication += (object sender, EventArgs args) => {
			Debug.Log("[Admob] videoAd OnAdLoaded");
		};

		videoAd.OnAdRewarded += (object sender, Reward args) => {
			Debug.Log("[Admob] videoAd OnAdRewarded:" + " " + args.Amount.ToString() + " " + args.Type);
		};
	}

	public void OnHasVideoButtonClick() {
		bool hasVideo = HasVideo ();
		HasVideoButtonText.text = (hasVideo) ? ("Has Video True") : ("Has Video False");
	}
	#endregion


	// ========== Native ================================================================================
	#region Native
	NativeExpressAdView nativeAd = null;
	public void NativeInit() {
		if(nativeAd != null) {
			nativeAd.Destroy ();
			nativeAd = null;
		}

		string adUnitId = "";
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			adUnitId = "ca-app-pub-7619324821036210/1135180686";
		} else {
			adUnitId = "ca-app-pub-7619324821036210/4647050289";
		}

		AdSize adsize = new AdSize(300, 100);
		AdPosition adPosition = AdPosition.Top;
		nativeAd = new NativeExpressAdView(adUnitId, adsize, adPosition);

		RegisterNativeCallback ();
	}

	public void LoadNative() {
		if(nativeAd != null) {
			AdRequest request = new AdRequest.Builder().Build();
			nativeAd.LoadAd(request);
		}
	}

	public void ShowNative() {
		if(nativeAd != null) {
			nativeAd.Show ();
		}
	}

	public void HideNative() {
		if(nativeAd != null) {
			nativeAd.Hide ();
		}
	}

	void RegisterNativeCallback() {
		nativeAd.OnAdLoaded += (object sender, EventArgs args) => {
			Debug.Log("[Admob] nativeAd OnAdLoaded");
		};

		nativeAd.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) => {
			Debug.Log("[Admob] nativeAd OnAdFailedToLoad error:" + args.Message);
		};

		nativeAd.OnAdOpening += (object sender, EventArgs args) => {
			Debug.Log("[Admob] nativeAd OnAdOpening");
		};

		nativeAd.OnAdClosed += (object sender, EventArgs args) => {
			Debug.Log("[Admob] nativeAd OnAdClosed");
		};

		nativeAd.OnAdLeavingApplication += (object sender, EventArgs args) => {
			Debug.Log("[Admob] nativeAd OnAdLeavingApplication");
		};

	}
	#endregion
}
	