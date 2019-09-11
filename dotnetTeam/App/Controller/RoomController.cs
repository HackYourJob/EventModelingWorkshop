using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace App.Controller
{
    public class RoomController : Microsoft.AspNetCore.Mvc.Controller
    {
	    private readonly IEventsPublisher _publisher;

	    public RoomController(IEventsPublisher publisher)
	    {
		    _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
	    }

        // GET: Room
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

	    [Route("[controller]/CheckingDone/{roomId}/{status}")]
	    public ActionResult CheckingDone(int roomId, RoomCheckStatus status) 
		{
		    var room = new Room(new RoomId(roomId.ToString()));
		    room.CheckingDone(_publisher, status);
			return View(status);
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