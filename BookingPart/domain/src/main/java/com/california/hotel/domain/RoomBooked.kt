package com.california.hotel.domain

import java.time.Instant

data class RoomBooked(val bookingId: String? = null, val timestamp: Instant = Instant.now()) : DomainEvent {

	override fun timestamp(): Instant {
		return timestamp
	}

	override fun type(): String {
		return "room-booked"
	}

}
