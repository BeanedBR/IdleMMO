using Assets.Scripts.Skills;
using Assets.Scripts.Skills.Woodcutting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Woodcutting
{
    public sealed class WoodcuttingPanel : MonoBehaviour
    {
        [Header("Skill Display")]
        [SerializeField, Tooltip("Tree assets to show, in order")]
        private TreeData[] trees;

        [SerializeField, Tooltip("TreeButton prefab")]
        private TreeButton treeButtonPrefab;

        [SerializeField, Tooltip("Parent object of the TreeButton prefabs (layout group)")]
        private Transform treeButtonContainer;

        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text xpText;
        [SerializeField] private Image xpBarFill;

        private Skill skill;

        private void Start()
        {
            skill = SkillManager.Instance.Get(SkillType.Woodcutting);

            // One button per tree each with it's own WoodcuttingAction:
            foreach (TreeData tree in trees)
            {
                if (tree == null) continue;

                TreeButton button = Instantiate(treeButtonPrefab, treeButtonContainer);
                //button.Setup(new WoodcuttingAction(tree));
            }
        }
    }
}