using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DownloadFile
    {
        public double Size { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FileName { get; set; }
        public double BitRate { get; set; }

        public DownloadFile(string FileName, double Size)
        {
            this.FileName = FileName;
            this.Size = Size;
            StartTime = DateTime.Now;
        }

        public void SetEndTime()
        {
            EndTime = DateTime.Now;
            BitRate = Size / (EndTime - StartTime).TotalSeconds;
        }
    }
}
