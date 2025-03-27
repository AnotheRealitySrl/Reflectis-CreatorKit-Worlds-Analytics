using UnityEngine;

namespace Reflectis.CreatorKit.Worlds.Analytics
{
    public class AnalyticDTO
    {
        [SerializeField] private EAnalyticVerb verb;
        [SerializeField] private int eventId;

        [SerializeField][SettableField] public object customAttributes;

        [SerializeField] private XAPIStatement statement;

        [SerializeField] private string locale;

        [SerializeField] private string context;

        public EAnalyticVerb Verb { get => verb; set => verb = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public string Locale { get => locale; set => locale = value; }
        public string Context { get => context; set => context = value; }
        public XAPIStatement Statement { get => statement; set => statement = value; }
    }
}
