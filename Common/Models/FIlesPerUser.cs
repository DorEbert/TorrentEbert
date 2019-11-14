using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torrent_Server_Side.Commom.Models
{
    [Table("FilesPerUser")]
    public class FilesPerUser
    {
        public User _User { get; set; }
        public List<FilesInfo> _FilesList { get; set; }

    }
}