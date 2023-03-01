using System;
namespace Pontaap.Studio
{
    public class EventManager  
    {
        /// <summary>
        /// Ateþleme tuþuna basýldýðýnda tetiklenecek fonksiyon.
        /// </summary>
        public static Action AFire;
        /// <summary>
        /// Score her güncellendiðinde bu fonksiyon tetiklecektir.
        /// </summary>
        public static Action AScore;
    }
}
