using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Security.Cryptography;

using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Net.Mail;

namespace CandidatePortal.Shared.PortalUtilities
{
   public class PortalUtility : IPortalUtilities
    {
        #region Error Code
        public string ErrorCodeByIndex(int nIndex) =>
        nIndex switch
        {
            0 => "Data is saved successfully.",
            1 => "Data is updated successfully.",
            2 => "Record is deleted successfully.",
            3 => "Problem is occurred while adding record.",
            4 => "Problem is occurred while updating record.",
            5 => "Problem is occurred while deleting record.",
            6 => "Record is not found.",
            7 => "Credentials are not valid.",
            8 => "User is registered successfully.",
            9 => "User is registered successfully. Problem is occurred while sending mail.",
            10 => "Object is null.",
            11 => "Model object is not valid.",
            12 => "Email is already registered.",
            13 => "Record is found.",
            14 => "Candidate with education type already exists.",
            15 => "Record is found with same name.",
            16 => "Job is applied successfully.",
            17 => "Problem is occurred while applying job.",
            18 => "Candidate with same job is found.",
            19 => "Email does not exist.",
            20 => "Message is submitted successfully.",
            21 => "Problem is occurred while submitting message.",
            _ => ""
        };
        # endregion


        #region IP And MAC Adress
        public string GetHostName()
        {
            return Dns.GetHostName();
        }

        public string GetIP4Address()
        {
            string sResult = "";
            IPAddress[] ipaddress = Dns.GetHostAddresses(GetHostName());
            foreach (IPAddress ip4 in ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                sResult = ip4.ToString();
                break;
            }
            return sResult;
        }

        public string GetIP6Address()
        {
            string sResult = "";
            IPAddress[] ipaddress = Dns.GetHostAddresses(GetHostName());
            foreach (IPAddress ip6 in ipaddress.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6))
            {
                sResult = ip6.ToString();
                break;
            }
            return sResult;
        }

        public string GetMACAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
        }
        #endregion

        #region Uplpad & Delete
        public string GetUniqueFileName(IFormFile flUpload, string sWebrootPath, string sFolder, string sPrefix, string sSessionName, bool bThumbnails, int nWidth = 0, int nHeight = 0)
        {
            string sUniqueFileName = "";

            if (flUpload is not null)
            {
                string sLocation = Path.Combine(sWebrootPath, sFolder);
                long length = Directory.GetFiles(sLocation, "*.*", SearchOption.TopDirectoryOnly).Length + 1;
                sUniqueFileName = sPrefix + "_" + length.ToString() + "_" + Guid.NewGuid().ToString().Substring(0, 13) + Path.GetExtension(flUpload.FileName);
                string sFilePath = Path.Combine(sLocation, sUniqueFileName);
                using (FileStream fs = new FileStream(sFilePath, FileMode.Create))
                {
                    flUpload.CopyTo(fs);
                    //flUpload.CopyTo(new FileStream(sFilePath, FileMode.Create));
                }

            }
            return sUniqueFileName;
        }

        public string GetUniqueFileNameAngular(IFormFile flUpload, string sWebrootPath, string sFolder, string sPrefix, string sSessionName, bool bThumbnails, int nWidth = 0, int nHeight = 0)
        {
            string sUniqueFileName = "";

            string sLocation = sWebrootPath + sFolder;// Path.Combine();

            var filePath = Path.Combine(sWebrootPath + sFolder, Path.GetRandomFileName());
            using (var stream = System.IO.File.Create(filePath))
            {
                flUpload.CopyToAsync(stream);
            }
            long length = Directory.GetFiles(sLocation, "*.*", SearchOption.TopDirectoryOnly).Length + 1;

            sUniqueFileName = sPrefix + "_" + Guid.NewGuid().ToString().Substring(0, 13) + Path.GetExtension(flUpload.FileName);
            string sFilePath = Path.Combine(sLocation, sUniqueFileName);

            using (var stream = new FileStream(sFilePath, FileMode.Create))
            {
                flUpload.CopyToAsync(stream);
            }

            using (FileStream fs = new FileStream(sFilePath, FileMode.Create))
            {
                flUpload.CopyTo(fs);
                //flUpload.CopyTo(new FileStream(sFilePath, FileMode.Create));
            }
            return sUniqueFileName;
        }

        public void DeleteFileFromFolder(string sLocation)
        {
            FileInfo fi = new FileInfo(sLocation);
            if (fi.Exists)
                fi.Delete();
        }
        #endregion

        public string HashSHA1(string sValue)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(sValue);

            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        

        private static Random random = new Random((int)DateTime.Now.Ticks);
        public string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string CalculateRelativeTime(DateTime myTime) {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - myTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public bool DateCompare(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return false;
            }
            return true;
        }
    }
}
