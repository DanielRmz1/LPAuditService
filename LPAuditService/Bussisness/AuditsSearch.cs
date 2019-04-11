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
            Audits = db.Audits.Include(x=>x.AuditConfigs).Where(x=>x.bit_Activo).ToList();
        }

        public async void BuscarAuditoriasSinEventos()
        {
            foreach(var audit in Audits)
            {
                foreach(var config in audit.AuditConfigs)
                {
                    if(config.dte_LastDateCreated < DateTime.Now || (config.dte_LastDateCreated - DateTime.Now).Days <= 15)
                    {
                        await DeployEvent.Add(config.int_IdAuditConfig);
                    }
                }
            }

            await DropDesactivatedAudits();
            await UpdateNotAnsweredEvents();
        }

        public async Task DropDesactivatedAudits()
        {
            try
            {
                var desactivatedAuditsConfigs = db.AuditConfigs.Where(x => !x.Audit.bit_Activo).ToList();

                if(desactivatedAuditsConfigs.Count > 0)
                {
                    var now = DateTime.Now;
                    var today = new DateTime(now.Year, now.Month, now.Day);

                    var idConfigs = desactivatedAuditsConfigs.Select(x => x.int_IdAuditConfig).ToArray();

                    var desactivatedAuditsEvents = db.Events.Where(x => (idConfigs.Contains(x.AuditConfig.int_IdAuditConfig)) && (x.dte_ScheduleDate >= today) && x.int_State != 2).ToList();

                    if (desactivatedAuditsEvents.Count > 0)
                    {
                        foreach (var item in desactivatedAuditsConfigs)
                            item.dte_LastDateCreated = DateTime.Now;

                        db.Events.RemoveRange(desactivatedAuditsEvents);                            

                        await db.SaveChangesAsync();
                    }
                } 
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task UpdateNotAnsweredEvents()
        {
            try
            {

                var now = DateTime.Now;
                var today = new DateTime(now.Year, now.Month, now.Day);

                var notAnsweredEvents = db.Events.Where(x => x.dte_ScheduleDate < today && x.int_State == 0).ToList();

                if (notAnsweredEvents.Count > 0)
                {

                    foreach (var e in notAnsweredEvents)
                    {
                        e.int_State = 1;
                        db.Entry(e).State = EntityState.Modified;
                    }

                    await db.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
