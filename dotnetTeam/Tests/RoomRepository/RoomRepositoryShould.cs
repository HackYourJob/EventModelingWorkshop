using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.EventStore;
using Domain;
using NFluent;
using Xunit;

namespace Tests.RoomRepository
{
	public class RoomRepositoryShould
	{
		[Fact]
		public async Task ReturnEmptyArryIfNothingInEventStore()
		{
			var roomRepo = new App.RoomRepository(new FakeEventStore(Array.Empty<IDomainEvent>()));
			Check.That(await roomRepo.GetCheckedInRoomIds()).IsEmpty();
		}
	}

	public class FakeEventStore : IEventStore
	{
		private readonly IDomainEvent[] _expectedList;

		public FakeEventStore(IDomainEvent[] expectedList)
		{
			_expectedList = expectedList ?? throw new ArgumentNullException(nameof(expectedList));
		}

		public Task Append(IDomainEvent domainEvent)
		{
			throw new NotImplementedException();
		}

		public Task<IDomainEvent[]> GetAggregateHistory()
		{
			return Task.FromResult(_expectedList);
		}
	}
}
