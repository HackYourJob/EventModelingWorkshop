package com.california.hotel.infrastrucutre.repository;

import com.california.hotel.domain.DomainEvent;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;
import org.springframework.stereotype.Service;

import java.io.File;
import java.io.IOException;
import java.time.Instant;
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
		DomainEvent event = null;
		try {
			Class<? extends DomainEvent> valueType = null;
			if (fileName.endsWith("payment-required.json")) {
				valueType = DomainEvent.PaymentRequired.class;
			} else if (fileName.endsWith("room-made-available.json")) {
				valueType = DomainEvent.RoomMadeAvailable.class;
			}
			event = getEvent(fileName, valueType);
		} catch (IOException e) {
			e.printStackTrace();
		}
		if (event != null) {
			event.setTimestamp(instantFromFilename(fileName));
		}
		return event;
	}

	private DomainEvent getEvent(String fileName, Class<? extends DomainEvent> valueType) throws IOException {
		return objectMapper.readValue(new File(eventsFolder + File.separator + fileName), valueType);
	}

	private Instant instantFromFilename(String fileName) {
		String millis = fileName.substring(0, fileName.indexOf("-"));
		return Instant.ofEpochMilli(Long.parseLong(millis));
	}


}
