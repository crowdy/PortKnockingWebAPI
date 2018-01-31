using PortKockLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortKockLib.Repositories
{
    interface IPortKnockReporitory
    {
        IList<PortKnockRecord> ListPortKnockRecords(uint items, uint page);
    }

    public class SqliteRepository: IPortKnockReporitory
    {
        IList<PortKnockRecord> IPortKnockReporitory.ListPortKnockRecords(uint items = 50, uint page = 10)
        {
            var retval = new List<PortKnockRecord>
            {
                new PortKnockRecord()
            };

            return retval;
        }
        
    }

    public class some
    {
        public object Func()
        {
            var repo = new SqliteRepository();
            return repo;

        }
    }
}
