using System.Collections.Generic;
using Assets.Scripts.Core.Actions;
using Assets.Data.Items;
using Assets.Scripts.Skills;
using Assets.Scripts.Skills.Woodcutting;
using UnityEngine;
using Unity.VisualScripting;
using System;

namespace Assets.Scripts.Debugging
{
    public sealed class WoodcuttingTester : MonoBehaviour
    {
        [SerializeField, Tooltip("Tree assets to test with")]
        private TreeData[] trees;

        [SerializeField, Tooltip("Position in the trees list used by 'Start Selected Tree' (0 = first tree, 1 = second tree, etc...)")]
        private int selectedTree;

        [SerializeField] private bool startFirstTreeOnPlay = true;

        // One action per tree in the same order as the trees list:
        private WoodcuttingAction[] actions;

        private void Awake()
        {
            actions = new WoodcuttingAction[trees.Length];
            for (int i = 0; i < trees.Length; i++)
            {
                if (trees[i] != null) actions[i] = new WoodcuttingAction(trees[i]);
            }
        }

        private void Start()
        {
            if (startFirstTreeOnPlay) StartTree(0);
        }

        [ContextMenu("Start Selected Tree")]
        private void StartSelectedTree() => StartTree(selectedTree);

        [ContextMenu("Stop Action")]
        private void StopAction() => ActionManager.Instance.StopAction();

        [ContextMenu("Add 1,000 Woodcutting XP")]
        private void AddXP()
        {
            Skill woodcutting = SkillManager.Instance.Get(SkillType.Woodcutting);
            int levelsGained = woodcutting.AddXp(1_000);
            Debug.Log($"+1,000 XP: gained {levelsGained} levels, Woodcutting is now level {woodcutting.Level}");

        }

        [ContextMenu("Print Skills")]
        private void PrintSkills()
        {
            foreach (Skill skill in SkillManager.Instance.All)
            {
                int nextLevelXp = ExperienceTable.GetXpForLevel(skill.Level + 1);

                Debug.Log($"{skill.Type}: level {skill.Level}, {skill.Xp} XP " +
                        $"(next level at {nextLevelXp} XP)");
            }
        }

        [ContextMenu("Print Bank")]
        private void PrintBank()
        {
            if (Bank.Instance.Items.Count == 0)
            {
                Debug.Log("Bank is empty");
                return;
            }

            foreach (KeyValuePair<ItemData, int> entry in Bank.Instance.Items)
            {
                Debug.Log($"Bank: {entry.Key.DisplayName} x{entry.Value}");
            }
        }

        private void StartTree(int index)
        {
            if (index < 0 || index >= actions.Length || actions[index] == null)
            {
                Debug.LogWarning($"No tree at position {index} in the trees list.");
                return;
            }

            ActionManager.Instance.StartAction(actions[index]);
        }
    }
}