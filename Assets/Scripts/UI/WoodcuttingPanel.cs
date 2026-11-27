using Assets.Scripts.Core.Actions;
using Assets.Scripts.Skills;
using Assets.Scripts.Skills.Woodcutting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
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
                button.Setup(new WoodcuttingAction(tree));
            }
        }

        // Checked evey frame, for a smllaer UI this approach will be okay. With more UI, this will change to be updated by events
        private void Update()
        {
            levelText.text = $"Woodcutting:\n Level {skill.Level}";

            if (skill.Level >= ExperienceTable.MaxLevel)
            {
                xpText.text = $"{skill.Xp:N0} XP (max level)";
            }
            else
            {
                int nextLevelXp = ExperienceTable.GetXpForLevel(skill.Level + 1);
                xpText.text = $"{skill.Xp:N0} / {nextLevelXp:N0}";
            }

            xpBarFill.fillAmount = skill.ProgressToNextLevel;
        }
    }
}