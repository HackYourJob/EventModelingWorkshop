import json
import os

from flask import Flask, request, session, redirect, url_for, render_template

from datetime import datetime

app = Flask(__name__)
app.secret_key = b'_5#y2L"F4Q8z\n\xec]/'

EVENTS_DIR = os.path.join(os.getcwd(), 'events')


class CheckinCommandHandler(object):

    def __init__(self, events=None):
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


def show_checkin_page():
    handler = CheckinCommandHandler()
    try:
        context = handler.process()
    except Exception as e:
        return show_exception({'message': str(e)})
    else:
        return render_template("checkin.html", **context)


def show_exception(exception):
    return render_template("exception.html", **{'exception': exception})


def show_missing_guest():
    return render_template("missing_guest.html")


@app.route('/')
def index():
    return redirect(url_for('checkin'))


@app.route('/checkin', methods=['GET', 'POST'])
def checkin():
    if request.method == 'POST':
        room_id = request.args.get('room')
        guest_id = request.args.get('guest')
        filename = '{}-Checkin-room{}-guest{}.json'.format(int(datetime.now().timestamp() * 1000), room_id, guest_id)
        filepath = os.path.join(os.getcwd(), 'events', filename)
        with open(filepath, 'w') as event_file:
            event_file.write(json.dumps({'room': room_id, 'guest': guest_id}))
        return 'Guest {} checked in with room {}'.format(guest_id, room_id)
    else:
        guest_id = request.args.get('guest')
        if guest_id is None:
            return show_missing_guest()
        return show_checkin_page()
