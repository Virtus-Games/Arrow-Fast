using System;
namespace Pontaap.Studio
{
    public class EventManager  
    {
        /// <summary>
        /// Ate�leme tu�una bas�ld���nda tetiklenecek fonksiyon.
        /// </summary>
        public static Action AFire;
        /// <summary>
        /// Score her g�ncellendi�inde bu fonksiyon tetiklecektir.
        /// </summary>
        public static Action AScore;
    }
}
