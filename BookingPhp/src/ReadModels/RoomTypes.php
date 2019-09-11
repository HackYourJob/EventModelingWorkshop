<?php


namespace App\ReadModels;


use App\Events\RoomTypePriceChanged;
use App\EventStore\Event;
use App\EventStore\EventStore;

class RoomTypes
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

        return array_reduce(array_filter($events, function (Event $event) {
            return $event->getType() === RoomTypePriceChanged::eventType;
        }), function (array $data, Event $event) {
            $data[$event->getData()['type']] = $event->getData();

            return $data;
        }, ['double' => ['type' => 'double', 'price' => 100], 'twin' => ['type' => 'twin', 'price' => 90]]);
    }
}
