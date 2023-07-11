
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NcPac_Project2.Data;
using NcPac_Project2.Models;
using NuGet.DependencyResolver;
using System.Diagnostics;
using System.Security.Claims;

namespace NcPac_Project2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NC_PAC_Context _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(ILogger<HomeController> logger,SignInManager<IdentityUser> signInManager, NC_PAC_Context context)
        {
            _logger = logger;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            //Meeting query one month in future
            #region
            var meetings = _context.Meetings
                .Include(m => m.MeetingMinutes)
                .Include(m => m.Committee)
                .Where(e => e.MeetingDate > DateTime.Today && e.MeetingDate < DateTime.Today.AddMonths(1))
                .OrderBy(e => e.MeetingDate)
                .AsNoTracking();

            var meetingsYearly = _context.Meetings
                .Include(m => m.MeetingMinutes)
                .Include(m => m.Committee)
                .Where(e => e.MeetingDate.Value.Year == DateTime.Today.Year)
                .OrderBy(e => e.MeetingDate)
                .AsNoTracking();

            //Count new meetings
            int newMeetings = meetings.Count();

            //Count yearly meetings
            int pastMeetings = meetingsYearly.Count();

            ViewBag.Meetings = meetings;
            ViewBag.NewMeetings = newMeetings;
            ViewBag.PastMeetings = pastMeetings;


            //Member Meetings one month in future
            if (_signInManager.IsSignedIn(User) && User.IsInRole("User"))
            {
                //Current Month
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var memMeetings = _context.Meetings
                .Include(m => m.MeetingMinutes)
                .Include(m => m.Committee)
                .Include(m => m.Attendances)
                .Where(e => e.MeetingDate > DateTime.Today && e.MeetingDate < DateTime.Today.AddMonths(1)
                && e.Attendances.Any(a => a.Member.MemberEmail == userEmail))
                .OrderBy(e => e.MeetingDate)
                .AsNoTracking();

                //Count Members new meetings
                int newMemMeetings = memMeetings.Count();
                ViewBag.MemMeetings = memMeetings;
                ViewBag.NewMemMeetings = newMemMeetings;
                
            }
            if (_signInManager.IsSignedIn(User) && User.IsInRole("User"))
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                //Yearly
                var memMeetingsYearly = _context.Meetings
                .Include(m => m.MeetingMinutes)
                .Include(m => m.Committee)
                .Include(m => m.Attendances)
                .Where(e => e.MeetingDate.Value.Year == DateTime.Today.Year && e.MeetingDate <= DateTime.Today
                    && e.Attendances.Any(a => a.Member.MemberEmail == userEmail))
                .OrderBy(e => e.MeetingDate)
                .AsNoTracking();

                //Count Meetings attended year to date
                int pastMemMeetings = memMeetingsYearly.Count();

                ViewBag.PastMemMeetings = pastMemMeetings;
            }
                
            #endregion

            //Members query
            #region
            //store renewal date 1 month in the future
            DateTime renewalDate = DateTime.Today.AddMonths(1);

            //store renewal date 3 months in the future
            DateTime renewalDate3 = DateTime.Today.AddMonths(3);

            var membersRenewal = _context.Members
                .Include(m => m.MemberCommitteeEnrolls).ThenInclude(m => m.Committee).ThenInclude(m => m.Division)
                .Where(m => m.MemberRenewalDate.Value.Year == renewalDate.Year && m.MemberRenewalDate.Value.Month 
                            == renewalDate.Month && m.MemberRenewalDate.Value.Day == renewalDate.Day)
                .OrderBy(m => m.MemberLastName)
                .AsNoTracking();

            var membersRenewal3 = _context.Members
                .Include(m => m.MemberCommitteeEnrolls).ThenInclude(m => m.Committee).ThenInclude(e => e.Division)
                .Where(e => e.MemberRenewalDate.Value.Year == renewalDate.Year && e.MemberRenewalDate.Value.Month
                            == renewalDate.Month && e.MemberRenewalDate.Value.Day == renewalDate.Day)
                .OrderBy(m => m.MemberLastName)
                .AsNoTracking();

            //Members Last Month
            var members = _context.Members
                .Include(m => m.MemberCommitteeEnrolls).ThenInclude(e => e.Committee).ThenInclude(e => e.Division)
                .Where(e => e.MemberRegisteredAt > DateTime.Today.AddMonths(-1))
                .OrderBy(m => m.MemberLastName)
                .AsNoTracking();

            //Count new members
            int newMembers = members.Count();
            //Count no roles assigned
            int membersNoEnrolls = members.Where(m => m.MemberCommitteeEnrolls == null || !m.MemberCommitteeEnrolls.Any()).Count();
            //Count renewals
            int membersRenewalDates = members.Where(m => m.MemberRenewalDate == renewalDate).Count();
            //Count renewals 3 months
            int membersRenewalDates3 = members.Where(m => m.MemberRenewalDate == renewalDate).Count();

            ViewBag.Members = members;
            ViewBag.NewMembers = newMembers;
            ViewBag.MembersNoEnrolls = membersNoEnrolls;
            ViewBag.MembersRenewal = membersRenewalDates;
            ViewBag.MembersRenewal3 = membersRenewalDates3;
            #endregion

            //Action Items query
            #region
            var tasks = _context.ActionItems
                .Include(t => t.Meeting)
                .Include(t => t.Member)
                .Where(e => e.ActionDueDate > DateTime.Today && e.ActionDueDate < DateTime.Today.AddMonths(1))
                .OrderBy(e => e.ActionDueDate)
                .AsNoTracking();

            int total = tasks.Count();
            int completedTasks = tasks.Count(e => e.IsCompleted);
            
            //Calculate percentage
            double completed = total == 0 ? 0 : (double)completedTasks / total * 100;

            //Count new action items
            int newActionItems = tasks.Count();
            //Count completed action items
            double tasksCompletedPercent = completed;

            ViewBag.Tasks = tasks;
            ViewBag.ActionItems = newActionItems;
            ViewBag.TasksCompletedPercent = tasksCompletedPercent;
            #endregion

            //Member Action Items
            #region
            if (_signInManager.IsSignedIn(User) && User.IsInRole("User"))
            {
                var memtasks = _context.ActionItems
                    .Include(t => t.Meeting).ThenInclude(t => t.Committee)
                    .Include(t => t.Member)
                    .Where(e => e.Member.MemberEmail == User.Identity.Name)
                    .AsNoTracking();

                ViewBag.MemTasks = memtasks;
            }

            //Get completed percentage for user Action Item current month
            if (_signInManager.IsSignedIn(User) && User.IsInRole("User"))
            {
                var today = DateTime.Today;
                var memtasksMonthly = _context.ActionItems
                    .Include(t => t.Meeting).ThenInclude(t => t.Committee)
                    .Include(t => t.Member)
                    .Where(e => e.Member.MemberEmail == User.Identity.Name
                             && e.ActionDueDate.Value.Month == today.Month
                             && e.ActionDueDate.Value.Year == today.Year)
                    .AsNoTracking();

                var completedTasksCount = memtasksMonthly.Count(e => e.IsCompleted);
                var totalTasks = memtasksMonthly.Count();
                var percentageCompleted = totalTasks == 0 ? 0 : (double)completedTasksCount / totalTasks * 100;

                ViewBag.MemtasksMonthly = memtasksMonthly;
                ViewBag.MemCompletedTasksCount = completedTasksCount;
                ViewBag.MemTotalTasksCount = totalTasks;
                ViewBag.MemPercentageCompleted = percentageCompleted;
            }

            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}