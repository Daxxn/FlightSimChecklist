using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ChecklistApp.Views
{
   public class EnumMarkup : MarkupExtension
   {
      private Type _enumType;
      public EnumMarkup() { }
      public EnumMarkup(Type type)
      {
         EnumType = type;
      }
      public override object ProvideValue(IServiceProvider serviceProvider)
      {
         if (EnumType is null) throw new InvalidOperationException("The EnumType must be specified.");

         Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
         Array enumValues = Enum.GetValues(actualEnumType);

         if (actualEnumType == this._enumType) return enumValues;

         Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
         enumValues.CopyTo(tempArray, 1);
         return tempArray;
      }

      public Type EnumType
      {
         get { return _enumType; }
         set
         {
            if (value != _enumType)
            {
               _enumType = value;
            }
         }
      }
   }
}
