using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.Mvc;
using Conference.Common;
using ConferenceManagement.Commands;
using ConferenceManagement.ReadModel;
using ECommon.IO;
using ENode.Commanding;

namespace Conference.Web.Admin.Controllers
{
    public class ConferenceController : Controller
    {
        private ICommandService _commandService;
        private ConferenceQueryService _conferenceQueryService;
        private ConferenceInfo _conference;

        public ConferenceController(ICommandService commandService, ConferenceQueryService conferenceQueryService)
        {
            _commandService = commandService;
            _conferenceQueryService = conferenceQueryService;
        }

        //static ConferenceController()
        //{
        //    Mapper.CreateMap<EditableConferenceInfo, ConferenceInfo>();
        //}

        //private ConferenceService service;

        //private ConferenceService Service
        //{
        //    get { return service ?? (service = new ConferenceService(MvcApplication.EventBus)); }
        //}

        //public ConferenceInfo Conference { get; private set; }

        // TODO: Locate and Create are the ONLY methods that don't require authentication/location info.

        /// <summary>
        /// We receive the slug value as a kind of cross-cutting value that 
        /// all methods need and use, so we catch and load the conference here, 
        /// so it's available for all. Each method doesn't need the slug parameter.
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var slug = (string)this.ControllerContext.RequestContext.RouteData.Values["slug"];
            if (!string.IsNullOrEmpty(slug))
            {
                this.ViewBag.Slug = slug;
                this._conference = _conferenceQueryService.FindConference(slug);

                if (this._conference != null)
                {
                    // check access
                    var accessCode = (string)this.ControllerContext.RequestContext.RouteData.Values["accessCode"];

                    if (accessCode == null || !string.Equals(accessCode, this._conference.AccessCode, StringComparison.Ordinal))
                    {
                        filterContext.Result = new HttpUnauthorizedResult("Invalid access code.");
                    }
                    else
                    {
                        this.ViewBag.OwnerName = this._conference.OwnerName;
                        this.ViewBag.WasEverPublished = this._conference.WasEverPublished;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        //#region Conference Details

        public ActionResult Locate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Locate(string email, string accessCode)
        {
            var conference = _conferenceQueryService.FindConference(email, accessCode);
            if (conference == null)
            {
                ModelState.AddModelError(string.Empty, "Could not locate a conference with the provided email and access code.");
                // Preserve input so the user doesn't have to type email again.
                ViewBag.Email = email;

                return View();
            }

            // TODO: This is not very secure. Should use a better authorization infrastructure in a real production system.
            return RedirectToAction("Index", new { slug = conference.Slug, accessCode });
        }

        //public ActionResult Index()
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(this.Conference);
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AsyncTimeout(5000)]
        public async Task<ActionResult> Create([Bind(Exclude = "Id,AccessCode,Seats,WasEverPublished")] ConferenceInfo conference)
        {
            if (!ModelState.IsValid) return View(conference);

            var command = new CreateConference();
            command.AggregateRootId = GuidUtil.NewSequentialId();
            command.Name = conference.Name;
            command.Description = conference.Description;
            command.Location = conference.Location;
            command.Tagline = conference.Tagline;
            command.TwitterSearch = conference.TwitterSearch;
            command.StartDate = conference.StartDate;
            command.EndDate = conference.EndDate;
            command.AccessCode = conference.AccessCode;
            command.OwnerName = conference.OwnerName;
            command.OwnerEmail = conference.OwnerEmail;
            command.Slug = conference.Slug;

            var result = await _commandService.ExecuteAsync(command, CommandReturnType.EventHandled);
            var commandResult = result.Data;

            if (commandResult.Status == CommandStatus.Failed)
            {
                ModelState.AddModelError("Slug", commandResult.ErrorMessage);
                return View(conference);
            }

            return RedirectToAction("Index", new { slug = conference.Slug, accessCode = conference.AccessCode });
        }

        //public ActionResult Edit()
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(this.Conference);
        //}

        //[HttpPost]
        //public ActionResult Edit(EditableConferenceInfo conference)
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var edited = Mapper.Map(conference, this.Conference);
        //        this.Service.UpdateConference(edited);
        //        return RedirectToAction("Index", new { slug = edited.Slug, accessCode = edited.AccessCode });
        //    }

        //    return View(this.Conference);
        //}

        //[HttpPost]
        //public ActionResult Publish()
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    this.Service.Publish(this.Conference.Id);

        //    return RedirectToAction("Index", new { slug = this.Conference.Slug, accessCode = this.Conference.AccessCode });
        //}

        //[HttpPost]
        //public ActionResult Unpublish()
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    this.Service.Unpublish(this.Conference.Id);

        //    return RedirectToAction("Index", new { slug = this.Conference.Slug, accessCode = this.Conference.AccessCode });
        //}

        //#endregion

        //#region Seat Types

        //public ViewResult Seats()
        //{
        //    return View();
        //}

        //public ActionResult SeatGrid()
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView(this.Service.FindSeatTypes(this.Conference.Id));
        //}

        //public ActionResult SeatRow(Guid id)
        //{
        //    return PartialView("SeatGrid", new SeatType[] { this.Service.FindSeatType(id) });
        //}

        //public ActionResult CreateSeat()
        //{
        //    return PartialView("EditSeat");
        //}

        //[HttpPost]
        //public ActionResult CreateSeat(SeatType seat)
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        seat.Id = GuidUtil.NewSequentialId();
        //        this.Service.CreateSeat(this.Conference.Id, seat);

        //        return PartialView("SeatGrid", new SeatType[] { seat });
        //    }

        //    return PartialView("EditSeat", seat);
        //}

        //public ActionResult EditSeat(Guid id)
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView(this.Service.FindSeatType(id));
        //}

        //[HttpPost]
        //public ActionResult EditSeat(SeatType seat)
        //{
        //    if (this.Conference == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            this.Service.UpdateSeat(this.Conference.Id, seat);
        //        }
        //        catch (ObjectNotFoundException)
        //        {
        //            return HttpNotFound();
        //        }

        //        return PartialView("SeatGrid", new SeatType[] { seat });
        //    }

        //    return PartialView(seat);
        //}

        //[HttpPost]
        //public void DeleteSeat(Guid id)
        //{
        //    this.Service.DeleteSeat(id);
        //}

        //#endregion

        //#region Orders

        //public ViewResult Orders()
        //{
        //    var orders = this.Service.FindOrders(this.Conference.Id);

        //    return View(orders);
        //}

        //#endregion
    }
}