package com.california.hotel.api;


import io.swagger.annotations.ApiOperation;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.Clock;
import java.time.ZoneId;
import java.util.Comparator;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping(value = "/api")
public class HotelResource {

	private final EventRepository eventRepository;

	public HotelResource(EventRepository eventRepository) {
		this.eventRepository = eventRepository;
	}

	@ApiOperation(value = "Book a room")
	@PostMapping(value = {"/booking/book-room"}, produces = MediaType.APPLICATION_JSON_VALUE)
	public ResponseEntity<String> booksHotelRoom(@RequestBody BookRoomCommand command) {
		new BookRoomCommandHandler(Clock.system(ZoneId.systemDefault())).apply(command)
            .forEach(eventRepository::persistEvent);
		return new ResponseEntity<>("OK", HttpStatus.OK);
	}

	@ApiOperation(value = "Payment information")
	@GetMapping(value = {"/payment/details/{guestId}"}, produces = MediaType.APPLICATION_JSON_VALUE)
	public ResponseEntity<PaymentDetails> paymentDetails(@PathVariable("guestId") String guestId) {
		List<DomainEvent> domainEvents = eventRepository.domainEvents();
		Optional<PaymentDetails> paymentRequired = this.eventHandler(domainEvents, guestId);
		return paymentRequired
			.map(paymentDetails -> new ResponseEntity<>(paymentDetails, HttpStatus.OK))
			.orElseGet(() -> new ResponseEntity<>(HttpStatus.NOT_FOUND));
	}

	private Optional<PaymentDetails> eventHandler(List<DomainEvent> events, String guestId) {
		return events.stream()
			.filter(x -> x instanceof PaymentRequired)
			.map(x -> (PaymentRequired) x)
			.filter(x -> x.guestId.equals(guestId))
			.sorted(Comparator.comparing(PaymentRequired::timestamp).reversed())
			.map(x -> new PaymentDetails(x.bookingId, x.amount))
			.findFirst();
	}

}
