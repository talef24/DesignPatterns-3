using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class Sorter
    {
        public Func<int, int, bool> ComparerMethod { get; set; }

        public Sorter(Func<int, int, bool> i_ComparerMethod)
        {
            ComparerMethod = i_ComparerMethod;
        }
        
        public void BubbleSort(Task[] i_Array)
        {
            bool arrayUnsorted = true;
            int lastIndexForSorting = i_Array.Length;

            while ((lastIndexForSorting > 1) && arrayUnsorted)
            {
                arrayUnsorted = bubble(i_Array, lastIndexForSorting--);
            }
        }

        private bool bubble(Task[] i_Array, int i_LastIndexForSorting)
        {
            bool isSwapped = false;

            for (int i = 1; i < i_LastIndexForSorting; i++)
            {
                if (ComparerMethod.Invoke(i_Array[i - 1].Description.Length, i_Array[i].Description.Length))
                {
                    swap(ref i_Array[i], ref i_Array[i - 1]);
                    isSwapped = true;
                }
            }

            return isSwapped;
        }

        private void swap(ref Task io_FirstElement, ref Task io_SecondElement)
        {
            Task temp = io_FirstElement;
            io_FirstElement = io_SecondElement;
            io_SecondElement = temp;
        }
    }
}
