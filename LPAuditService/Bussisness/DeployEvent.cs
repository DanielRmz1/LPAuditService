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
            var config = db.AuditConfigs.Include(x => x.int_Checklist).SingleOrDefault(x => x.int_IdAuditConfig == id_config);
            db.Periods.ToList();
            
            await AddEventByChecklist(config.int_Checklist, config);
        }

        private static async Task AddEventByChecklist(Checklist checklist, AuditConfig config)
        {
            switch (config.int_Period.chr_RepeatPeriod)
            {
                //case "o":
                //    break;
                case "d":
                    await AddWeeklyDailyEvents(checklist, config.dte_LastDateCreated, config.int_Period);
                    ModifyConfigLastDate(config, 30);
                    break;
                case "w":
                    await AddWeeklyDailyEvents(checklist, config.dte_LastDateCreated, config.int_Period);
                    ModifyConfigLastDate(config, 30);
                    break;
                case "m":
                    await AddMonthlyEvents(checklist, config.dte_LastDateCreated, config.int_Period);
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

        private static async Task AddWeeklyDailyEvents(Checklist checklist, DateTime dateTime, Period period)
        {
            try
            {
                var lastDate = (dateTime < DateTime.Now) ? DateTime.Now : dateTime;

                for (var day = lastDate; day <= lastDate.AddDays(30); day = day.AddDays(1))
                {
                    if (IsValidDay(day, period))
                    {
                        //var usersChecklist = db.UsersChecklists.Include(x=>x.User).Where(x => x.Checklist.int_IdList == checklist.int_IdList).ToList();

                        //foreach (var asotation in usersChecklist)
                        //{
                        //    AddEvent(checklist, day, asotation.User);
                        //}

                        await db.SaveChangesAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task AddMonthlyEvents(Checklist checklist, DateTime dateTime, Period period)
        {
            try
            {
                var today = (dateTime < DateTime.Now) ? DateTime.Now : dateTime;

                var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
                for (var month = firstDayOfCurrentMonth; month <= firstDayOfCurrentMonth.AddMonths(6); month = month.AddMonths(1))
                {
                    for(var day = month; day <= month.AddMonths(1); day = day.AddDays(1))
                    {
                        if (IsValidDay(day, period) && day >= DateTime.Now)
                        {
                            //var usersChecklist = db.UsersChecklists.Include(x => x.User).Where(x => x.Checklist.int_IdList == checklist.int_IdList).ToList();

                            //foreach (var asotation in usersChecklist)
                            //{
                             //   AddEvent(checklist, day, asotation.User);
                            //}
                            
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

        private static void AddEvent(Checklist checklist, DateTime scheduleDate, User user)
        {
            db.Events.Add(new Event() {
                Checklist_Id = checklist,
                chr_Title = "",
                dte_ScheduleDate = scheduleDate,
                int_State = 0,
                User = user
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
