package com.california.hotel.domain;

import java.time.Instant;
import java.time.LocalDate;
import java.util.Objects;

public class PaymentRequired implements DomainEvent {
	public String bookingId;
	public Instant timestamp;
    public String guestId;
    public LocalDate startDate;
    public LocalDate endDate;
    public int amount;
    public String roomType;

	public PaymentRequired() {
	}

	public PaymentRequired(String bookingId, Instant timestamp, String guestId, LocalDate startDate, LocalDate endDate, int amount, String roomType) {
		this.bookingId = bookingId;
		this.timestamp = timestamp;
        this.guestId = guestId;
        this.startDate = startDate;
        this.endDate = endDate;
        this.amount = amount;
        this.roomType = roomType;
    }

	public String getBookingId() {
		return bookingId;
	}

	public void setBookingId(String bookingId) {
		this.bookingId = bookingId;
	}

	public Instant getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(Instant timestamp) {
		this.timestamp = timestamp;
	}

	public String getGuestId() {
		return guestId;
	}

	public void setGuestId(String guestId) {
		this.guestId = guestId;
	}

	public LocalDate getStartDate() {
		return startDate;
	}

	public void setStartDate(LocalDate startDate) {
		this.startDate = startDate;
	}

	public LocalDate getEndDate() {
		return endDate;
	}

	public void setEndDate(LocalDate endDate) {
		this.endDate = endDate;
	}

	public int getAmount() {
		return amount;
	}

	public void setAmount(int amount) {
		this.amount = amount;
	}

	public String getRoomType() {
		return roomType;
	}

	public void setRoomType(String roomType) {
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
