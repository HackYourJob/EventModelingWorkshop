import json
from datetime import datetime
from .constants import *


class CheckinCommandHandler(object):

    def __init__(self, room_id, guest_id, events=None):
        self.room_id = room_id
        self.guest_id = guest_id
        self.events = events or []
        if events is None:
            self._read_events()

    def _read_events(self):
        filtered_event_filenames = [f for f in os.listdir(EVENTS_DIR)]

        for event_filename in filtered_event_filenames:
            with open(os.path.join(EVENTS_DIR, event_filename), 'r') as event_file:
                self.events.append(json.load(event_file))

    def process(self):
        if len(self.events) == 0:
            raise Exception('There is no events to process')

        # Write event file...

        filename = '{}-Checkin-room{}-guest{}.json'.format(int(datetime.now().timestamp() * 1000),
                                                           self.room_id,
                                                           self.guest_id)

        filepath = os.path.join(os.getcwd(), 'events', filename)
        with open(filepath, 'w') as event_file:
            event_file.write(json.dumps({'room': self.room_id, 'guest': self.guest_id}))

        return 'Guest {} checked in with room {}'.format(self.guest_id, self.room_id)
