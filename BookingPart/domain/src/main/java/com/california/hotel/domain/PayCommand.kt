package com.california.hotel.domain;

public class PayCommand {
    public String bookingId;

    public PayCommand(String bookingId) {
        this.bookingId = bookingId;
    }

    public String getBookingId() {
        return bookingId;
    }

    public void setBookingId(String bookingId) {
        this.bookingId = bookingId;
    }
}
