using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Controller
{
    public class RoomController : Microsoft.AspNetCore.Mvc.Controller
    {
	    private readonly IRoomRepository _roomRepository;
	    private readonly IEventsPublisher _publisher;

	    public RoomController(IEventsPublisher publisher, IRoomRepository roomRepository )
	    {
		    _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
		    _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
	    }

        public ActionResult Index()
        {
            return View();
        }

        [Route("[controller]/checkin")]
        public ActionResult CheckIn() 
        {
	        return View();
        }

        [HttpPost]
        [Route("[controller]/checkin")]
        public async Task<ActionResult> CheckIn(string roomId)
        {
	        await _publisher.Publish(new RoomCheckedIn(new RoomId(roomId)));
	        return View();
        }

		[Route("[controller]/tobechecked")]
	    public async Task<ActionResult> ToBeChecked() 
		{
			var model = new ToBeCheckedModel(await _roomRepository.GetNotCheckedRoomIds());
		    return View(model);
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

		[Route("[controller]/toclean")]
		public async Task<ActionResult> ToClean()
		{
			return View((await _roomRepository.GetCheckedInRoomIds()).ToList());
		}

		[Route("[controller]/requestclean/{roomId}")]
		public ActionResult RequestClean(string roomId)
		{
			_publisher.Publish(new RoomCleaningRequested(new RoomId(roomId)));
			return RedirectToAction(nameof(ToClean));
		}
		
		[Route("[controller]/dirtyrooms")]
		public async Task<ActionResult> DirtyRooms() 
		{
			return View(await _roomRepository.GetDirtyRoomIds());
		}

		[Route("[controller]/cleaning/{roomId}")]
		public ActionResult Cleaning(string roomId)
		{
			return View(new RoomId(roomId));
		}

		[Route("[controller]/cleandone/{roomId}")]
		public ActionResult CleanDone(string roomId)
		{
			_publisher.Publish(new RoomCleaned(new RoomId(roomId)));
			return RedirectToAction(nameof(DirtyRooms));
		}

		[Route("[controller]/checkout")]
		public async Task<ActionResult> ToCheckedOut() 
		{
			return View(await _roomRepository.GetCheckInRoomIds());
		}

		[Route("[controller]/checkout/{roomId}")]
		public ActionResult CheckOut(string roomId)
		{
			_publisher.Publish(new GuestCheckedOut(new RoomId(roomId)));
			
			return RedirectToAction(nameof(ToCheckedOut));
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

	public class ToBeCheckedModel
	{
		public RoomId[] NotCheckedRoomIds { get; }

		public ToBeCheckedModel(RoomId[] notCheckedRoomIds)
		{
			NotCheckedRoomIds = notCheckedRoomIds;
		}
	}
}