using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Thorlabs.CCS_Series;

namespace DotNETConsoleApp1
{
    internal class Program
    {

        #region Fields
        private static TLCCS instrument = new TLCCS(IntPtr.Zero);
        #endregion

        static void Main(string[] args)
        {

            Console.WriteLine("==============================================================");
            Console.WriteLine(" Thorlabs instrument driver sample for TLCCS series instruments");
            Console.WriteLine("==============================================================");


            // connect the ccs device and start the sample c application. Read out the instrument resource name from the sample application
            // the instrument number 0x8081 is used for the CCS100 device. If you have another CCS instrument please change the number.
            // 0x8081: CCS100
            // 0x8083: CCS125
            // 0x8085: CCS150
            // 0x8087: CCS175
            // 0x8089: CCS200

            string instrumentNumber = "0x8081";
            string resourceName = "USB0::0x1313::" + instrumentNumber + "::M" + "00233299" + "::RAW";

            instrument = new TLCCS(resourceName, false, false);


            int devicestatus;
            int res = instrument.getDeviceStatus(out devicestatus);

            if (0 != (devicestatus))
            {
                Console.WriteLine("Status 0");
            }
            else
            {
                Console.WriteLine("OK");
            }

            Console.WriteLine(">> Get Device Info <<");

                        StringBuilder manufacturerName = new StringBuilder();
            //            StringBuilder instrumentName = new StringBuilder();
            //            StringBuilder serialNumberWfs = new StringBuilder();
            //            StringBuilder serialNumberCam = new StringBuilder();
            //            StringBuilder driverVersion = new StringBuilder();

            //            instrument.identificationQuery(manufacturerName, instrumentName, serialNumberWfs, serialNumberCam, driverVersion);


            //short SelfTestResult;
            //StringBuilder SelfTestMessage = new StringBuilder();
            //instrument.selfTest(out SelfTestResult, SelfTestMessage);

            //Console.WriteLine(SelfTestMessage);


            //         Console.WriteLine(">> Opened Instrument <<");
            //         Console.Write("Manufacturer           : ");
            //        Console.WriteLine(manufacturerName);
            //        Console.Write("Instrument Name        : ");
            //        Console.WriteLine(instrumentName);
            //        Console.Write("Serial Number WFS      : ");
            //        Console.WriteLine(serialNumberWfs);


            double integration_time = 0.1;
            instrument.setIntegrationTime(integration_time);


            // start the scan
            res = instrument.startScan();


            double[] spectrumdata = new double[3648];
            res = instrument.getScanData(spectrumdata);


            for (int i = 0; i < spectrumdata.Length; i++)

            {
                Console.WriteLine(i.ToString() + ", " + Convert.ToString(spectrumdata[i]) + "\r\n");

            }


            res = instrument.getDeviceStatus(out devicestatus);

            if (0 != (devicestatus))
            {
                Console.WriteLine("Status 0");
            }
            else
            {
                Console.WriteLine("OK");
            }


            Console.ReadKey();
        }

    }
}

