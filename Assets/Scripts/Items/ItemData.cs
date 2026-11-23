using UnityEngine;

namespace Assets.Scripts.Items
{
    // Create definitions of each of the items (Normal Logs, Oak Logs, Sardines, Trout, Copper Ore, etc...)
    // Describes what an item is...

    [CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/Item")]
    public sealed class ItemData : ScriptableObject
    {
        [SerializeField, Tooltip("Data never changes.")]
        private string id;

        [SerializeField, Tooltip("Name shown to the player")]
        private string displayName;

        public string Id => id;
        public string DisplayName => displayName;
    }
}

