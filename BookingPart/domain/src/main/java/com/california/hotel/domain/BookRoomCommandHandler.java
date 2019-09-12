package com.california.hotel.domain;

import java.time.Clock;
import java.util.Collections;
import java.util.List;
import java.util.function.Function;

public class BookRoomCommandHandler
        implements Function<BookRoomCommand, List<DomainEvent>> {
    private Clock clock;

    public BookRoomCommandHandler(Clock clock) {
        this.clock = clock;
    }

    public List<DomainEvent> apply(BookRoomCommand command) {
		return Collections.singletonList(new DomainEvent.PaymentRequired(
			command.getBookingId(),
			command.getStartDate(),
			command.getEndDate(),
			command.getAmount(),
			command.getRoomType(),
			clock.instant()
		));
    }
}
