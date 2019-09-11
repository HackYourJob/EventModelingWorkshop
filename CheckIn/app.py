from flask import Flask, request, session, redirect, url_for, render_template

from app.handler import CheckinCommandHandler
from app.view import CheckinRoomsAvailabilityView

app = Flask(__name__)
app.secret_key = b'_5#y2L"F4Q8z\n\xec]/'


def show_checkin_page():
    view = CheckinRoomsAvailabilityView()
    try:
        context = view.check()
    except Exception as e:
        return show_exception({'message': str(e)})
    else:
        return render_template("checkin.html", **context)


def show_exception(exception):
    return render_template("exception.html", **{'exception': exception})


@app.route('/')
def index():
    return redirect(url_for('checkin'))


@app.route('/checkin', methods=['GET', 'POST'])
def checkin():
    if request.method == 'POST':
        room_id = request.args.get('room')
        guest_id = request.args.get('guest')

        if room_id is None:
            return show_exception({'exception': {'message': 'Room ID is missing'}})
        if guest_id is None:
            return show_exception({'exception': {'message': 'Guest ID is missing'}})

        handler = CheckinCommandHandler(room_id=room_id, guest_id=guest_id)
        try:
            result = handler.process()
        except Exception as e:
            return show_exception({'message': str(e)})
        else:
            return result

    else:
        guest_id = request.args.get('guest')
        if guest_id is None:
            return show_exception({'exception': {'message': 'Guest ID is missing'}})

        return show_checkin_page()
