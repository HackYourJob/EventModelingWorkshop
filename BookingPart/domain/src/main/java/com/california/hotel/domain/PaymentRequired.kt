package com.california.hotel.domain

import java.time.Instant
import java.time.LocalDate

data class PaymentRequired(
	var bookingId: String? = null,
	var timestamp: Instant? = null,
	var startDate: LocalDate? = null,
	var endDate: LocalDate? = null,
	var amount: Int = 0,
	var roomType: String? = null) : DomainEvent {

	override fun timestamp(): Instant? {
		return timestamp
	}

	override fun type(): String {
		return "payment-required"
	}

}
