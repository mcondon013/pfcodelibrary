using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace PFSystemObjects
{
    /// <summary>
    /// Class for retrieving various kinds of system information.
    /// </summary>
    public class SysInfo
    {
        /// <summary>
        /// Structure used for storing memory information.
        /// </summary>
        public struct pfMemoryInfo
        {
#pragma warning disable 1591
            public long TotalPhysicalMemory;
            public long TotalVisibleMemory;
            public long TotalFreeMemory;
            public long TotalCommittedMemory;
#pragma warning restore 1591
        }
        
        
        //properties
        /// <summary>
        /// Total amount of memory on the computer.
        /// </summary>
        public static long TotalMemory
        {
            get
            {
                ManagementClass osInfo = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection instances = osInfo.GetInstances();
                long nValue = -1;

                foreach (ManagementObject instance in instances)
                {
                    nValue = Convert.ToInt64(instance["TotalVisibleMemorySize"].ToString());

                }

                return nValue;
            }
                
        }

        /// <summary>
        /// Total amount of free memory on the computer.
        /// </summary>
        public static long TotalFreeMemory
        {
            get
            {
                ManagementClass osInfo = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection instances = osInfo.GetInstances();
                long nValue = -1;

                foreach (ManagementObject instance in instances)
                {
                    nValue = Convert.ToInt64(instance["FreePhysicalMemory"].ToString());
                }

                return nValue;
            }

        }

        /// <summary>
        /// Total amount of committed memory on the computer.
        /// </summary>
        public static long TotalCommittedMemory
        {
            get
            {
                ManagementClass osInfo = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection instances = osInfo.GetInstances();
                long nTotalMemory = -1;
                long nFreeMemory = -1;
                long nCommittedMemory = -1;

                foreach (ManagementObject instance in instances)
                {
                    nTotalMemory = Convert.ToInt64(instance["TotalVisibleMemorySize"].ToString());
                    nFreeMemory = Convert.ToInt64(instance["FreePhysicalMemory"].ToString());
                    nCommittedMemory = nTotalMemory - nFreeMemory;
                }


                return nCommittedMemory;
            }

        }

        /// <summary>
        /// Percentage of total memory currently in use.
        /// </summary>
        public static long PercentMemoryUsage
        {
            get
            {
                ManagementClass osInfo = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection instances = osInfo.GetInstances();
                long nTotalMemory = -1;
                long nFreeMemory = -1;
                long nCommittedMemory = -1;
                long nPercentMemoryUsed = -1;
                float nTemp = (float)0.0;

                foreach (ManagementObject instance in instances)
                {
                    nTotalMemory = Convert.ToInt64(instance["TotalVisibleMemorySize"].ToString());
                    nFreeMemory = Convert.ToInt64(instance["FreePhysicalMemory"].ToString());
                    nCommittedMemory = nTotalMemory - nFreeMemory;
                    if(nTotalMemory!=0)
                    {
                        nTemp=(float)nCommittedMemory/(float)nTotalMemory;
                        nPercentMemoryUsed = (long)(nTemp * 100.0);
                    }
                    else
                    {
                        nPercentMemoryUsed=0;
                    }
                }


                return nPercentMemoryUsed;
            }

        }

        //methods

        /// <summary>
        /// Gets information on memory usage for the computer.
        /// </summary>
        /// <returns>pfMemoryInfo object that contains results.</returns>
        public static pfMemoryInfo  GetMemoryInfo()
        {
            pfMemoryInfo memoryInfo = new pfMemoryInfo();
            ManagementClass osInfo = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection osInstances = osInfo.GetInstances();
            ManagementClass computerInfo = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection computerInstances = computerInfo.GetInstances();
            long nTotalMemory = -1;
            long nFreeMemory = -1;
            long nCommittedMemory = -1;
            long nComputerPhysicalMemory = -1;
 
            foreach (ManagementObject instance in osInstances)
            {
                nTotalMemory = Convert.ToInt64(instance["TotalVisibleMemorySize"].ToString());
                nFreeMemory = Convert.ToInt64(instance["FreePhysicalMemory"].ToString());
                nCommittedMemory = nTotalMemory - nFreeMemory;
                memoryInfo.TotalVisibleMemory = nTotalMemory;
                memoryInfo.TotalFreeMemory = nFreeMemory;
                memoryInfo.TotalCommittedMemory = nCommittedMemory;
            }

            foreach (ManagementObject instance in computerInstances)
            {
                nComputerPhysicalMemory=Convert.ToInt64(instance["TotalPhysicalMemory"].ToString());
                memoryInfo.TotalPhysicalMemory = nComputerPhysicalMemory;
            }

            return memoryInfo;

        }

        /// <summary>
        /// Routine to report whether or not a default printer exists for the system.
        /// </summary>
        /// <returns>Returns false if the default printer is not defined.</returns>
        public static bool DefaultPrinterIsDefined()
        {
            bool defaultPrinterSet = true;

            System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            //"default printer is not set"
            if (String.IsNullOrEmpty(settings.PrinterName))
            {
                defaultPrinterSet = false;
            }
            else if (settings.PrinterName.ToLower().Contains("default printer is not set"))
            {
                defaultPrinterSet = false;
            }

            return defaultPrinterSet;
        }

        /// <summary>
        /// Routine to retrieve the name of the default printer.
        /// </summary>
        /// <returns>String value representing the name of the printer.</returns>
        public static string GetDefaultPrinterName()
        {

            System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            return settings.PrinterName;

        }

        /// <summary>
        /// Routine to get a list of all the printers defined for the system.
        /// </summary>
        /// <returns>If any printers exist, returns a string array containing their names.
        ///  Returns null if no printers defined for the system.</returns>
        public static string[] GetPrinterNameList()
        {
            List<string> installedPrintersList = new List<string>();
            string[] installedPrinterNames;

            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                installedPrintersList.Add(printer);
            }

            if (installedPrintersList.Count > 0)
            {
                installedPrinterNames = installedPrintersList.ToArray();
            }
            else
            {
                installedPrinterNames = null;
            }
            return installedPrinterNames;
        }

    }//end class
}//end namespace
