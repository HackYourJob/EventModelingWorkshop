package com.california.hotel.infrastrucutre.repository;

import com.california.hotel.domain.DomainEvent;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;
import org.springframework.stereotype.Service;

import java.io.File;
import java.io.IOException;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Service
@Configuration("application.properties")
public class EventRepository {

	private final ObjectMapper objectMapper;

	public EventRepository(ObjectMapper objectMapper) {
		this.objectMapper = objectMapper;
	}

	@Value("${events.folder}")
    String eventsFolder;

	public void persistEvent(DomainEvent event) {
		try {
			objectMapper.writeValue(new File(eventsFolder + "/" + event.getTimestamp().toEpochMilli() + "-" + event.getType() + ".json"), event);
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public List<DomainEvent> domainEvents() {
		return Stream.of(new File(eventsFolder).list())
			.filter(x -> x.endsWith(".json"))
			.map(this::fileNameToDomainEvent)
			.filter(Objects::nonNull)
			.collect(Collectors.toList());
	}

	private DomainEvent fileNameToDomainEvent(String fileName) {
		try {
			if (fileName.endsWith("payment-required.json")) {
				return objectMapper.readValue(new File(eventsFolder + File.separator + fileName), DomainEvent.PaymentRequired.class);
			}
			else if (fileName.endsWith("room-made-available.json")) {
				return objectMapper.readValue(new File(eventsFolder + File.pathSeparator + fileName), DomainEvent.RoomMadeAvailable.class);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		return null;
	}


}
