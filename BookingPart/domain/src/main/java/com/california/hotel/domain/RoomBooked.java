package com.california.hotel.domain;

import java.time.Instant;
import java.util.Objects;

public class RoomBooked implements DomainEvent {
	public final String bookingId;
	public final Instant timestamp;

	public RoomBooked(String bookingId, Instant timestamp) {
		this.bookingId = bookingId;
		this.timestamp = timestamp;
    }

	@Override
	public Instant timestamp() {
		return timestamp;
	}

	@Override
	public String type() {
		return "room-booked";
	}

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
		RoomBooked that = (RoomBooked) o;
        return bookingId == that.bookingId &&
                timestamp.equals(that.timestamp);
    }

    @Override
    public int hashCode() {
        return Objects.hash(timestamp);
    }
}
