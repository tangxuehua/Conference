using System;
using System.Web.Mvc;
using Registration.ReadModel;

namespace Conference.Web.Public.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IConferenceQueryService _conferenceQueryService;

        public ConferenceController(IConferenceQueryService conferenceQueryService)
        {
            _conferenceQueryService = conferenceQueryService;
        }

        public ActionResult Display(string slug)
        {
            return View(_conferenceQueryService.GetConferenceDetails(slug));
        }
    }
}
