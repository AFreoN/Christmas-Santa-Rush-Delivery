using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public static string UnlockLevels = "unlock_all_levels";
    public static string SpecialOffer = "special_offer";
    public static string RemoveAd = "remove_ads";
    public static string CartsUnlock = "unlock_all_carts";
    public static string Cart2Unlock = "unlock_cart3"; //Tesla
    public static string Cart3Unlock = "unlock_cart4"; //4 Dear
    public static string Cart4Unlock = "unlock_cart5"; //4 Wolf
    public static string Cart5Unlock = "unlock_cart6"; //4 Bear
    public static string Bundle1 = "bundle_10k";
    public static string Bundle2 = "bundle_5k";
    public static string Bundle3 = "bundle_20k";
    public static string Bundle4 = "bundle_50k";

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Cart3Unlock, ProductType.NonConsumable);
        builder.AddProduct(Cart2Unlock, ProductType.NonConsumable);
        builder.AddProduct(Cart4Unlock, ProductType.NonConsumable);
        builder.AddProduct(Cart5Unlock, ProductType.NonConsumable);
        builder.AddProduct(CartsUnlock, ProductType.NonConsumable);
        builder.AddProduct(RemoveAd, ProductType.NonConsumable);
        builder.AddProduct(UnlockLevels, ProductType.NonConsumable);
        builder.AddProduct(SpecialOffer, ProductType.Consumable);
        builder.AddProduct(Bundle2, ProductType.Consumable);
        builder.AddProduct(Bundle3, ProductType.Consumable);
        builder.AddProduct(Bundle4, ProductType.Consumable);
        builder.AddProduct(Bundle1, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    public void RemoveADFun()
    {
        BuyProductID(RemoveAd);
    }
    public void CartsUnlockFun()
    {
        BuyProductID(CartsUnlock);
    }
    public void Cart3UnlockFun()
    {
        BuyProductID(Cart3Unlock);
    }
    public void Cart2UnlockFun()
    {
        BuyProductID(Cart2Unlock);
    }
    public void Cart4UnlockFun()
    {
        BuyProductID(Cart4Unlock);
    }
    public void Cart5UnlockFun()
    {
        BuyProductID(Cart5Unlock);
    }
    public void Unlocklevelsfunc()
    {
        BuyProductID(UnlockLevels);
    }
    public void Specialofferfunc()
    {
        BuyProductID(SpecialOffer);
    }
    public void BundleOffer2()
    {
        BuyProductID(Bundle2);
    }
    public void BundleOffer1()
    {
        BuyProductID(Bundle1);
    }
    public void BundleOffer3()
    {
        BuyProductID(Bundle3);
    }
    public void BundleOffer4()
    {
        BuyProductID(Bundle4);        
    }
    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        if (String.Equals(args.purchasedProduct.definition.id, UnlockLevels, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("Levels", 19);
            PlayerPrefs.SetInt("Levels2", 19);
            PlayerPrefs.SetInt("ModeLockChk", 1);
            MainMenuScript.Instance.StartWork();
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, SpecialOffer, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // PlayerPrefs.SetInt("SPECIALOFFER", 1);
            PlayerPrefs.SetInt("ADSUNLOCK", 1);
            PlayerPrefs.SetInt("Levels", 19);
            PlayerPrefs.SetInt("Levels2", 19);
            PlayerPrefs.SetInt("ModeLockChk", 1);
            PlayerPrefs.SetInt("CartsUnlock", 1);
            MainMenuScript.Instance.StartWork();
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, RemoveAd, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("ADSUNLOCK", 1);
            MainMenuScript.Instance.StartWork();
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, CartsUnlock, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("CartsUnlock", 1);
            MainMenuScript.Instance.StartWork();            
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, Cart3Unlock, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("4Cart", 4);
            MainMenuScript.Instance.InappCartsNew();
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, Cart2Unlock, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("3Cart", 3);
            MainMenuScript.Instance.InappCartsNew();
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, Cart4Unlock, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("5Cart", 5);
            MainMenuScript.Instance.InappCartsNew();
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, Cart5Unlock, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("6Cart", 6);
            MainMenuScript.Instance.InappCartsNew();
        }        
        else
        if (String.Equals(args.purchasedProduct.definition.id, Bundle2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 5000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Bundle1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 10000);
        }
        else
       if (String.Equals(args.purchasedProduct.definition.id, Bundle3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 20000);
        }
        else
        if (String.Equals(args.purchasedProduct.definition.id, Bundle4, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 50000);
            PlayerPrefs.SetInt("ADSUNLOCK", 1);
            MainMenuScript.Instance.StartWork();
        }
        else
        {

            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
