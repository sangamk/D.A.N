using System;
using System.Collections.Generic;

namespace Assets.Engine.DataModels
{
    [Serializable]
    public class Planet
    {
        public string name;
        public string title;
        public string description;
        public TimelineEvent[] timeline;  
    }
}