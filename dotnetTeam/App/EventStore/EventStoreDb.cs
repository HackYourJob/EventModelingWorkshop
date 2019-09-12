using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.EventStore
{
    public class EventStoreDb : IEventStore
    {
        private static IEventStoreConnection _connection;
        private static UserCredentials _userCredentials;
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private bool _connected;

        public EventStoreDb(string storeIpAdress)
        {
            _userCredentials = new UserCredentials("admin", "changeit");
            var settings = ConnectionSettings.Create()
                .UseConsoleLogger()
                .SetDefaultUserCredentials(_userCredentials)
                .KeepReconnecting()
                .KeepRetrying();

            _connection = EventStoreConnection.Create(
                settings,
                new IPEndPoint(IPAddress.Parse(storeIpAdress), 1113));

            _connection.Connected += OnConnected;
            _connection.Disconnected += OnDisconnected;
            _connection.ConnectAsync();
        }

        private void OnConnected(object sender, ClientConnectionEventArgs e)
        {
            _connected = true;
        }

        private void OnDisconnected(object sender, ClientConnectionEventArgs e)
        {
            _connected = false;
        }

        public async Task Append(IDomainEvent domainEvent)
        {
            var json = JsonConvert.SerializeObject(domainEvent, SerializerSettings);
            var bytes = Encoding.UTF8.GetBytes(
                json);
            var eventData = new EventData(
                Guid.NewGuid(),
                MappingEventTypeToKey[domainEvent.GetType()],
                true,
                bytes,
                new byte[0]);

            await _connection.AppendToStreamAsync(
                "cleanHotelStream",
                ExpectedVersion.Any,
                eventData);
        }

        public async Task<IDomainEvent[]> GetAggregateHistory()
        {
            var allevents = await _connection.ReadAllEventsBackwardAsync(Position.End, 4096, false, _userCredentials);
            return allevents.Events.Where(e => MappingKeyToEventType .ContainsKey(e.Event.EventType)).Select(ToDomainEvent).ToArray();
        }

        private IDomainEvent ToDomainEvent(ResolvedEvent e)
        {
            var payload = Encoding.UTF8.GetString(e.Event.Data);
            var domainEvent = (IDomainEvent)JsonConvert.DeserializeObject(payload, MappingKeyToEventType[e.Event.EventType]);
            return domainEvent;
        }
        
        private static readonly IDictionary<string, Type> MappingKeyToEventType = new Dictionary<string, Type>()
        {
            ["roomCheckedAsOk"] = typeof(RoomCheckedAsOk),
            ["roomDamageReported"] = typeof(RoomDamageReported),
            ["roomCleaningRequested"] = typeof(RoomCleaningRequested),
            ["guestCheckedOut"] = typeof(GuestCheckedOut),
            ["roomCheckedIn"] = typeof(RoomCheckedIn),
            ["roomCleaned"] = typeof(RoomCleaned),
        };
        
        private static readonly IDictionary<Type, string> MappingEventTypeToKey = MappingKeyToEventType.ToDictionary(x => x.Value, x => x.Key);
    }
}