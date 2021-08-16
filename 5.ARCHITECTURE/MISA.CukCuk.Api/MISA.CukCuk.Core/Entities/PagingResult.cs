using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class PagingResult<MISAEntity>
    {
        public PagingResult(int TotalRecord, List<MISAEntity> Data)
        {
            this.TotalRecord = TotalRecord;
            this.Data = Data;
        }

        public int TotalRecord { get; set; }

        public List<MISAEntity> Data { get; set; }
    }
}
