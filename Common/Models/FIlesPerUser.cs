using System.Collections.Generic;

namespace Torrent_Server_Side.Commom.Models
{
    public class FilesPerUser
    {
        public User _User { get; set; }
        public List<FilesInfo> _FilesList { get; set; }

    }
}