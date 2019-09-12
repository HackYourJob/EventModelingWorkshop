<?php


namespace App\EventStore;


class InMemoryEventStore implements EventStore
{
    private $events = [];

    public function append(Event $event, string $suffix): void
    {
        $this->events[] = $event;
    }

    public function getEvents(): array
    {
        return $this->events;
    }
}
