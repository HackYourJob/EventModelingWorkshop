package com.california.hotel.domain

import java.time.LocalDate

class BookRoomCommand(
	var bookingId: String? = null,
	var startDate: LocalDate? = null,
	var endDate: LocalDate? = null,
	var roomType: String? = null,
	var amount: Int = 0
)
