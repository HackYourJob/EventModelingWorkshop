package com.california.hotel.api;

import com.california.hotel.domain.DomainEvent;
import com.california.hotel.domain.RoomMadeAvailable;
import com.google.common.collect.ImmutableMap;
import com.google.common.collect.ImmutableSet;

import java.time.LocalDate;
import java.time.ZoneId;
import java.util.Map;
import java.util.Set;

public class Availability {
    public final Map<String, Set<LocalDate>> innerMap;

    Availability(Map<String, Set<LocalDate>> innerMap) {
        this.innerMap = innerMap;
    }

    public static Availability init() {
        return new Availability(
                ImmutableMap.of(
                    "king", ImmutableSet.of(),
                    "twin", ImmutableSet.of()
            )
        );
    }

    private Availability setAvailableAt(String type, LocalDate date) {
        Set<LocalDate> newDates = ImmutableSet
                .<LocalDate>builder()
                .addAll(this.innerMap.get(type))
                .add(date)
                .build();
        Map<String, Set<LocalDate>> newInnerMap = ImmutableMap.<String, Set<LocalDate>>builder()
                .putAll(this.innerMap)
                .put(type, newDates)
                .build();
        return new Availability(
                newInnerMap
        );
    }

    public Availability handle(DomainEvent event) {
        if(event instanceof RoomMadeAvailable) {
            return this.setAvailableAt(((RoomMadeAvailable) event).getRoomType(), ((RoomMadeAvailable) event).getTimestamp().atZone(ZoneId.systemDefault()).toLocalDate());
        }
        else {
            return this;
        }
    }

    public static Availability merge(Availability availability1, Availability availability2) {
        Set<String> keys = ImmutableSet.<String>builder()
                .addAll(availability1.innerMap.keySet())
                .addAll(availability2.innerMap.keySet())
                .build();

        ImmutableMap.Builder<String, Set<LocalDate>> mergedMapBuilder = ImmutableMap.builder();
        keys.stream()
                .peek(key ->
                    mergedMapBuilder.put(key, ImmutableSet.<LocalDate>builder()
                                    .addAll(availability1.innerMap.get(key))
                                    .addAll(availability2.innerMap.get(key))
                                    .build())
                );
        return new Availability(mergedMapBuilder.build());

    }
}
