using Assets.Data.Items;
using UnityEngine;

namespace Assets.Scripts.Skills.Woodcutting
{
    // Creates definitions of a tree (normal tree, oak tree, etc..) and holds it's attributes.

    [CreateAssetMenu(fileName = "NewTree", menuName = "Scriptable Objects/Woodcutting/Tree")]
    public class TreeData : ScriptableObject
    {
        [SerializeField, Tooltip("Id never changes.")]
        private string id;

        [SerializeField, Tooltip("Name shown to the player.")]
        private string displayName;

        [SerializeField, Min(1), Tooltip("Level required to cut this tree.")]
        private int levelRequired = 1;

        [SerializeField, Min(0.05f), Tooltip("How many seconds it takes to cut the tree.")]
        private float secondsPerLog = 3f;

        [SerializeField, Min(0), Tooltip("How much XP the player gains from cutting the tree.")]
        private int xpPerLog = 10;

        [SerializeField, Tooltip("The log item the tree gives after the player cuts it.")]
        private ItemData logItem;

        public string Id => id;
        public string DisplayName => displayName;
        public int LevelRequired => levelRequired;
        public float SecondsPerLog => secondsPerLog;
        public int XPPerLog => xpPerLog;
        public ItemData LogItem => logItem;
    }
 }
