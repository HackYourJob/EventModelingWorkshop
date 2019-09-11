<?php


namespace App\ReadModels;


use App\Events\RoomAddedToInventory;
use App\EventStore\Event;
use App\EventStore\EventStore;

class RoomExists
{
    /**
     * @var EventStore
     */
    private $eventStore;

    public function __construct(EventStore $eventStore)
    {
        $this->eventStore = $eventStore;
    }

    public function __invoke(int $id): bool
    {
        foreach ($this->eventStore->getEvents() as $event) {
            /** @var $event Event */
            if ($event->getType() === RoomAddedToInventory::eventType && $event->getData()['id'] === $id) {
                return true;
            }
        }

        return false;
    }
}
