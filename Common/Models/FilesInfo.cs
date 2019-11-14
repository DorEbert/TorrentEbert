using System.ComponentModel.DataAnnotations.Schema;
namespace Torrent_Server_Side.Commom.Models
{
    [Table("FilesInfo")]
    public class FilesInfo
    {
        public int    Files_ID { get; set; }
        public string FileName { get; set; }
        public double Size     { get; set; }
        public string Type     { get; set; }
        public string Location { get; set; }
        public int    Amount_Of_Peers    { get; set; }
    }
}