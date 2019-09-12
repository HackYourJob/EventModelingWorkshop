package com.california.hotel.api;

import org.springframework.stereotype.Service;

@Service
public class AvailabilityRepository {
    private final EventRepository eventRepository;

    public AvailabilityRepository(EventRepository eventRepository) {
        this.eventRepository = eventRepository;
    }

    public Availability getAvailability() {

        return eventRepository.domainEvents().stream()
                .reduce(
                        Availability.init(),
                        (availability, domainEvent) -> availability.handle(domainEvent),
                        Availability::merge
                );
    }
}
