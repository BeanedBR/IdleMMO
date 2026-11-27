using Assets.Scripts.Core.Actions;
using Assets.Scripts.Skills.Woodcutting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TreeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image treeImage;

        [SerializeField] private TMP_Text detailsText;
        [SerializeField] private Image progressFill;
        

        private WoodcuttingAction action;

        // Called by WoodcuttingPanel straight after creating this button:
        public void Setup(WoodcuttingAction action)
        {
            this.action = action;
            nameText.text = action.Tree.DisplayName;
            treeImage.sprite = action.Tree.DisplayImage;
            button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            ActionManager.Instance.ToggleAction(action);
        }

        private void Update()
        {
            if (action == null) return;

            bool canStart = action.CanStart(out string reason);
            bool isActive = ActionManager.Instance.CurrentAction == action;

            button.interactable = canStart;

            TreeData tree = action.Tree;
            detailsText.text = canStart
                ? $"{tree.XPPerLog} XP / {tree.SecondsPerLog}s"
                : reason;

            progressFill.fillAmount = isActive ? action.SmoothProgress : 0f;
            button.image.color = isActive ? new Color32(81, 81, 81, 255) : new Color32(113, 113, 113, 255);
        }
    }

}
