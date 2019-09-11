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

		[Route("[controller]/tobechecked")]
	    public ActionResult ToBeChecked() {
		    return View();
	    }

	    [Route("[controller]/checking/{roomId}")]
		public ActionResult Checking(int roomId)
	    {
		    return View(new CheckingModel(roomId));
	    }

	    [Route("[controller]/checkingdone/{roomId}")]
	    public ActionResult CheckingDone(int roomId) 
		{
		    var room = new Room(new RoomId(roomId.ToString()));
		    room.CheckingDone(_publisher);
			return RedirectToAction("tobechecked");
	    }

		[HttpPost]
	    [Route("[controller]/reportdamage/{roomId}")]
	    public ActionResult ReportDamage(int roomId, string description)
		{
		    var room = new Room(new RoomId(roomId.ToString()));
		    room.ReportDamage(_publisher, description);
			return RedirectToAction("tobechecked");
		}

	    [Route("[controller]/checkoutguest/{roomId}")]
		public ActionResult CheckoutGuest(int roomId)
	    {
		    var guest = new Guest();
		    guest.Checkout(_publisher, new RoomId(roomId.ToString()));
			return RedirectToAction("tobechecked");
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