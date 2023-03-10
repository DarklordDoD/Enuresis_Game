using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace DailyRewardsSystem
{
    public enum RewardType
    {
        Currency,
        Snacks
    }

    [Serializable] public struct Reward
    {
        public RewardType Type;
        public int Amount;
    }

    public class DailyRewards : MonoBehaviour
    {
        //[Header("Main Menu UI")]
        //[SerializeField] Text currencyText;
        //[SerializeField] Text snacksText;
        

        [Space]
        [Header("Reward UI")]
        [SerializeField] GameObject rewardsCanvas;
        [SerializeField] Button openButton;
        [SerializeField] Button closeButton;
        [SerializeField] Image rewardImage;
        [SerializeField] Text rewardAmountText;
        [SerializeField] Button claimButton;
        [SerializeField] GameObject rewardsNotification;
        [SerializeField] GameObject noMoreRewardsPanel;

        [Space]
        [Header("Reward Images")]
        [SerializeField] Sprite iconCurrencySprite;
        [SerializeField] Sprite iconSnacksSprite;

        [Space]
        [Header("Rewards Database")]
        [SerializeField] RewardsDatabase rewardsDB;

        [Space]
        [Header("Timing")]
        // Next reward wait delay is using seconds, just change the format if need be.
        [SerializeField] double nextRewardDelay = 20f;
        //Checks for rewards every 5 seconds that pass
        [SerializeField] float checkForRewardDelay = 5f;

        private int nextRewardIndex;
        private bool isRewardReady = false;
        private GameObject gameController;
        private GameObject snacksMenu;
        private int currency;
        private int snacks;
        private DateTime rewardClaimDateTime;

        void Start()
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");
            snacksMenu = GameObject.Find("Snacks");

            if (snacksMenu.GetComponent<SnackMenu>().menuOpen)
                snacksMenu.GetComponent<SnackMenu>().OpenMenu();

            rewardClaimDateTime = gameController.GetComponent<Ressourcer>().dato;

            Initialize();

            StopAllCoroutines();
            StartCoroutine ( CheckForRewards());   
        }

        void Initialize()
        {
            //Add Click Events
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(OnOpenButtonClick);

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClick);

            //Claim button
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener( OnClaimButtonClick );
        }

        IEnumerator CheckForRewards()
        {

            while (true)
            {
                if (!isRewardReady)
                {
                    DateTime currentDatetime = DateTime.Now;                 

                    //Get total seconds between these two dates (in your real game use TotalHours)
                    double elapsedSeconds = (currentDatetime - rewardClaimDateTime).TotalSeconds;

                    if (elapsedSeconds >= nextRewardDelay)
                        ActivateReward();
                    else
                        DeactivateReward();
                }

                yield return new WaitForSeconds(checkForRewardDelay);
            }
         
        }

        void ActivateReward()
        {
            rewardClaimDateTime = DateTime.Now;

            isRewardReady = true;

            noMoreRewardsPanel.SetActive(false);
            rewardsNotification.SetActive(true);


            //Update Reward UI
            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            if (reward.Type == RewardType.Currency)
                rewardImage.sprite = iconCurrencySprite;

           //Other items if you need to add them
          // else if (reward.Type == RewardType."Thing")
         //    rewardImage.sprite = icon"Thing"Sprite;

            else rewardImage.sprite = iconSnacksSprite;

            rewardAmountText.text = String.Format("+{0}", reward.Amount);    
        }

        void DeactivateReward ()
        {
            isRewardReady = false;

            noMoreRewardsPanel.SetActive(true);
            rewardsNotification.SetActive(false);
        }

        void OnClaimButtonClick ()
        {
            Reward reward = rewardsDB.GetReward(nextRewardIndex);

            //check reward type
            if (reward.Type == RewardType.Currency)
            {
                //Debug.Log ("<color=white>"+reward.Type.ToString ()+ "Claimed : </color>+" + reward.Amount );
                currency = reward.Amount;
                //TO DO : FX??
                UpdateCurrencyTextUI ();
                CloseAfterReward();
            }
            else if (reward.Type == RewardType.Snacks)
            {
                //Debug.Log("<color=yellow>" + reward.Type.ToString() + "Claimed : </color>+" + reward.Amount);
                snacks = reward.Amount;
                //TO DO : FX??
                UpdateSnacksTextUI();
                CloseAfterReward();
            }
            //Save next reward index
            nextRewardIndex++;
            if (nextRewardIndex>=rewardsDB.rewardsCount)
                nextRewardIndex = 0;

            PlayerPrefs.SetInt("Next_Reward_Index", nextRewardIndex);

            //Save DateTime of the last claim click
            PlayerPrefs.SetString ( "Reward_Claim_Datetime", DateTime.Now.ToString() );

            DeactivateReward();
        }

        //Update Mainmenu UI (currency, snacks) ---------------------------
        void UpdateCurrencyTextUI()
        {
            gameController.GetComponent<Ressourcer>().monny += currency;
        }
        void UpdateSnacksTextUI()
        {
            gameController.GetComponent<Snacks>().changeSnaks("Snacks_Merged", 5500, snacks);
        }

        private void CloseAfterReward()
        {
            Invoke("OnCloseButtonClick", 0.5f);
        }
        //Open | Close UI -------------------------------------------------
        void OnOpenButtonClick()
        {
            rewardsCanvas.SetActive(true);

            if (snacksMenu.GetComponent<SnackMenu>().menuOpen)
                snacksMenu.GetComponent<SnackMenu>().OpenMenu();
        }
        void OnCloseButtonClick()
        {
            rewardsCanvas.SetActive(false);
        }
    }

}
