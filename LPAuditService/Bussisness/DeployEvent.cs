using LPAuditService.Models;
using LPAuditService.Models.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LPAuditService.Models.Checking;
using LPAuditService.Models.Calendar;
using LPAuditService.Models.Account;

namespace LPAuditService.Bussisness
{
    public static class DeployEvent
    {
        private static LayoutProcessContext db = new LayoutProcessContext();

        public static async Task Add(int id_config)
        {
            var config = db.AuditConfigs.Include(x=>x.int_Period).SingleOrDefault(x => x.int_IdAuditConfig == id_config);
            //db.Periods.ToList();
            
            await AddEventByChecklist(config);
        }

        private static async Task AddEventByChecklist(AuditConfig config)
        {
            switch (config.int_Period.chr_RepeatPeriod)
            {
                //case "o":
                //    break;
                case "d":
                    await AddWeeklyDailyEvents(config);
                    ModifyConfigLastDate(config, 30);
                    break;
                case "w":
                    await AddWeeklyDailyEvents(config);
                    ModifyConfigLastDate(config, 30);
                    break;
                case "m":
                    await AddMonthlyEvents(config);
                    ModifyConfigLastDate(config, 60);
                    break;
                case "q":
                    break;
            }
        }

        private static void ModifyConfigLastDate(AuditConfig config, int daysAdded)
        {
            config.dte_LastDateCreated = DateTime.Now.AddDays(daysAdded);
            db.Entry(config).State = EntityState.Modified;
            db.SaveChanges();
        }

        //private static async Task AddOnceEvents(Checklist checklist)
        //{
        //    try
        //    {
        //        var usersChecklist = db.UsersChecklists.Include(x => x.User).Where(x => x.Checklist.int_IdList == checklist.int_IdList).ToList();

        //        foreach (var asotation in usersChecklist)
        //        {
        //            AddEvent(checklist, DateTime.Now.AddDays(), asotation.User);
        //        }

        //        await db.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private static async Task AddWeeklyDailyEvents(AuditConfig config)
        {
            try
            {
                var lastDate = (config.dte_LastDateCreated < DateTime.Now) ? DateTime.Now : config.dte_LastDateCreated;

                for (var day = lastDate; day <= lastDate.AddDays(30); day = day.AddDays(1))
                {
                    if (IsValidDay(day, config.int_Period))
                    {
                        var usersInAudit = db.UsersAudits.Include(x => x.User).Where(x => x.AuditConfig.int_IdAuditConfig == config.int_IdAuditConfig).ToList();

                        foreach (var asotation in usersInAudit)
                        {
                            AddEvent(day, asotation.User, config);
                        }

                        await db.SaveChangesAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task AddMonthlyEvents(AuditConfig config)
        {
            try
            {
                var today = (config.dte_LastDateCreated < DateTime.Now) ? DateTime.Now : config.dte_LastDateCreated;

                var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
                for (var month = firstDayOfCurrentMonth; month <= firstDayOfCurrentMonth.AddMonths(6); month = month.AddMonths(1))
                {
                    for(var day = month; day <= month.AddMonths(1); day = day.AddDays(1))
                    {
                        if (IsValidDay(day, config.int_Period) && day >= DateTime.Now)
                        {
                            var usersInAudit = db.UsersAudits.Include(x => x.User).Where(x => x.AuditConfig.int_IdAuditConfig == config.int_IdAuditConfig).ToList();

                            foreach (var asotation in usersInAudit)
                            {
                                AddEvent(day, asotation.User, config);
                            }

                            break;
                        }
                    }

                    await db.SaveChangesAsync();

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void AddQuincEvents()
        {

        }

        private static void AddEvent(DateTime scheduleDate, User user, AuditConfig config)
        {
            db.Events.Add(new Event() {
                chr_Title = "",
                dte_ScheduleDate = new DateTime(scheduleDate.Year, scheduleDate.Month, scheduleDate.Day),
                int_State = 0,
                User = user,
                AuditConfig = config
            });
        }

        private static bool IsValidDay(DateTime day, Period period)
        {
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (period.bit_Mon) return true;
                    break;
                case DayOfWeek.Tuesday:
                    if (period.bit_Tue) return true;
                    break;
                case DayOfWeek.Wednesday:
                    if (period.bit_Wed) return true;
                    break;
                case DayOfWeek.Thursday:
                    if (period.bit_Thu) return true;
                    break;
                case DayOfWeek.Friday:
                    if (period.bit_Fri) return true;
                    break;
                case DayOfWeek.Saturday:
                    if (period.bit_Sat) return true;
                    break;
                case DayOfWeek.Sunday:
                    if (period.bit_Sun) return true;
                    break;
            }

            return false;
        }
    }
}
