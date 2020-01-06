using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;

namespace FuturaDataTCC.Utilitarios
{   
    class loading
    {
        static private frmLoading load; //Form Loading.
        static private Thread thread; //Thread para controle de loading.

        static public void showLoading(int x, int y)
        {

            load = new frmLoading();

            load.Location = new Point(x, y);

            thread = new Thread(showForThread);

            thread.Start();

        }

        static private void showForThread()
        {

            load.ShowDialog();

        }



        static public void stopLoading()
        {

            thread.Abort();

        }
    }
}
