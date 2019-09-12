package com.california.hotel.api;


import com.california.hotel.domain.*;
import com.california.hotel.infrastrucutre.repository.EventRepository;
import io.swagger.annotations.ApiOperation;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.Clock;
import java.time.LocalDate;
import java.time.ZoneId;
import java.util.*;

@RestController
@RequestMapping(value = "/api")
public class HotelResource {

	private final EventRepository eventRepository;
	private final AvailabilityRepository availabilityRepository;

	public HotelResource(EventRepository eventRepository, AvailabilityRepository availabilityRepository) {
		this.eventRepository = eventRepository;
		this.availabilityRepository = availabilityRepository;
	}

	@ApiOperation(value = "Book a room")
	@PostMapping(value = {"/booking/book-room"}, consumes = MediaType.APPLICATION_JSON_VALUE, produces = MediaType.TEXT_PLAIN_VALUE)
	public ResponseEntity<String> booksHotelRoom(@RequestBody BookRoomCommand command) {
		new BookRoomCommandHandler(Clock.system(ZoneId.systemDefault())).apply(command)
            .forEach(eventRepository::persistEvent);
		return new ResponseEntity<>("OK", HttpStatus.OK);
	}

	@ApiOperation(value = "Payment information")
	@GetMapping(value = {"/payment/details/{bookingId}"}, produces = MediaType.APPLICATION_JSON_VALUE)
	public ResponseEntity<PaymentDetails> paymentDetails(@PathVariable("bookingId") String bookingId) {
		List<DomainEvent> domainEvents = eventRepository.domainEvents();
		Optional<PaymentDetails> paymentRequired = this.eventHandler(domainEvents, bookingId);
		return paymentRequired
			.map(paymentDetails -> new ResponseEntity<>(paymentDetails, HttpStatus.OK))
			.orElseGet(() -> new ResponseEntity<>(HttpStatus.NOT_FOUND));
	}

	@ApiOperation(value = "Pay")
	@PostMapping(value = {"/payment/pay"}, produces = MediaType.APPLICATION_JSON_VALUE)
	public ResponseEntity<String> pay(@RequestBody PayCommand command) {
		new PayCommandHandler(Clock.system(ZoneId.systemDefault())).apply(command)
				.forEach(eventRepository::persistEvent);
		return new ResponseEntity<>("OK", HttpStatus.OK);
	}

	@ApiOperation(value = "Availability")
	@GetMapping(value = {"/availability"}, produces = MediaType.APPLICATION_JSON_VALUE)
	public ResponseEntity<Map<String, Set<LocalDate>>> availability() {
	    Availability availability = availabilityRepository.getAvailability();
		return new ResponseEntity<>(availability.innerMap, HttpStatus.OK);
	}

	private Optional<PaymentDetails> eventHandler(List<DomainEvent> events, String bookingId) {
		return events.stream()
			.filter(x -> x instanceof PaymentRequired)
			.map(x -> (PaymentRequired) x)
			.filter(x -> x.getBookingId().equals(bookingId))
			.sorted(Comparator.comparing(PaymentRequired::timestamp).reversed())
			.map(x -> new PaymentDetails(x.getBookingId(), x.getAmount()))
			.findFirst();
	}

}
