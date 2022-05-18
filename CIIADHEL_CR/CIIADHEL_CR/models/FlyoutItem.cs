using System;

namespace CIIADHEL_CR.models
{
    //Menu data model
    internal class FlyoutItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetPage { get; set; }
    }
}
