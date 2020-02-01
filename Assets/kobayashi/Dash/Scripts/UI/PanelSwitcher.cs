using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Dash
{
    public class PanelSwitcher : MonoBehaviour
    {

        void Awake()
        {
            SortPanelOrder();
        }

        void Start()
        {
            SwitchPanel(BasePanel.PanelType.Start);
        }

        Dictionary<BasePanel.PanelType, BasePanel> panelDict = new Dictionary<BasePanel.PanelType, BasePanel>();
        /// <summary>
        /// sort panelList order.
        /// </summary>
        void SortPanelOrder()
        {
            var panelList = GetComponentsInChildren<BasePanel>(true).ToList();

            panelList = panelList.OrderBy(eventer => eventer.MyType).ToList();
            foreach (var e in panelList)
            {
                if (!e.gameObject.activeSelf) e.gameObject.SetActive(false);
                panelDict[e.MyType] = e;
            }
        }

        public void ActivatePanel(BasePanel.PanelType panelType)
        {
            if (!HasPanel(panelType)) return;
            panelDict[panelType].Activate();
        }
        public void DeactivatePanel(BasePanel.PanelType panelType)
        {
            if (!HasPanel(panelType)) return;
            panelDict[panelType].Deactivate();
        }

        public void SwitchPanel(BasePanel.PanelType toPanelType)
        {
            if (!HasPanel(toPanelType)) return;
            var activePanelList = panelDict.Where(panel => panel.Value.IsActive).ToList();
            activePanelList.ForEach(panel => panel.Value.Deactivate());

            ActivatePanel(toPanelType);
        }

        bool HasPanel(BasePanel.PanelType key)
        {
            var hasPanel = panelDict.ContainsKey(key);
            if (!hasPanel)
            {
                Debug.LogError("error. panel switcher hasn't " + key + " panel.");
            }

            return hasPanel;
        }
    }
}