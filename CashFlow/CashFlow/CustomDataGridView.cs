using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashFlow
{
    public partial class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Keys.Enter == keyData)
            {
                SendKeys.Send("{tab}");
                return true;
            }
            return base.ProcessDialogKey(keyData); 
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (13 == e.KeyValue)
            {
                SendKeys.Send("{tab}");
                return true;
            }
            return base.ProcessDataGridViewKey(e);
        }


        //public new System.Drawing.Rectangle GetCellDisplayRectangle(int columnIndex, int rowIndex, bool cutOverflow)
        //{
        //    try
        //    {
        //        return base.GetCellDisplayRectangle(columnIndex, rowIndex, cutOverflow);
        //    }
        //    catch (System.ArgumentOutOfRangeException)
        //    {
        //    }
        //}

    }


}
