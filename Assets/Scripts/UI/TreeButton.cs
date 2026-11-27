using Assets.Scripts.Skills.Woodcutting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Woodcutting
{
    public class TreeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text nameText;

        [SerializeField] private TMP_Text detailsText;
        [SerializeField] private Image progressFill;

        private WoodcuttingAction action;
    }

}
