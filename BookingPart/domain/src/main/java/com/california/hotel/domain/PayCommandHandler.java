package com.california.hotel.domain;

import java.time.Clock;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;

public class PayCommandHandler
        implements Function<PayCommand, List<DomainEvent>> {
    private Clock clock;

    public PayCommandHandler(Clock clock) {
        this.clock = clock;
    }

    public List<DomainEvent> apply(PayCommand command) {
        List<DomainEvent> events = new ArrayList<>();
		events.add(new PaymentSucceed(command.getBookingId(), clock.instant()));
		events.add(new RoomBooked(command.getBookingId(), clock.instant()));
        return events;
    }
}
