package com.california.hotel.api;

import java.time.Clock;
import java.util.Collections;
import java.util.List;
import java.util.UUID;
import java.util.function.Function;

public class BookRoomCommandHandler
        implements Function<BookRoomCommand, List<DomainEvent>> {
    private Clock clock;

    public BookRoomCommandHandler(Clock clock) {
        this.clock = clock;
    }

    public List<DomainEvent> apply(BookRoomCommand command) {
        return Collections.singletonList(new PaymentRequired(UUID.randomUUID().toString(), clock.instant(), command.guestId, command.startDate, command.endDate, command.amount, command.roomType));
    }
}
