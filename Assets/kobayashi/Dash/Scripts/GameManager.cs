using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dash
{
    public class GameManager : MonoBehaviour
    {

        protected static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (GameManager)FindObjectOfType(typeof(GameManager));
                    if (instance == null)
                    {
                        var name = typeof(GameManager).Name;
                        Debug.LogFormat("Create singleton object: {0}", name);
                        GameObject obj = new GameObject(name);
                        instance = obj.AddComponent<GameManager>();
                    }
                }
                return instance;
            }
        }

        [SerializeField] PanelSwitcher panelSwitcher;
        [SerializeField] AnnounceUI announceUI;

        [SerializeField] AnnounceType currentAnnounceType;
        public AnnounceType CurrentAnnounceType
        {
            get { return currentAnnounceType; }
        }

        [SerializeField] float gameDuration = 60;
        public float GameDuration
        {
            get { return gameDuration; }
        }

        void Awake()
        {
            FadeManager.Instance.Fade(Color.white, new Color(1, 1, 1, 0), 1f, true, () =>
             {
                 StartCountdown();
             });
        }

        void Start()
        {
            StartCountdown();
        }

        #region game step

        public void StartCountdown()
        {
            panelSwitcher.SwitchPanel(BasePanel.PanelType.Start);
        }

        public void StartGame()
        {
            Debug.Log("start game");
            panelSwitcher.SwitchPanel(BasePanel.PanelType.Game);
        }

        public void FinishGame()
        {
            Debug.Log("finish game");
            panelSwitcher.ActivatePanel(BasePanel.PanelType.Finish);
            StartCoroutine(Finish());
        }

        IEnumerator Finish()
        {
            yield return new WaitForSeconds(1f);
            FadeManager.Instance.Fade(new Color(1, 1, 1, 0), Color.white, 1f, false, () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            });
        }

        #endregion

        void SetAnnounceType()
        {
            currentAnnounceType = (AnnounceType)Random.Range(0, 4);
            SaveData.SaveAnnounce(currentAnnounceType);
        }

        public void Announce()
        {
            announceUI.Show();
            if (Random.value < 0.1f)
                CharaSpawner.Instance.SpawnHage();
        }
    }
}