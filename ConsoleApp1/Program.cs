
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Thorlabs.CCS_Series;


namespace CCS100_CSConsole
{

    public class Program
    {

        #region Fields
        private static TLCCS instrument = new TLCCS(IntPtr.Zero);
        #endregion



        static void Main(string[] args)
        {



        }



        private TLCCS ccsSeries;

        public Form1()
        {
            InitializeComponent();
        }

//        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            // release the device
//            if (ccsSeries != null)
//                ccsSeries.Dispose();
//        }

        private void button_StartScan_Click(object sender, EventArgs e)
        {
            if (textBox_SerialNumber.Text.Length == 0)
            {
                MessageBox.Show("Please insert the 8 numerics of the serial number");
                return;
            }

            // set the busy cursor
            this.Cursor = Cursors.WaitCursor;

            // connect the ccs device and start the sample c application. Read out the instrument resource name from the sample application
            // the instrument number 0x8081 is used for the CCS100 device. If you have another CCS instrument please change the number.
            // 0x8081: CCS100
            // 0x8083: CCS125
            // 0x8085: CCS150
            // 0x8087: CCS175
            // 0x8089: CCS200
            string instrumentNumber = "0x8089";
            string resourceName = "USB0::0x1313::" + instrumentNumber + "::M" + textBox_SerialNumber.Text.ToString() + "::RAW";

            // initialize device with the resource name (be sure the device is still connected)
            ccsSeries = new TLCCS(resourceName, false, false);

            int status;
            int res = ccsSeries.getDeviceStatus(out status);

            // set the integration time
            res = ccsSeries.setIntegrationTime((double)numericUpDown_IntegrationTime.Value);

            // start the scan
            res = ccsSeries.startScan();

            if (res == 0)
            {
                // color the button green
                button_StartScan.BackColor = System.Drawing.Color.LightGreen;
                button_StartScan.Enabled = false;
            }

            // reset the cursor
            this.Cursor = Cursors.Default;

            // has the device started?
            res = ccsSeries.getDeviceStatus(out status);

            // camera has data available for transfer
            if ((status & 0x0010) > 0)
            {
                double[] data = new double[3648];
                res = ccsSeries.getScanData(data);
            }

        }
    }
}



