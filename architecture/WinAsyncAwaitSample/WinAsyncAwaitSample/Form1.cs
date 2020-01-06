using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAsyncAwaitSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            tbxReturnOfAsync1.Text = "";
            tbxReturnOfAsync1.Refresh();
            //Here i Register the Start Date of the Hole Process - Running Async or Normal Way
            DateTime dateOfStart = DateTime.Now;
            string result = "";
            // Call the method that runs synchronously.
            //(1) This Method will run in a Thread, not Freezing the Cursor on TextBox. (2) The 1 million for will run faster because will not stop here.
            if (rdbRunSyncronous.Checked) {
                tbxReturnOfAsync1.Text = "The Process Starts Syncronous at " + DateTime.Now.ToString("mm:ss");
                tbxReturnOfAsync1.Refresh();
                result = WaitSynchronously();
            }

            // Choosing this and commetting other: Will Call the method that runs asynchronously.
            //(1) This Method will run in a Thread, not Freezing the Cursor on TextBox. (2) The 1 million for will run faster because will not stop here.
            else {
                tbxReturnOfAsync1.Text = "The Process Starts Asyncronous at " + DateTime.Now.ToString("mm:ss");
                result = await WaitAsynchronouslyAsync();
            }
            // Display the result.           
            
            
            var diffInSeconds = (DateTime.Now - dateOfStart).TotalSeconds;
            tbxReturnOfAsync1.Text = "The For Ends and takes total of " + diffInSeconds.ToString().Substring(0,5) + " seconds";
            tbxReturnOfAsync1.Refresh();

            tbxReturnOfAsync1.Text += result; //here occurs when the syncronous finish...
        }

        private async void btnGo2_Click(object sender, EventArgs e)
        {
            tbxReturnOfAsync2.Text = "";
            tbxReturnOfAsync2.Refresh();
            //Here i Register the Start Date of the Hole Process - Running Async or Normal Way
            DateTime dateOfStart = DateTime.Now;
            string result = "";
            // Call the method that runs synchronously.
            //(1) This Method will run in a Thread, not Freezing the Cursor on TextBox. (2) The 1 million for will run faster because will not stop here.
            if (rdbRunSyncronous2.Checked)
            {
                tbxReturnOfAsync2.Text = "The Process Starts Syncronous at " + DateTime.Now.ToString("mm:ss");
                tbxReturnOfAsync2.Refresh();
                result = WaitSynchronouslyFor();
            }

            // Choosing this and commetting other: Will Call the method that runs asynchronously.
            //(1) This Method will run in a Thread, not Freezing the Cursor on TextBox. (2) The 1 million for will run faster because will not stop here.
            else
            {
                tbxReturnOfAsync2.Text = "The Process Starts Asyncronous at " + DateTime.Now.ToString("mm:ss");
                result = await WaitAsynchronouslyAsyncFor();
            }
            // Display the result.           


            var diffInSeconds = (DateTime.Now - dateOfStart).TotalSeconds;
            tbxReturnOfAsync2.Text = "The For Ends and takes total of " + diffInSeconds.ToString().Substring(0, 5) + " seconds";
            tbxReturnOfAsync2.Refresh();

            tbxReturnOfAsync2.Text += result; //here occurs when the syncronous finish...
        }

        // The following method runs asynchronously. The UI thread is not
        // blocked during the delay. You can move or resize the Form1 window 
        // while Task.Delay is running.
        public async Task<string> WaitAsynchronouslyAsync()
        {
            await Task.Delay(10000);
            return "Finished";
        }

        // The following method runs synchronously, despite the use of async.
        // You cannot move or resize the Form1 window while Thread.Sleep
        // is running because the UI thread is blocked.
        public string WaitSynchronously()
        {
            // Add a using directive for System.Threading.
            Thread.Sleep(10000);
            return "Finished";
        }

        // The following method runs asynchronously. The UI thread is not
        // blocked during the delay. You can move or resize the Form1 window 
        // while Task.Delay is running.
        public async Task<string> WaitAsynchronouslyAsyncFor()
        {
            await Task.Run(() => {
                // Just loop.
                for (int i = 0; i < 5000000; i++)
                {
                    int a = i + new Random(100).Next(); //some randon math
                }
            });
            return "OK";
        }

        // The following method runs synchronously, despite the use of async.
        // You cannot move or resize the Form1 window while Thread.Sleep
        // is running because the UI thread is blocked.
        public string WaitSynchronouslyFor()
        {
            // Add a using directive for System.Threading.
            for (int i = 0; i < 5000000; i++)
            {
                int a = i + new Random(100).Next(); //some randon math
            }
            return "OK";
        }        
    }
}
