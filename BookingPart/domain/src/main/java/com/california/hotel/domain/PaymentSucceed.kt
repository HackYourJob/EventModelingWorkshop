package com.california.hotel.domain

import java.time.Instant

class PaymentSucceed(val bookingId: String? = null, val timestamp: Instant = Instant.now()) : DomainEvent {

	override fun timestamp(): Instant {
		return timestamp
	}

	override fun type(): String {
		return "payment-succeed"
	}

}
