import pytest

from logic import CheckinCommandHandler


def test_no_events():
    handler = CheckinCommandHandler(events=[])
    with pytest.raises(Exception) as e_info:
        handler.process()


def test_available_room_known_guest_events():
    event_room = {}
    event_guest = {}
    handler = CheckinCommandHandler(events=[event_room, event_guest])
    assert (handler.process() is dict)
