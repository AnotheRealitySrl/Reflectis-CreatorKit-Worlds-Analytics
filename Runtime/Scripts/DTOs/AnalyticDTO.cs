using UnityEngine;

namespace Reflectis.CreatorKit.Worlds.Analytics
{
    public class AnalyticDTO
    {
        [SerializeField] private EAnalyticVerb verb;
        [SerializeField] private int eventId;

        [SerializeField][SettableField] public object customAttributes;

        public EAnalyticVerb Verb { get => verb; set => verb = value; }
        public int EventId { get => eventId; set => eventId = value; }
    }
}
