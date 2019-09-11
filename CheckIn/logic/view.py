import json
from .constants import *


class CheckinRoomsAvailabilityView(object):

    def __init__(self, booking_id, booking_events=None, room_events=None):
        self.booking_id = booking_id
        self.booking_events = booking_events or []
        self.room_events = room_events or []

        if room_events is None:
            self._read_events(self.room_events, 'roomadded')
        if booking_events is None:
            self._read_events(self.booking_events, 'booking')

    def _read_events(self, events, key):
        filtered_event_filenames = [f for f in os.listdir(EVENTS_DIR) if key in f.lower()]
        for event_filename in filtered_event_filenames:
            with open(os.path.join(EVENTS_DIR, event_filename), 'r') as event_file:
                events.append(json.load(event_file))

    def check(self):
        if self.booking_id is None:
            raise Exception('There is no booking ID')
        if len(self.booking_events) == 0:
            raise Exception('There is no booking events to process')
        if len(self.room_events) == 0:
            raise Exception('There is no room events to process')

        rooms = [{'id': r['id'], 'type': r['type']} for r in self.room_events]
        bookings = [b for b in self.booking_events if str(b['id']) == str(self.booking_id)]
        if len(bookings) == 1:
            raise Exception('There is no valid booking event for id: {}'.format(self.booking_id))
        if len(bookings) > 1:
            raise Exception('There is multiple booking events for id: {}'.format(self.booking_id))

        return {'rooms': rooms, 'booking': bookings[0]}
