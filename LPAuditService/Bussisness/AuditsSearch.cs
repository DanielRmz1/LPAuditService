using LPAuditService.Models;
using LPAuditService.Models.Auditing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPAuditService.Bussisness
{
    public class AuditsSearch
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        public List<Audit> Audits { get; set; }

        public AuditsSearch()
        {
            Audits = db.Audits.Include(x => x.AuditConfigs).ToList();
        }

        public void BuscarAuditoriasSinEventos()
        {
            foreach(var audit in Audits)
            {
                foreach(var config in audit.AuditConfigs)
                {
                    
                }
            }
        }

    }
}
