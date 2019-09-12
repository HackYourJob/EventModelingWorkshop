package com.california.hotel.domain;

import java.time.Instant;
import java.time.LocalDate;
import java.util.Objects;

public class PaymentRequired implements DomainEvent {
	public final String bookingId;
	public final Instant timestamp;
    public final String guestId;
    public final LocalDate startDate;
    public final LocalDate endDate;
    public final int amount;
    public final String roomType;

	public PaymentRequired(String bookingId, Instant timestamp, String guestId, LocalDate startDate, LocalDate endDate, int amount, String roomType) {
		this.bookingId = bookingId;
		this.timestamp = timestamp;
        this.guestId = guestId;
        this.startDate = startDate;
        this.endDate = endDate;
        this.amount = amount;
        this.roomType = roomType;
    }

	@Override
	public Instant timestamp() {
		return timestamp;
	}

	@Override
	public String type() {
		return "payment-required";
	}

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
		PaymentRequired that = (PaymentRequired) o;
        return amount == that.amount &&
                timestamp.equals(that.timestamp) &&
                guestId.equals(that.guestId) &&
                startDate.equals(that.startDate) &&
                endDate.equals(that.endDate) &&
                roomType.equals(that.roomType);
    }

    @Override
    public int hashCode() {
        return Objects.hash(timestamp, guestId, startDate, endDate, amount, roomType);
    }
}
