package com.california.hotel.api;

import java.time.Instant;

public class RoomMadeAvailable
    implements DomainEvent
{
    private final Instant timestamp;
    private final String type;
    private final String roomId;
    private final String roomType;


    public Instant getTimestamp() {
        return timestamp;
    }

    public String getType() {
        return type;
    }

    public String getRoomId() {
        return roomId;
    }

    public String getRoomType() {
        return roomType;
    }

    public RoomMadeAvailable(Instant timestamp, String type, String roomId, String roomType) {
        this.timestamp = timestamp;
        this.type = type;
        this.roomId = roomId;
        this.roomType = roomType;
    }

    @Override
    public Instant timestamp() {
        return timestamp;
    }

    @Override
    public String type() {
        return type;
    }
}
