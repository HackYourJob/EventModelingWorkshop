import json
from .constants import *


class CheckinRoomsAvailabilityView(object):

    def __init__(self, events=None):
        self.events = events or []
        if events is None:
            self._read_events()

    def _read_events(self):
        filtered_event_filenames = [f for f in os.listdir(EVENTS_DIR)]

        for event_filename in filtered_event_filenames:
            with open(os.path.join(EVENTS_DIR, event_filename), 'r') as event_file:
                self.events.append(json.load(event_file))

    def check(self):
        if len(self.events) == 0:
            raise Exception('There is no events to process')

        rooms = [
            {'number': '101', 'type': 'King', 'price': '300'},
            {'number': '102', 'type': 'King', 'price': '300'},
            {'number': '204', 'type': 'double', 'price': '250'},
            {'number': '205', 'type': 'double', 'price': '250'},
            {'number': '206', 'type': 'King', 'price': '300'}
        ]

        guest = {
            'id': 999,
            'first_name': 'John',
            'last_name': 'Doe',
            'nights': '3'
        }

        return {'rooms': rooms, 'guest': guest}
