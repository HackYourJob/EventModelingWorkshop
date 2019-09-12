package com.california.hotel.domain

import com.fasterxml.jackson.annotation.JsonIgnore
import java.time.Instant
import java.time.LocalDate

sealed class DomainEvent(var type: String, @JsonIgnore open var timestamp: Instant = Instant.now()) {
	data class PaymentRequired(
		var bookingId: String? = null,
		var startDate: LocalDate? = null,
		var endDate: LocalDate? = null,
		var amount: Int = 0,
		var roomType: String? = null,
		var instant: Instant = Instant.now()
	) : DomainEvent("payment-required", instant)

	data class PaymentSucceed(
		var bookingId: String? = null,
		var instant: Instant = Instant.now()
	) : DomainEvent("payment-succeed")

	data class RoomBooked(
		var bookingId: String? = null,
		var instant: Instant = Instant.now()
	) : DomainEvent("room-booked")

	data class RoomMadeAvailable(
		var roomId: String,
		var roomType: String,
		var instant: Instant = Instant.now()
	) : DomainEvent("room-made-available")

}
