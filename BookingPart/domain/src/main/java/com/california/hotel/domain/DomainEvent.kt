package com.california.hotel.domain;

import java.time.Instant;

public interface DomainEvent {
	Instant timestamp();
	String type();
}
