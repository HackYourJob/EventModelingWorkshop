package com.california.hotel.infrastructure.repository;

import com.california.hotel.domain.BookRoomCommand;
import com.california.hotel.domain.BookRoomCommandHandler;
import com.california.hotel.domain.DomainEvent;
import com.california.hotel.domain.PaymentRequired;
import org.junit.Test;

import java.time.Clock;
import java.time.Instant;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Collections;
import java.util.List;
import java.util.UUID;

import static org.assertj.core.api.Assertions.assertThat;

public class BookRoomCommandHandlerTest {

    @Test
    public void shouldEmitAPaymentRequiredEventWhenBookingARoom() {
        Clock clock = Clock.fixed(Instant.EPOCH, ZoneId.systemDefault());
        BookRoomCommandHandler systemUnderTest = new BookRoomCommandHandler(clock);
		String bookingId = UUID.randomUUID().toString();
        LocalDate startDate = LocalDate.of(2019, 9, 10);
        LocalDate endDate = LocalDate.of(2019, 9, 11);
        String roomType = "twin";
        int amount = 300;
		BookRoomCommand command = new BookRoomCommand(bookingId, startDate, endDate, roomType, amount);
		List<DomainEvent> expected = Collections.singletonList(new PaymentRequired(bookingId, clock.instant(), startDate, endDate, amount, roomType));
        List<DomainEvent> events = systemUnderTest.apply(command);
        assertThat(events).containsExactlyElementsOf(expected);
    }

}
