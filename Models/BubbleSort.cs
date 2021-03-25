using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class BubbleSort
   {
      #region - Fields & Properties

      #endregion

      #region - Constructors
      public BubbleSort() { }
      #endregion

      #region - Methods
      public static void SortProperty<T>(IList<T> data, Func<T, int> sortFunction)
      {
         for (int i = 0; i < data.Count; i++)
         {
            if (i < data.Count - 1)
            {
               var itemA = data[i];
               var itemB = data[i + 1];

               if (sortFunction(itemA) > sortFunction(itemB))
               {
                  var tempA = data[i];
                  data[i] = data[i + 1];
                  data[i + 1] = tempA;
               }
            }
         }
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}
