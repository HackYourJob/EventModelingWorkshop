<?php


namespace App\ReadModels;


use App\Events\RoomAddedToInventory;
use App\EventStore\Event;
use App\EventStore\EventStore;

class RoomInventory
{
    /**
     * @var EventStore
     */
    private $eventStore;

    public function __construct(EventStore $eventStore)
    {
        $this->eventStore = $eventStore;
    }

    public function __invoke(): array
    {
        $events = $this->eventStore->getEvents();

        return array_map(function (Event $event) {
            return $event->getData();
        }, array_filter($events, function (Event $event) {
            return $event->getType() === RoomAddedToInventory::eventType;
        }));
    }
}
