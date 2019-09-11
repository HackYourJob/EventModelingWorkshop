package com.california.hotel.api;

import java.time.Instant;

public interface DomainEvent {
	Instant timestamp();
	String type();
}
