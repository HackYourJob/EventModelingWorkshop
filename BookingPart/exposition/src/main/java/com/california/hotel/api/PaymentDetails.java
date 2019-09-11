package com.california.hotel.api;

public class PaymentDetails {
	public String bookingId;
	public int amount;

	public PaymentDetails(String bookingId, int amount) {
		this.bookingId = bookingId;
		this.amount = amount;
	}

	public String getBookingId() {
		return bookingId;
	}

	public void setBookingId(String bookingId) {
		this.bookingId = bookingId;
	}

	public int getAmount() {
		return amount;
	}

	public void setAmount(int amount) {
		this.amount = amount;
	}
}
