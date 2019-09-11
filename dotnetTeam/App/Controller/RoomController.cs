using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Controller
{
    public class RoomController : Microsoft.AspNetCore.Mvc.Controller
    {
	    private readonly IEventsPublisher _publisher;

	    public RoomController(IEventsPublisher publisher)
	    {
		    _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
	    }

        public ActionResult Index()
        {
            return View();
        }

		[Route("[controller]/ToBeChecked")]
	    public ActionResult ToBeChecked() {
		    return View();
	    }

	    [Route("[controller]/Checking/{roomId}")]
		public ActionResult Checking(int roomId)
	    {
		    return View(new CheckingModel(roomId));
	    }

	    [Route("[controller]/CheckingDone/{roomId}")]
	    public ActionResult CheckingDone(int roomId) 
		{
		    var room = new Room(new RoomId(roomId.ToString()));
		    room.CheckingDone(_publisher);
			return RedirectToAction("ToBeChecked");
	    }

		[HttpPost]
	    [Route("[controller]/ReportDamage/{roomId}")]
	    public ActionResult ReportDamage(int roomId, string description)
		{
		    var room = new Room(new RoomId(roomId.ToString()));
		    room.ReportDamage(_publisher, description);
			return RedirectToAction("ToBeChecked");
		}

	    [Route("[controller]/CheckoutGuest/{roomId}")]
		public ActionResult CheckoutGuest(int roomId)
	    {
		    var guest = new Guest();
		    guest.Checkout(_publisher, new RoomId(roomId.ToString()));
			return RedirectToAction("ToBeChecked");
		}
	}

	public class CheckingModel
	{
		public int RoomId { get; }

		public CheckingModel(int roomId)
		{
			RoomId = roomId;
		}
	}
}