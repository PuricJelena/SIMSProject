using SimsProjekat.CompositeCommon;
using SimsProjekat.CompositeCommon.Enums;
using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimsProjekat.UI.Dialogs.Model
{
   public class ComboData<T> 
    {
       public string Name { get; set; }
        public T Value { get; set; }
    }
} 
