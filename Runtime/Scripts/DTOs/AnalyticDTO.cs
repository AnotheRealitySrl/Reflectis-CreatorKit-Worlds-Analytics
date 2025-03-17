using UnityEngine;

namespace Reflectis.CreatorKit.Worlds.Analytics
{
    public class AnalyticDTO
    {
        [SerializeField] private EAnalyticVerb verb;
        [SerializeField] private int eventId;

        [SerializeField][SettableField] public object customAttributes;

        [SerializeField] private XAPIVerb xApiVerb;
        [SerializeField] private XAPIObject xApiObject;

        public EAnalyticVerb Verb { get => verb; set => verb = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public XAPIVerb XApiVerb { get => xApiVerb; set => xApiVerb = value; }
        public XAPIObject XApiObject { get => xApiObject; set => xApiObject = value; }
    }
}
