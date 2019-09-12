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
		override var timestamp: Instant = Instant.now()
	) : DomainEvent("payment-required", timestamp)

	data class PaymentSucceed(
		var bookingId: String? = null,
		override var timestamp: Instant = Instant.now()
	) : DomainEvent("payment-succeed", timestamp)

	data class RoomBooked(
		var bookingId: String? = null,
		override var timestamp: Instant = Instant.now()
	) : DomainEvent("room-booked", timestamp)

	data class RoomMadeAvailable(
		var roomId: String,
		var roomType: String,
		override var timestamp: Instant = Instant.now()
	) : DomainEvent("room-made-available", timestamp)

}
