using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitExcercise
{
    public class ProgressModel
    {
        public int PercentageRead { get; set; } = 0;
        public List<FileDataModel> ReadCompletedFiles { get; set; } = new List<FileDataModel>();
    }
}
