package com.california.hotel.domain;

import java.time.LocalDate;

public class BookRoomCommand {
    public String guestId;
    public LocalDate startDate;
    public LocalDate endDate;
    public String roomType;
    public int amount;

	public BookRoomCommand() {
	}

	public BookRoomCommand(String guestId, LocalDate startDate, LocalDate endDate, String roomType, int amount) {
        this.guestId = guestId;
        this.startDate = startDate;
        this.endDate = endDate;
        this.roomType = roomType;
        this.amount = amount;
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

    public String getRoomType() {
        return roomType;
    }

    public void setRoomType(String roomType) {
        this.roomType = roomType;
    }

    public int getAmount() {
        return amount;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }
}
